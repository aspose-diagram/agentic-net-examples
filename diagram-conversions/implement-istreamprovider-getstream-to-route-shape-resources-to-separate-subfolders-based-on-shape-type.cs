using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CustomStreamProvider : IStreamProvider
{
    private readonly string _basePath;

    // Constructor receives the base output directory where subfolders will be created.
    public CustomStreamProvider(string basePath)
    {
        _basePath = basePath;
    }

    // Called before a resource stream is created. No special initialization needed here.
    public void InitStream(StreamProviderOptions options)
    {
        // No initialization required for this implementation.
    }

    // Called after a resource stream is closed. No special cleanup needed here.
    public void CloseStream(StreamProviderOptions options)
    {
        // No cleanup required for this implementation.
    }

    // Provides a writable stream for each resource (image, shape, etc.) during HTML export.
    public Stream GetStream(StreamProviderOptions options)
    {
        // The DefaultPath property contains the original file name (e.g., "image1.png").
        string fileName = Path.GetFileName(options.DefaultPath);
        string extension = Path.GetExtension(fileName).ToLowerInvariant();

        // Determine subfolder based on file type.
        string subFolder = "Resources";
        if (extension == ".png" || extension == ".jpg" || extension == ".jpeg" || extension == ".gif" || extension == ".bmp")
        {
            subFolder = "Images";
        }
        else if (extension == ".svg")
        {
            subFolder = "Shapes";
        }

        // Ensure the target directory exists.
        string targetDir = Path.Combine(_basePath, subFolder);
        Directory.CreateDirectory(targetDir);

        // Full path for the resource file.
        string fullPath = Path.Combine(targetDir, fileName);

        // Create the file stream for writing.
        FileStream stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
        // Assign the stream back to options (required by the interface contract).
        options.Stream = stream;

        return stream;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output directory.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputDirectory>");
            return;
        }

        string inputPath = args[0];
        string outputDir = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure output directory exists (create if necessary).
        if (!Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML export options with the custom stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new CustomStreamProvider(outputDir);

            // Define the main HTML file path.
            string htmlOutputPath = Path.Combine(outputDir, "output.html");

            // Save the diagram as HTML using the configured options.
            diagram.Save(htmlOutputPath, htmlOptions);

            Console.WriteLine($"HTML export completed successfully: {htmlOutputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}