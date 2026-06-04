using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace VisioBatchHtmlExport
{
    // Shared IStreamProvider implementation for HTML export
    public class SharedStreamProvider : IStreamProvider
    {
        private readonly string _outputFolder;

        public SharedStreamProvider(string outputFolder)
        {
            _outputFolder = outputFolder;
        }

        // Called before a resource stream is created
        public void InitStream(StreamProviderOptions options)
        {
            // Ensure the output folder exists
            Directory.CreateDirectory(_outputFolder);

            // options.DefaultPath contains the relative path for the resource (e.g., images)
            string resourcePath = Path.Combine(_outputFolder, options.DefaultPath ?? Guid.NewGuid().ToString());

            // Create the file stream for the resource
            options.Stream = new FileStream(resourcePath, FileMode.Create, FileAccess.Write);
        }

        // Called after the resource stream is closed
        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the stream if it was created
            options.Stream?.Dispose();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Input folder containing Visio files
            string inputFolder = @"C:\VisioFiles";

            // Output folder for generated HTML files
            string outputFolder = @"C:\VisioHtmlOutput";

            // Create a shared stream provider instance
            var streamProvider = new SharedStreamProvider(outputFolder);

            // Get all Visio files (support common extensions)
            string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in visioFiles)
            {
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx")
                {
                    continue; // Skip non-Visio files
                }

                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Prepare HTML save options
                    HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                    {
                        // Assign the shared stream provider
                        StreamProvider = streamProvider,
                        // Optional: export all pages
                        PageCount = int.MaxValue,
                        // Optional: save as a single HTML file
                        SaveAsSingleFile = false,
                        // Optional: set a default font to avoid missing font issues
                        DefaultFont = "Arial"
                    };

                    // Determine output HTML file path
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                    string outputHtmlPath = Path.Combine(outputFolder, fileNameWithoutExt + ".html");

                    // Ensure the output directory exists
                    Directory.CreateDirectory(Path.GetDirectoryName(outputHtmlPath));

                    // Save the diagram as HTML
                    diagram.Save(outputHtmlPath, htmlOptions);

                    Console.WriteLine($"Successfully exported '{filePath}' to HTML.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch conversion completed.");
        }
    }
}