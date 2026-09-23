using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CustomStreamProvider : IStreamProvider
{
    private readonly string _outputPath;

    public CustomStreamProvider(string outputPath)
    {
        _outputPath = outputPath;
    }

    // Called before the HTML export starts writing to the stream
    public void InitStream(StreamProviderOptions options)
    {
        // Create a file stream for the HTML file
        var stream = new FileStream(_outputPath, FileMode.Create, FileAccess.Write);
        options.Stream = stream;
    }

    // Called after the HTML export finishes
    public void CloseStream(StreamProviderOptions options)
    {
        options.Stream?.Close();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Input folder containing Visio files; can be passed as an argument or hard‑coded
        string inputFolder = args.Length > 0 ? args[0] : @"C:\VisioFiles";

        // Collect all supported Visio files (e.g., .vsdx, .vdx, .vsd)
        string[] diagramFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        var supportedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".vsdx", ".vdx", ".vsd", ".vsx", ".vtx", ".vssx", ".vstx", ".vsdm", ".vssm", ".vstm", ".html", ".mmd" };
        var filesToProcess = new List<string>();
        foreach (var file in diagramFiles)
        {
            if (supportedExtensions.Contains(Path.GetExtension(file)))
                filesToProcess.Add(file);
        }

        // Process each diagram in parallel, each thread gets its own IStreamProvider instance
        Parallel.ForEach(filesToProcess, diagramPath =>
        {
            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Determine HTML output path (same name, .html extension)
                string htmlOutputPath = Path.ChangeExtension(diagramPath, ".html");

                // Configure HTML save options with a dedicated stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.StreamProvider = new CustomStreamProvider(htmlOutputPath);
                // Optional: do not export hidden pages
                htmlOptions.ExportHiddenPage = false;

                // Save the diagram as HTML using the custom stream provider
                diagram.Save(htmlOutputPath, htmlOptions);
            }
            catch (Exception ex)
            {
                // Simple error handling – write to console
                Console.WriteLine($"Error processing '{diagramPath}': {ex.Message}");
            }
        });

        Console.WriteLine("Batch conversion completed.");
    }
}