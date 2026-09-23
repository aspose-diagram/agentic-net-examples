using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Custom stream provider for HTML export
    public class CustomStreamProvider : IStreamProvider
    {
        // Called before a resource stream is requested
        public void InitStream(StreamProviderOptions options)
        {
            // Set a custom base URL for resources (e.g., images) in the generated HTML
            options.CustomPath = "https://example.com/resources/";

            // If you need to supply a stream for a specific resource, assign it here:
            // options.Stream = new FileStream("path/to/resource", FileMode.Open, FileAccess.Read);
        }

        // Called after the resource stream is no longer needed
        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the stream if one was provided
            if (options.Stream != null)
            {
                options.Stream.Dispose();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";

                // Output HTML file path
                string outputPath = "output.html";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Configure HTML save options with the custom stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.StreamProvider = new CustomStreamProvider();

                // Save the diagram as HTML
                diagram.Save(outputPath, htmlOptions);

                Console.WriteLine($"Diagram successfully exported to HTML: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}