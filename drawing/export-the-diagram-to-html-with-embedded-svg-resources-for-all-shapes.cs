using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class HtmlStreamProvider : IStreamProvider
{
    private readonly string _outputDirectory;

    public HtmlStreamProvider(string outputDirectory)
    {
        _outputDirectory = outputDirectory;
    }

    // Called by Aspose.Diagram when a resource stream is needed
    public void InitStream(StreamProviderOptions options)
    {
        // Ensure the output directory exists
        Directory.CreateDirectory(_outputDirectory);

        // Build the full file path for the resource using the default name provided by Aspose.Diagram
        string filePath = Path.Combine(_outputDirectory, options.DefaultPath);

        // Create a writable file stream for the resource
        options.Stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
    }

    // Called by Aspose.Diagram after writing to the resource stream
    public void CloseStream(StreamProviderOptions options)
    {
        // Close and dispose the stream if it was created
        options.Stream?.Close();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output HTML file path (same folder, same name with .html extension)
        string outputDirectory = Path.GetDirectoryName(inputPath) ?? Directory.GetCurrentDirectory();
        string outputFileName = Path.GetFileNameWithoutExtension(inputPath) + ".html";
        string outputPath = Path.Combine(outputDirectory, outputFileName);

        // Directory where embedded SVG (and other) resources will be saved
        string resourcesDirectory = Path.Combine(outputDirectory, Path.GetFileNameWithoutExtension(outputFileName) + "_files");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Do not export hidden pages (bool property)
                ExportHiddenPage = false,
                // Do not include comments in the HTML
                IsExportComments = false,
                // Use custom stream provider to control where resources are written
                StreamProvider = new HtmlStreamProvider(resourcesDirectory)
            };

            // Save the diagram as HTML; resources (SVG images) will be written via the stream provider
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine($"Diagram successfully exported to HTML: {outputPath}");
            Console.WriteLine($"Embedded resources are stored in: {resourcesDirectory}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred during export: {ex.Message}");
        }
    }
}
