using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class TimestampStreamProvider : IStreamProvider
{
    private readonly string _outputDirectory;

    public TimestampStreamProvider(string outputDirectory)
    {
        _outputDirectory = outputDirectory;
    }

    // Called by Aspose.Diagram when a resource stream is needed
    public void InitStream(StreamProviderOptions options)
    {
        // Ensure the output directory exists
        Directory.CreateDirectory(_outputDirectory);

        // Extract the original file name from the default path provided by Aspose
        string originalFileName = Path.GetFileName(options.DefaultPath);

        // Create a timestamp prefix (yyyyMMddHHmmssfff) for uniqueness
        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");

        // Build the new file name with the timestamp prefix
        string newFileName = $"{timestamp}_{originalFileName}";

        // Combine with the output directory to get the full path
        string fullPath = Path.Combine(_outputDirectory, newFileName);

        // Assign a writable FileStream to the options so Aspose can write the resource
        options.Stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
    }

    // Called by Aspose.Diagram after the resource has been written
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
        // Expect two arguments: input Visio file path and output HTML file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputHtmlPath>");
            return;
        }

        string inputPath = args[0];
        string outputHtmlPath = args[1];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Determine the directory where HTML resources (images, etc.) will be stored
        string outputDir = Path.GetDirectoryName(outputHtmlPath);
        if (string.IsNullOrEmpty(outputDir))
        {
            Console.Error.WriteLine("Unable to determine output directory from the HTML path.");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Create a subfolder for resources to keep the HTML folder tidy
        string resourcesDir = Path.Combine(outputDir, "Resources");
        Directory.CreateDirectory(resourcesDir);

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options and assign the custom stream provider
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new TimestampStreamProvider(resourcesDir);

            // Save the diagram as HTML using the configured options
            diagram.Save(outputHtmlPath, htmlOptions);

            Console.WriteLine($"Diagram successfully saved to HTML: {outputHtmlPath}");
            Console.WriteLine($"Resources are stored in: {resourcesDir}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}