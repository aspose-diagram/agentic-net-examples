using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Custom stream provider that supplies a read‑only FileStream for existing resources.
    // This avoids duplicating resources when exporting diagrams to HTML.
    public class ReadOnlyResourceStreamProvider : IStreamProvider
    {
        // Called by Aspose.Diagram before a resource stream is needed.
        public void InitStream(StreamProviderOptions options)
        {
            // options.DefaultPath contains the full path of the resource file.
            // Open the file in read‑only mode and assign it to options.Stream.
            if (!string.IsNullOrEmpty(options.DefaultPath) && File.Exists(options.DefaultPath))
            {
                // FileShare.Read allows other processes to read the same file concurrently.
                var stream = new FileStream(options.DefaultPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                options.Stream = stream;
            }
            else
            {
                // If the file does not exist, throw an exception to indicate the problem.
                throw new FileNotFoundException($"Resource file not found: {options.DefaultPath}");
            }
        }

        // Called by Aspose.Diagram after the resource stream is no longer needed.
        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the stream if it was created.
            if (options.Stream != null)
            {
                options.Stream.Dispose();
                options.Stream = null;
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

                // Configure HTML export options.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // Assign the custom stream provider to handle external resources.
                    StreamProvider = new ReadOnlyResourceStreamProvider()
                };

                // Output HTML file path.
                string outputPath = "output.html";

                // Export the diagram to HTML using the configured options.
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