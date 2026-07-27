using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace HtmlConversionWithCustomStreamProvider
{
    // Custom stream provider for HTML export resources
    public class CustomStreamProvider : IStreamProvider
    {
        // Called when a resource stream is needed during HTML export
        public void InitStream(StreamProviderOptions options)
        {
            // Set a custom base URL for resources (e.g., images, scripts)
            options.CustomPath = "http://example.com/resources/";

            // Provide a stream where the resource will be written.
            // Here we use a MemoryStream as a placeholder.
            options.Stream = new MemoryStream();
        }

        // Called after the resource has been written
        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the stream if it was created
            if (options.Stream != null)
            {
                options.Stream.Dispose();
                options.Stream = null;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio diagram
                string inputPath = "sample.vsdx";

                // Path for the generated HTML file
                string outputPath = "output.html";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Configure HTML save options with the custom stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.StreamProvider = new CustomStreamProvider();

                // Export the diagram to HTML using the configured options
                diagram.Save(outputPath, htmlOptions);

                Console.WriteLine($"Diagram exported to HTML successfully: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}