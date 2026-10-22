using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class FileStreamProvider : IStreamProvider
{
    // Base directory where all resource files (images, etc.) will be saved.
    private readonly string _baseDirectory;

    public FileStreamProvider(string baseDirectory)
    {
        _baseDirectory = baseDirectory;
    }

    // Called by Aspose.Diagram when a new resource stream is required.
    public void InitStream(StreamProviderOptions options)
    {
        // Combine the base directory with the default relative path provided by Aspose.
        string fullPath = Path.Combine(_baseDirectory, options.DefaultPath);

        // Ensure the target directory exists.
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
        options.Stream?.Close();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: DiagramBatchConvert <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Guard against missing input folder.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure the output folder exists.
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        // Define supported Visio file extensions.
        string[] supportedExtensions = new[] { ".vsdx", ".vsd", ".vdx", ".vssx", ".vstx", ".mmd" };

        // Collect all diagram files from the input folder.
        List<string> diagramFiles = new List<string>();
        foreach (string ext in supportedExtensions)
        {
            diagramFiles.AddRange(Directory.GetFiles(inputFolder, $"*{ext}", SearchOption.TopDirectoryOnly));
        }

        // Process each diagram in parallel to improve throughput.
        Parallel.ForEach(diagramFiles, diagramPath =>
        {
            // Guard against the file being removed between enumeration and processing.
            if (!File.Exists(diagramPath))
            {
                Console.Error.WriteLine($"File not found (skipped): {diagramPath}");
                return;
            }

            try
            {
                // Load the diagram.
                Diagram diagram = new Diagram(diagramPath);

                // Prepare output sub‑directory for this diagram's HTML resources.
                string diagramName = Path.GetFileNameWithoutExtension(diagramPath);
                string diagramOutputDir = Path.Combine(outputFolder, diagramName);
                if (!Directory.Exists(diagramOutputDir))
                {
                    Directory.CreateDirectory(diagramOutputDir);
                }

                // Configure HTML save options with a dedicated stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.StreamProvider = new FileStreamProvider(diagramOutputDir);

                // Define the output HTML file path.
                string htmlOutputPath = Path.Combine(diagramOutputDir, diagramName + ".html");

                // Save the diagram as HTML using the configured options.
                diagram.Save(htmlOutputPath, htmlOptions);

                // Release resources held by the diagram.
                diagram.Dispose();

                Console.WriteLine($"Successfully converted: {diagramPath} -> {htmlOutputPath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing of this file.
                Console.Error.WriteLine($"Error processing '{diagramPath}': {ex.Message}");
            }
        });
    }
}