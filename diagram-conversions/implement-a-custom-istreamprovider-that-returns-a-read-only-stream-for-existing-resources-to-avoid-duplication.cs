using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramExport
{
    // Custom stream provider that supplies a read‑only FileStream for existing resources.
    public class ReadOnlyStreamProvider : IStreamProvider
    {
        // Called by Aspose when a stream is needed.
        public void InitStream(StreamProviderOptions options)
        {
            // Use the default path supplied by Aspose to open a read‑only file stream.
            // The stream is assigned back to the options so Aspose can read from it.
            options.Stream = new FileStream(
                options.DefaultPath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read);
        }

        // Called by Aspose when the stream is no longer required.
        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the stream if it was created.
            options.Stream?.Dispose();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed).
                string inputPath = "sample.vsdx";

                // Load the diagram.
                Diagram diagram = new Diagram(inputPath);

                // Configure HTML export options.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // Assign the custom read‑only stream provider.
                    StreamProvider = new ReadOnlyStreamProvider()
                };

                // Export the diagram to HTML using the save overload that accepts SaveOptions.
                string outputPath = "output.html";
                diagram.Save(outputPath, htmlOptions);

                Console.WriteLine("Diagram exported to HTML successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}