using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ProgressStreamProvider : IStreamProvider
{
    private readonly string _outputDirectory;
    private int _streamCount = 0;

    public ProgressStreamProvider(string outputDirectory)
    {
        _outputDirectory = outputDirectory;
    }

    // Called before a stream is created – no special initialization needed.
    public void InitStream(StreamProviderOptions options)
    {
        // No initialization required for this implementation.
    }

    // Called to obtain a stream for a resource (e.g., image, CSS) during HTML export.
    public Stream GetStream(StreamProviderOptions options)
    {
        // Increment the counter and update the console progress indicator.
        _streamCount++;
        Console.Write($"\rStreams created: {_streamCount}");

        // Ensure the output directory exists.
        if (!Directory.Exists(_outputDirectory))
        {
            Directory.CreateDirectory(_outputDirectory);
        }

        // Combine the output directory with the default path provided by the options.
        // options.DefaultPath is read‑only and contains the relative file name.
        string filePath = Path.Combine(_outputDirectory, options.DefaultPath);

        // Ensure the directory for the specific file exists (handles subfolders).
        string fileDir = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(fileDir))
        {
            Directory.CreateDirectory(fileDir);
        }

        // Return a writable file stream for the resource.
        return new FileStream(filePath, FileMode.Create, FileAccess.Write);
    }

    // Called after the stream has been written – no special cleanup needed.
    public void CloseStream(StreamProviderOptions options)
    {
        // No cleanup required for this implementation.
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output directory.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputDirectory>");
            return;
        }

        string inputPath = args[0];
        string outputDir = args[1];

        // Guard: verify the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure the output directory exists or can be created.
        try
        {
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output directory '{outputDir}': {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML export options with a custom stream provider for progress reporting.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new ProgressStreamProvider(outputDir);

            // Define the main HTML file path.
            string mainHtmlPath = Path.Combine(outputDir, "index.html");

            // Save the diagram as HTML, invoking the stream provider for each resource.
            diagram.Save(mainHtmlPath, htmlOptions);

            // Move to a new line after the progress bar output.
            Console.WriteLine("\nHTML export completed successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}