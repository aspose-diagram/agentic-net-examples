using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Custom stream provider that supplies a read‑only FileStream for each requested resource.
    // This avoids duplicating resources when exporting to HTML.
    public class ReadOnlyResourceStreamProvider : IStreamProvider
    {
        // Called by Aspose.Diagram before a resource stream is needed.
        public void InitStream(StreamProviderOptions options)
        {
            // options.DefaultPath contains the file path of the resource to be exported.
            // Open the file in read‑only mode and assign it to the options.
            if (!string.IsNullOrEmpty(options.DefaultPath) && File.Exists(options.DefaultPath))
            {
                // FileShare.Read allows other processes to read the file simultaneously.
                options.Stream = new FileStream(options.DefaultPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            else
            {
                // If the resource does not exist, throw an exception to indicate the failure.
                throw new FileNotFoundException($"Resource not found: {options.DefaultPath}");
            }
        }

        // Called by Aspose.Diagram after the resource has been processed.
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

                // Load the diagram from file.
                Diagram diagram = new Diagram(inputPath);

                // Configure HTML export options.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // Use the custom stream provider to serve existing resources.
                    StreamProvider = new ReadOnlyResourceStreamProvider(),

                    // Example: export all pages and embed resources as separate files.
                    SaveAsSingleFile = false,
                    PageCount = int.MaxValue
                };

                // Output HTML file path.
                string outputPath = "output.html";

                // Export the diagram to HTML using the configured options.
                diagram.Save(outputPath, htmlOptions);

                Console.WriteLine("HTML export completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}