using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramBatchProcessing
{
    // Custom stream provider for HTML export.
    // Each thread gets its own instance, ensuring thread‑safety.
    public class MemoryStreamProvider : IStreamProvider
    {
        // Called before a resource stream is created.
        public void InitStream(StreamProviderOptions options)
        {
            // Use a fresh MemoryStream for each resource.
            options.Stream = new MemoryStream();
        }

        // Called after the resource stream is no longer needed.
        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the stream to free memory.
            options.Stream?.Dispose();
        }
    }

    public class Program
    {
        // Entry point.
        public static void Main(string[] args)
        {
            // Input folder containing Visio files.
            string inputFolder = args.Length > 0 ? args[0] : @"C:\VisioFiles";

            // Output folder for HTML files.
            string outputFolder = args.Length > 1 ? args[1] : @"C:\VisioHtmlOutput";

            // Ensure output directory exists.
            Directory.CreateDirectory(outputFolder);

            // Collect all supported Visio files.
            string[] diagramFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
            List<string> supportedFiles = new List<string>();
            foreach (string file in diagramFiles)
            {
                string ext = Path.GetExtension(file).ToLowerInvariant();
                if (ext == ".vsdx" || ext == ".vsd" || ext == ".vdx" || ext == ".vsx" || ext == ".vtx")
                {
                    supportedFiles.Add(file);
                }
            }

            // Process each diagram in parallel.
            Parallel.ForEach(supportedFiles, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, diagramPath =>
            {
                try
                {
                    // Load the diagram.
                    Diagram diagram = new Diagram(diagramPath);

                    // Prepare HTML save options with a dedicated stream provider.
                    HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                    {
                        // Assign a new provider per thread.
                        StreamProvider = new MemoryStreamProvider(),
                        // Optional: export hidden pages = false for faster processing.
                        ExportHiddenPage = false,
                        // Use a single file per diagram for simplicity.
                        SaveAsSingleFile = true
                    };

                    // Determine output HTML file name.
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(diagramPath);
                    string outputPath = Path.Combine(outputFolder, fileNameWithoutExt + ".html");

                    // Save the diagram as HTML.
                    diagram.Save(outputPath, htmlOptions);
                }
                catch (Exception ex)
                {
                    // Log the error to console; in production replace with proper logging.
                    Console.WriteLine($"Error processing '{diagramPath}': {ex.Message}");
                }
            });

            Console.WriteLine("Batch processing completed.");
        }
    }
}