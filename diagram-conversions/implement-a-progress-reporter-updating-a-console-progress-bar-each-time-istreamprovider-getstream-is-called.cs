using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExportWithProgress
{
    // Custom stream provider that reports progress to the console each time a stream is initialized.
    public class ProgressStreamProvider : IStreamProvider
    {
        private int _callCount = 0;

        // Called by Aspose.Diagram when a new stream is needed during HTML export.
        public void InitStream(StreamProviderOptions options)
        {
            _callCount++;
            // Assign a new memory stream for the requested resource.
            options.Stream = new MemoryStream();

            // Simple console progress indicator.
            Console.WriteLine($"[Progress] InitStream called {_callCount} time(s).");
        }

        // Called by Aspose.Diagram after the stream is no longer needed.
        public void CloseStream(StreamProviderOptions options)
        {
            // Ensure the stream is properly disposed.
            options.Stream?.Dispose();
            Console.WriteLine($"[Progress] CloseStream called for stream #{_callCount}.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input diagram file path (adjust as needed).
                string inputPath = "input.vsdx";

                // Output HTML file path.
                string outputPath = "output.html";

                // Load the diagram.
                Diagram diagram = new Diagram(inputPath);

                // Configure HTML save options and assign the custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    StreamProvider = new ProgressStreamProvider()
                };

                // Save the diagram to HTML, progress will be reported via the stream provider.
                diagram.Save(outputPath, htmlOptions);

                Console.WriteLine("Export completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}