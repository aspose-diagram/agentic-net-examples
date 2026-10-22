using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: Please provide a directory path as the first argument.");
            return;
        }

        string directoryPath = args[0];

        // Guard: ensure the directory exists.
        if (!Directory.Exists(directoryPath))
        {
            Console.Error.WriteLine($"Error: Directory does not exist: {directoryPath}");
            return;
        }

        // Define Visio file extensions to process.
        string[] extensions = new[] { ".vsdx", ".vsd", ".vsx", ".vtx", ".vss", ".vst", ".vssx", ".vstx", ".vsdm", ".vssm", ".vstm" };

        // Retrieve all Visio files in the specified directory (non‑recursive).
        string[] visioFiles = Directory.GetFiles(directoryPath)
            .Where(f => extensions.Contains(Path.GetExtension(f), StringComparer.OrdinalIgnoreCase))
            .ToArray();

        if (visioFiles.Length == 0)
        {
            Console.WriteLine("No Visio files found in the specified directory.");
            return;
        }

        foreach (string filePath in visioFiles)
        {
            // Guard: ensure the file still exists before processing.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"Warning: File not found (skipping): {filePath}");
                continue;
            }

            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(filePath);

                // Export each page as a separate HTML file.
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Build output HTML file name.
                    string htmlFileName = $"{Path.GetFileNameWithoutExtension(filePath)}_page{i + 1}.html";
                    string htmlOutputPath = Path.Combine(directoryPath, htmlFileName);

                    // Configure HTML save options for a single page.
                    HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                    {
                        PageIndex = i,
                        PageCount = 1,
                        // Assign a custom stream provider to handle embedded resources.
                        StreamProvider = new CustomStreamProvider(htmlOutputPath)
                    };

                    // Save the page to HTML using the configured options.
                    diagram.Save(htmlOutputPath, htmlOptions);
                }

                Console.WriteLine($"Successfully converted: {filePath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}

/// <summary>
/// Custom implementation of IStreamProvider to direct embedded resource streams
/// (e.g., images) to the file system alongside the generated HTML file.
/// </summary>
class CustomStreamProvider : IStreamProvider
{
    private readonly string _htmlFilePath;

    public CustomStreamProvider(string htmlFilePath)
    {
        _htmlFilePath = htmlFilePath;
    }

    // Called by Aspose.Diagram when a resource stream needs to be created.
    public void InitStream(StreamProviderOptions options)
    {
        // Determine the directory of the HTML file.
        string htmlDirectory = Path.GetDirectoryName(_htmlFilePath) ?? string.Empty;

        // Combine the HTML directory with the relative path supplied by Aspose.
        string resourceRelativePath = options.DefaultPath ?? string.Empty;
        string fullResourcePath = Path.Combine(htmlDirectory, resourceRelativePath);

        // Ensure the target directory exists.
        string resourceDir = Path.GetDirectoryName(fullResourcePath);
        if (!string.IsNullOrEmpty(resourceDir) && !Directory.Exists(resourceDir))
        {
            Directory.CreateDirectory(resourceDir);
        }

        // Assign a writable FileStream to the options.
        options.Stream = new FileStream(fullResourcePath, FileMode.Create, FileAccess.Write);
    }

    // Called by Aspose.Diagram after the resource stream has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        // Safely close the stream if it was created.
        options.Stream?.Close();
    }
}