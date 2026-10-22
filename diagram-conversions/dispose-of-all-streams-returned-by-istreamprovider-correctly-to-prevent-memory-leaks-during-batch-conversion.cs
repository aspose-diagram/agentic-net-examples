using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class MyStreamProvider : IStreamProvider
{
    // Base directory where all HTML resources (images, shapes) will be saved.
    private readonly string _basePath;

    public MyStreamProvider(string basePath)
    {
        _basePath = basePath;
    }

    // Called by Aspose.Diagram before writing a resource stream.
    public void InitStream(StreamProviderOptions options)
    {
        // The relative path for the current resource (e.g., "images/img1.png").
        string relativePath = options.DefaultPath;

        // If the path is empty, assign a null stream to avoid errors.
        if (string.IsNullOrEmpty(relativePath))
        {
            options.Stream = Stream.Null;
            return;
        }

        // Combine base output folder with the relative resource path.
        string fullPath = Path.Combine(_basePath, relativePath);

        // Ensure the directory for the resource exists.
        string directory = Path.GetDirectoryName(fullPath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Create a writable file stream for the resource.
        options.Stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
    }

    // Called by Aspose.Diagram after the resource has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        // Dispose the stream to release file handles and prevent memory leaks.
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
        // Expect two arguments: input folder and output folder.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Validate input folder.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Get all Visio files (VSDX) in the input folder.
        string[] visioFiles = Directory.GetFiles(inputFolder, "*.vsdx");

        foreach (string visioPath in visioFiles)
        {
            // Guard against missing file (should not happen after GetFiles, but added for safety).
            if (!File.Exists(visioPath))
            {
                Console.Error.WriteLine($"File not found: {visioPath}");
                continue;
            }

            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(visioPath);

                // Prepare HTML save options with a custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // The provider will handle creation and disposal of all resource streams.
                    StreamProvider = new MyStreamProvider(outputFolder)
                };

                // Determine output HTML file path.
                string htmlFileName = Path.GetFileNameWithoutExtension(visioPath) + ".html";
                string htmlOutputPath = Path.Combine(outputFolder, htmlFileName);

                // Save the diagram as HTML.
                diagram.Save(htmlOutputPath, htmlOptions);
                Console.WriteLine($"Successfully converted: {visioPath} -> {htmlOutputPath}");
            }
            catch (Exception ex)
            {
                // Report any errors during conversion.
                Console.Error.WriteLine($"Error processing '{visioPath}': {ex.Message}");
            }
        }
    }
}