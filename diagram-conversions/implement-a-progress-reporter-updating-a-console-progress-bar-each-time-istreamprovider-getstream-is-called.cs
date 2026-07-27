using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExportWithProgress
{
    // Custom stream provider that reports progress to the console each time a stream is initialized.
    public class ConsoleProgressStreamProvider : IStreamProvider
    {
        // Counter for the number of resources processed.
        private static int _resourceCount = 0;

        // Called by Aspose.Diagram before writing a resource (e.g., an image) to a stream.
        public void InitStream(StreamProviderOptions options)
        {
            // Increment the counter and display progress.
            _resourceCount++;
            Console.Write($"\rResources processed: {_resourceCount}");

            // Provide a writable stream for the resource.
            // Using MemoryStream here; Aspose.Diagram will write the resource data into it.
            options.Stream = new MemoryStream();
        }

        // Called after the resource has been written.
        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the stream that was used for the resource.
            options.Stream?.Dispose();
        }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Path to the source Visio diagram.
                string inputPath = "input.vsdx";

                // Path to the output HTML folder (Aspose.Diagram will create files inside this folder).
                string outputFolder = "output_html";

                // Load the diagram.
                Diagram diagram = new Diagram(inputPath);

                // Configure HTML save options and assign the custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // Export all pages.
                    PageCount = int.MaxValue,
                    // Use the custom provider to get progress updates.
                    StreamProvider = new ConsoleProgressStreamProvider()
                };

                // Ensure the output folder exists.
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                // Save the diagram as HTML. Each resource (images, CSS, etc.) will trigger InitStream/CloseStream.
                string htmlFilePath = Path.Combine(outputFolder, "diagram.html");
                diagram.Save(htmlFilePath, htmlOptions);

                // Final newline after progress output.
                Console.WriteLine("\nHTML export completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}