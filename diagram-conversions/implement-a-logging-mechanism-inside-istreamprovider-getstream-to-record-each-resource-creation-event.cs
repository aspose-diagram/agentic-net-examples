using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Custom stream provider that logs each resource creation event.
    public class LoggingStreamProvider : IStreamProvider
    {
        // Called by Aspose.Diagram when a new resource stream is needed.
        public void InitStream(StreamProviderOptions options)
        {
            // Log the resource being created.
            Console.WriteLine($"[Log] InitStream called for resource: {options.DefaultPath}");

            // Provide a memory stream for the resource.
            options.Stream = new MemoryStream();
        }

        // Called by Aspose.Diagram when the resource stream is no longer needed.
        public void CloseStream(StreamProviderOptions options)
        {
            // Log the resource being closed.
            Console.WriteLine($"[Log] CloseStream called for resource: {options.DefaultPath}");

            // Ensure the stream is properly disposed.
            options.Stream?.Dispose();
        }
    }

    class Program
    {
        static void Main()
        {
            // Create an empty diagram.
            Diagram diagram = new Diagram();

            // Configure HTML save options and assign the custom stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                StreamProvider = new LoggingStreamProvider()
            };

            // Save the diagram to HTML. This will trigger InitStream/CloseStream for each resource.
            string outputPath = "output.html";
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine("Diagram saved to HTML with logging.");
        }
    }
}