using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class GzipStreamProvider : IStreamProvider
{
    // Initialize the stream for a resource, compressing it with GZip before writing to disk.
    public void InitStream(StreamProviderOptions options)
    {
        // The DefaultPath property provides the intended file name for the resource.
        string resourcePath = options.DefaultPath;

        // Ensure the directory for the resource exists.
        string directory = Path.GetDirectoryName(resourcePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Create a file stream for the resource and wrap it with GZip compression.
        FileStream fileStream = new FileStream(resourcePath, FileMode.Create, FileAccess.Write);
        GZipStream gzipStream = new GZipStream(fileStream, CompressionMode.Compress);

        // Assign the compressed stream back to the options so Aspose can write into it.
        options.Stream = gzipStream;
    }

    // Close and dispose the stream after the resource has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        if (options.Stream != null)
        {
            options.Stream.Dispose();
            options.Stream = null;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output HTML file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <inputVisioPath> <outputHtmlPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options with the custom GZIP stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new GzipStreamProvider();

            // Ensure the output directory exists.
            string outputDir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Save the diagram to HTML; resources will be written via the GZIP provider.
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine($"Diagram successfully saved to HTML with compressed resources at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}