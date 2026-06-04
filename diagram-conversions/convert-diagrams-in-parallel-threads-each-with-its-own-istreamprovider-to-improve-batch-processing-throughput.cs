using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramBatchExport
{
    // Implements IStreamProvider to write HTML resources (images, CSS, etc.) to files.
    public class FileStreamProvider : IStreamProvider
    {
        private readonly string _basePath;

        public FileStreamProvider(string basePath)
        {
            _basePath = basePath;
        }

        // Called by Aspose.Diagram when a resource stream is needed.
        public void InitStream(StreamProviderOptions options)
        {
            // Combine base folder with the default path supplied by the library.
            string fullPath = Path.Combine(_basePath, options.DefaultPath);
            string directory = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            // Assign a FileStream to the options so the library can write the resource.
            options.Stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
        }

        // Called after the library finishes writing to the stream.
        public void CloseStream(StreamProviderOptions options)
        {
            options.Stream?.Close();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input folder containing Visio files (e.g., .vsdx, .vdx, .vsd).
                string inputFolder = @"C:\Diagrams\Input";
                // Output folder where HTML exports will be placed.
                string outputFolder = @"C:\Diagrams\Output";

                // Gather all supported Visio files.
                List<string> diagramFiles = new List<string>();
                diagramFiles.AddRange(Directory.GetFiles(inputFolder, "*.vsdx"));
                diagramFiles.AddRange(Directory.GetFiles(inputFolder, "*.vdx"));
                diagramFiles.AddRange(Directory.GetFiles(inputFolder, "*.vsd"));
                diagramFiles.AddRange(Directory.GetFiles(inputFolder, "*.vsx"));
                diagramFiles.AddRange(Directory.GetFiles(inputFolder, "*.vtx"));
                diagramFiles.AddRange(Directory.GetFiles(inputFolder, "*.vssx"));
                diagramFiles.AddRange(Directory.GetFiles(inputFolder, "*.vstx"));
                diagramFiles.AddRange(Directory.GetFiles(inputFolder, "*.vsdm"));
                diagramFiles.AddRange(Directory.GetFiles(inputFolder, "*.vssm"));
                diagramFiles.AddRange(Directory.GetFiles(inputFolder, "*.vstm"));
                diagramFiles.AddRange(Directory.GetFiles(inputFolder, "*.html")); // optional source HTML

                // Process each diagram in parallel, each thread gets its own IStreamProvider.
                Parallel.ForEach(diagramFiles, diagramPath =>
                {
                    try
                    {
                        // Determine output HTML file name.
                        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(diagramPath);
                        string htmlOutputPath = Path.Combine(outputFolder, fileNameWithoutExt + ".html");
                        string resourceFolder = Path.Combine(outputFolder, fileNameWithoutExt + "_files");

                        // Ensure the resource folder exists.
                        if (!Directory.Exists(resourceFolder))
                            Directory.CreateDirectory(resourceFolder);

                        // Load the diagram.
                        using (Diagram diagram = new Diagram(diagramPath))
                        {
                            // Configure HTML export options.
                            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                            htmlOptions.StreamProvider = new FileStreamProvider(resourceFolder);
                            // Optional: set a default font to avoid missing glyphs.
                            htmlOptions.DefaultFont = "Arial";

                            // Export to HTML.
                            diagram.Save(htmlOutputPath, htmlOptions);
                        }

                        Console.WriteLine($"Successfully exported: {diagramPath}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing {diagramPath}: {ex.Message}");
                    }
                });

                Console.WriteLine("Batch export completed.");

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }
}