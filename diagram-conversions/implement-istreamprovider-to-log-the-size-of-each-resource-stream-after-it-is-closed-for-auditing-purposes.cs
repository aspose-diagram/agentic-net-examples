using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Custom stream provider that logs the size of each resource after it is closed.
    public class CustomStreamProvider : IStreamProvider
    {
        // Called by Aspose before writing a resource. Provide a stream for the resource.
        public void InitStream(StreamProviderOptions options)
        {
            // Use a memory stream to capture the resource data.
            options.Stream = new MemoryStream();
        }

        // Called by Aspose after the resource has been written and the stream is closed.
        public void CloseStream(StreamProviderOptions options)
        {
            // Ensure the stream exists.
            if (options.Stream != null)
            {
                // Get the length of the written data.
                long size = options.Stream.Length;

                // Log the resource path and its size.
                Console.WriteLine($"Resource '{options.DefaultPath}' size: {size} bytes");

                // Dispose the stream to release resources.
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

                // Path to the source Visio file.
                string inputPath = "input.vsdx";

                // Path for the generated HTML output.
                string outputPath = "output.html";

                // Load the diagram.
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Configure HTML save options with the custom stream provider.
                    HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                    {
                        StreamProvider = new CustomStreamProvider()
                    };

                    // Save the diagram as HTML. Resources (e.g., images) will be processed via the stream provider.
                    diagram.Save(outputPath, htmlOptions);
                }

                Console.WriteLine("HTML export completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}