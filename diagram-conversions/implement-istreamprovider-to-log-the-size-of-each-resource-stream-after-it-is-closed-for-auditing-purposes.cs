using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CustomStreamProvider : IStreamProvider
{
    // Called by Aspose.Diagram before writing a resource (e.g., image) to a stream.
    public void InitStream(StreamProviderOptions options)
    {
        // The DefaultPath property contains the relative file name for the resource.
        string resourcePath = options.DefaultPath;

        // Ensure the directory for the resource exists.
        string directory = Path.GetDirectoryName(resourcePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Create a file stream that Aspose will write the resource into.
        options.Stream = new FileStream(resourcePath, FileMode.Create, FileAccess.Write);
    }

    // Called by Aspose.Diagram after the resource has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        if (options.Stream != null)
        {
            // Capture the size of the written resource.
            long sizeInBytes = options.Stream.Length;

            // Dispose the stream to release the file handle.
            options.Stream.Dispose();

            // Log the resource name and its size for auditing.
            Console.WriteLine($"Resource '{options.DefaultPath}' written, size: {sizeInBytes} bytes.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Determine input Visio file path.
        string inputPath = args.Length > 0 ? args[0] : "sample.vsdx";

        // Guard: verify the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output HTML file path.
        string outputHtmlPath = args.Length > 1 ? args[1] : "output.html";

        // Ensure the output directory exists.
        string outputDir = Path.GetDirectoryName(outputHtmlPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options and assign the custom stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new CustomStreamProvider();

            // Save the diagram as HTML. Resources (images, etc.) will be handled by the stream provider.
            diagram.Save(outputHtmlPath, htmlOptions);
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}