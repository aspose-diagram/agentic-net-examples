using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace BatchHtmlExport
{
    // Custom stream provider that creates a MemoryStream for each resource
    // and disposes it when the export process signals that the stream is no longer needed.
    public class MyStreamProvider : IStreamProvider
    {
        // Called by Aspose.Diagram before writing a resource.
        public void InitStream(StreamProviderOptions options)
        {
            // Create a new memory stream for the resource.
            // The stream will be assigned to options.Stream and later disposed.
            options.Stream = new MemoryStream();
        }

        // Called by Aspose.Diagram after the resource has been written.
        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the stream to release unmanaged resources and avoid memory leaks.
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
            try
            {

                // Input folder containing Visio files (e.g., .vsdx)
                string inputFolder = @"C:\Visio\Input";
                // Output folder for generated HTML files
                string outputFolder = @"C:\Visio\Output";

                // Ensure output directory exists
                Directory.CreateDirectory(outputFolder);

                // Get all Visio files in the input folder
                string[] visioFiles = Directory.GetFiles(inputFolder, "*.vsdx");

                foreach (string visioPath in visioFiles)
                {
                    // Determine output HTML file path
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(visioPath);
                    string htmlPath = Path.Combine(outputFolder, fileNameWithoutExt + ".html");

                    // Load the diagram inside a using block to ensure proper disposal
                    using (Diagram diagram = new Diagram(visioPath))
                    {
                        // Configure HTML save options with the custom stream provider
                        HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                        {
                            StreamProvider = new MyStreamProvider()
                        };

                        // Save the diagram as HTML
                        diagram.Save(htmlPath, htmlOptions);
                    }

                    // At this point, the diagram and all streams created by MyStreamProvider
                    // have been disposed, preventing memory leaks.
                    Console.WriteLine($"Exported '{visioPath}' to '{htmlPath}'.");
                }

                Console.WriteLine("Batch HTML export completed.");

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }
}