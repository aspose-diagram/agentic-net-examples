using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CustomStreamProvider : IStreamProvider
{
    // Base directory where all resources will be placed
    private readonly string _baseOutputDir;

    public CustomStreamProvider(string baseOutputDir)
    {
        _baseOutputDir = baseOutputDir;
    }

    // Called by Aspose when a resource stream is required
    public void InitStream(StreamProviderOptions options)
    {
        try
        {
            // Ensure the default path is available
            if (string.IsNullOrEmpty(options.DefaultPath))
            {
                Console.Error.WriteLine("StreamProviderOptions.DefaultPath is null or empty.");
                return;
            }

            // Determine the file name from the default path
            string fileName = Path.GetFileName(options.DefaultPath);
            if (string.IsNullOrEmpty(fileName))
            {
                Console.Error.WriteLine($"Unable to extract file name from path '{options.DefaultPath}'.");
                return;
            }

            // Decide subfolder based on file extension (image vs shape)
            string extension = Path.GetExtension(fileName).ToLowerInvariant();
            string subFolder = "others";

            // Common image extensions
            if (extension == ".png" || extension == ".jpg" || extension == ".jpeg" ||
                extension == ".gif" || extension == ".bmp" || extension == ".tiff")
            {
                subFolder = "images";
            }
            // SVG files are typically shape resources
            else if (extension == ".svg")
            {
                subFolder = "shapes";
            }

            // Build the full directory path and ensure it exists
            string targetDir = Path.Combine(_baseOutputDir, subFolder);
            Directory.CreateDirectory(targetDir);

            // Full path for the resource file
            string fullPath = Path.Combine(targetDir, fileName);

            // Assign a writable file stream to the options
            options.Stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error in InitStream: {ex.Message}");
        }
    }

    // Called by Aspose after the resource has been written
    public void CloseStream(StreamProviderOptions options)
    {
        try
        {
            // Close and dispose the stream if it was created
            if (options.Stream != null)
            {
                options.Stream.Close();
                options.Stream.Dispose();
                options.Stream = null;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error in CloseStream: {ex.Message}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (hard‑coded for this example)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define output directory and main HTML file name
        string outputDir = "output";
        string htmlFilePath = Path.Combine(outputDir, "diagram.html");

        try
        {
            // Ensure the output directory exists
            Directory.CreateDirectory(outputDir);

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options with the custom stream provider
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new CustomStreamProvider(outputDir);

            // Save the diagram as HTML; resources will be routed by the provider
            diagram.Save(htmlFilePath, htmlOptions);

            Console.WriteLine($"Diagram successfully saved to '{htmlFilePath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}