using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CustomStreamProvider : IStreamProvider
{
    // Base folder where HTML resources (images, etc.) will be saved
    private readonly string _baseOutputFolder;

    public CustomStreamProvider(string baseOutputFolder)
    {
        _baseOutputFolder = baseOutputFolder;
    }

    // Called by Aspose.Diagram when a resource stream is needed
    public void InitStream(StreamProviderOptions options)
    {
        // Combine the base folder with the default relative path provided by Aspose
        string resourcePath = Path.Combine(_baseOutputFolder, options.DefaultPath);

        // Ensure the directory for the resource exists
        string directory = Path.GetDirectoryName(resourcePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Create a file stream for writing the resource
        options.Stream = new FileStream(resourcePath, FileMode.Create, FileAccess.Write);
    }

    // Called by Aspose.Diagram after the resource has been written
    public void CloseStream(StreamProviderOptions options)
    {
        // Safely close the stream if it was created
        options.Stream?.Close();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input folder and output folder
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: BatchVisioToHtml <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Guard: ensure input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists (create if necessary)
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Create a shared IStreamProvider instance for all HTML exports
        IStreamProvider streamProvider = new CustomStreamProvider(outputFolder);

        // Get all Visio files in the input folder (common extensions)
        string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in visioFiles)
        {
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx" &&
                extension != ".vsx" && extension != ".vtx")
            {
                // Skip non‑Visio files
                continue;
            }

            // Guard: ensure the file still exists before processing
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Determine output HTML file path
                string outputHtmlPath = Path.Combine(outputFolder,
                    Path.GetFileNameWithoutExtension(filePath) + ".html");

                // Configure HTML save options with the shared stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.StreamProvider = streamProvider;

                // Load the Visio diagram inside a using block
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Save the diagram as HTML using the configured options
                    diagram.Save(outputHtmlPath, htmlOptions);
                }

                Console.WriteLine($"Successfully converted: {filePath} -> {outputHtmlPath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during conversion
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}