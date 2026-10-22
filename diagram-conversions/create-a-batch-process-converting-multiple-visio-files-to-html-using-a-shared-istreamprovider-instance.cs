using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class SharedStreamProvider : IStreamProvider
{
    private readonly string _resourceFolder;

    public SharedStreamProvider(string resourceFolder)
    {
        _resourceFolder = resourceFolder;
        if (!Directory.Exists(_resourceFolder))
        {
            Directory.CreateDirectory(_resourceFolder);
        }
    }

    // Called by Aspose when a resource (e.g., image) needs a stream
    public void InitStream(StreamProviderOptions options)
    {
        // Determine the file name for the resource; DefaultPath provides the suggested name
        string fileName = options.DefaultPath ?? "resource.bin";
        string fullPath = Path.Combine(_resourceFolder, fileName);

        // Ensure the directory exists
        string dir = Path.GetDirectoryName(fullPath);
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        // Assign a writable file stream to the options
        options.Stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);

        // Optional: set a custom base URL for the resources (relative path)
        options.CustomPath = ""; // empty means same folder as HTML
    }

    // Called by Aspose after the resource has been written
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
        // Determine input and output directories
        string inputFolder = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();
        string outputFolder = args.Length > 1 ? args[1] : Path.Combine(Directory.GetCurrentDirectory(), "HtmlOutput");
        string resourceFolder = Path.Combine(outputFolder, "Resources");

        // Guard: input folder must exist
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output and resource folders exist
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }
        if (!Directory.Exists(resourceFolder))
        {
            Directory.CreateDirectory(resourceFolder);
        }

        // Create a shared IStreamProvider instance for all conversions
        IStreamProvider streamProvider = new SharedStreamProvider(resourceFolder);

        // Get all Visio files (VSDX and VSD) in the input folder
        string[] visioFiles = Directory.GetFiles(inputFolder, "*.vsdx");
        string[] visioLegacy = Directory.GetFiles(inputFolder, "*.vsd");
        string[] allFiles = new string[visioFiles.Length + visioLegacy.Length];
        visioFiles.CopyTo(allFiles, 0);
        visioLegacy.CopyTo(allFiles, visioFiles.Length);

        foreach (string visioPath in allFiles)
        {
            // Guard: ensure the file still exists
            if (!File.Exists(visioPath))
            {
                Console.Error.WriteLine($"File not found: {visioPath}");
                continue;
            }

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Prepare HTML save options and assign the shared stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.StreamProvider = streamProvider;

                // Determine output HTML file path
                string htmlFileName = Path.GetFileNameWithoutExtension(visioPath) + ".html";
                string htmlOutputPath = Path.Combine(outputFolder, htmlFileName);

                // Save diagram as HTML
                diagram.Save(htmlOutputPath, htmlOptions);

                Console.WriteLine($"Successfully converted '{visioPath}' to HTML at '{htmlOutputPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{visioPath}': {ex.Message}");
            }
        }
    }
}