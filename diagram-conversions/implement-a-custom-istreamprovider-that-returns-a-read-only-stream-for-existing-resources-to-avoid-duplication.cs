using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Custom stream provider that supplies read‑only streams for existing files.
    // This avoids creating duplicate streams when the HTML exporter requests resources.
    public class ReadOnlyStreamProvider : IStreamProvider
    {
        // Called by Aspose.Diagram when a resource stream is needed.
        public void InitStream(StreamProviderOptions options)
        {
            // The exporter provides the expected file path in options.DefaultPath.
            // Open the file in read‑only mode if it exists; otherwise supply an empty stream.
            if (!string.IsNullOrEmpty(options.DefaultPath) && File.Exists(options.DefaultPath))
            {
                options.Stream = new FileStream(
                    options.DefaultPath,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.Read);
            }
            else
            {
                // Fallback to a null stream to prevent null reference exceptions.
                options.Stream = Stream.Null;
            }
        }

        // Called after the exporter finishes using the stream.
        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the stream if it was created.
            if (options.Stream != null)
            {
                options.Stream.Dispose();
            }
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio diagram.
                string inputPath = "input.vsdx";

                // Load the diagram.
                Diagram diagram = new Diagram(inputPath);

                // Configure HTML export options and assign the custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    StreamProvider = new ReadOnlyStreamProvider()
                };

                // Export the diagram to HTML.
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