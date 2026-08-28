using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Custom stream provider that logs the size of each resource stream after it is closed.
    public class AuditingStreamProvider : IStreamProvider
    {
        // Called when a new resource stream is needed.
        public void InitStream(StreamProviderOptions options)
        {
            // Provide a memory stream for the resource.
            options.Stream = new MemoryStream();
        }

        // Called after the resource stream has been written and is about to be closed.
        public void CloseStream(StreamProviderOptions options)
        {
            Stream? stream = options.Stream;
            if (stream != null)
            {
                // Log the size of the resource.
                Console.WriteLine($"Resource '{options.DefaultPath}' closed. Size: {stream.Length} bytes.");

                // Ensure the stream is properly disposed.
                stream.Dispose();
            }
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram.
                using Diagram diagram = new Diagram("input.vsdx");

                // Configure HTML export options and assign the custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    StreamProvider = new AuditingStreamProvider()
                };

                // Export the diagram to HTML. Resources (e.g., images) will be handled by the stream provider.
                diagram.Save("output.html", htmlOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}