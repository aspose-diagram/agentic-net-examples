using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace BatchDiagramExport
{
    // Custom IStreamProvider implementation for HTML export.
    // Writes each resource (e.g., images, CSS) to a dedicated folder.
    public class FileStreamProvider : IStreamProvider
    {
        private readonly string _outputFolder;

        public FileStreamProvider(string outputFolder)
        {
            _outputFolder = outputFolder;
        }

        // Called by Aspose.Diagram when a new resource stream is needed.
        public void InitStream(StreamProviderOptions options)
        {
            // Ensure the output folder exists.
            Directory.CreateDirectory(_outputFolder);

            // options.DefaultPath provides the relative file name for the resource.
            // Create a FileStream for that file and assign it to options.Stream.
            string filePath = Path.Combine(_outputFolder, options.DefaultPath);
            options.Stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        }

        // Called after the resource has been written.
        public void CloseStream(StreamProviderOptions options)
        {
            // Close the stream if it was created.
            if (options.Stream != null)
            {
                options.Stream.Close();
                options.Stream = null;
            }
        }
    }

    public class Program
    {
        // Entry point.
        public static void Main(string[] args)
        {
            try
            {

                // Input folder containing Visio files.
                string inputFolder = @"C:\Diagrams\Input";

                // Output base folder for HTML exports.
                string outputBaseFolder = @"C:\Diagrams\Output";

                // Get all supported Visio files.
                string[] diagramFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
                if (diagramFiles.Length == 0)
                {
                    Console.WriteLine("No diagram files found in the input folder.");
                    return;
                }

                // Process each file in parallel.
                Parallel.ForEach(diagramFiles, diagramPath =>
                {
                    try
                    {
                        // Load the diagram.
                        Diagram diagram = new Diagram(diagramPath);

                        // Determine output paths.
                        string diagramName = Path.GetFileNameWithoutExtension(diagramPath);
                        string diagramOutputFolder = Path.Combine(outputBaseFolder, diagramName);
                        string htmlOutputPath = Path.Combine(diagramOutputFolder, diagramName + ".html");

                        // Ensure the folder for HTML resources exists.
                        Directory.CreateDirectory(diagramOutputFolder);

                        // Create a stream provider that writes resources into the diagram's folder.
                        IStreamProvider streamProvider = new FileStreamProvider(diagramOutputFolder);

                        // Configure HTML save options.
                        HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                        {
                            ExportHiddenPage = false,
                            IsExportComments = false,
                            StreamProvider = streamProvider
                        };

                        // Save the diagram as HTML.
                        diagram.Save(htmlOutputPath, htmlOptions);

                        Console.WriteLine($"Successfully exported '{diagramPath}' to HTML.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing '{diagramPath}': {ex.Message}");
                    }
                });

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }
}