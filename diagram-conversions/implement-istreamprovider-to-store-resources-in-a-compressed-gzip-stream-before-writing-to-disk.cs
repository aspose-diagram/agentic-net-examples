using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class GzipStreamProvider : IStreamProvider
{
    // Initialize the stream for a resource. The resource will be written to a GZIP-compressed file.
    public void InitStream(StreamProviderOptions options)
    {
        // Determine the target file path for the compressed resource.
        // Append ".gz" to the default path provided by Aspose.
        string gzipFilePath = options.DefaultPath + ".gz";

        // Ensure the directory for the target file exists.
        string directory = Path.GetDirectoryName(gzipFilePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Create a file stream and wrap it with GZipStream for compression.
        FileStream fileStream = new FileStream(gzipFilePath, FileMode.Create, FileAccess.Write);
        GZipStream gzipStream = new GZipStream(fileStream, CompressionMode.Compress);

        // Assign the compressed stream back to the options so Aspose writes into it.
        options.Stream = gzipStream;
    }

    // Close and dispose the stream after the resource has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        if (options.Stream != null)
        {
            // Disposing the GZipStream also disposes the underlying FileStream.
            options.Stream.Dispose();
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
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputHtmlPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
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

            // Save the diagram to HTML using the configured options.
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine("Diagram successfully saved to HTML with GZIP-compressed resources.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}