using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Custom stream provider to set a base URL for linked resources in HTML export
    public class CustomStreamProvider : IStreamProvider
    {
        // Called when the export process initializes the stream provider
        public void InitStream(StreamProviderOptions options)
        {
            // Set the custom base URL that will be prefixed to all resource links
            options.CustomPath = "https://mycustomdomain.com/resources/";
        }

        // Called when the export process finishes using the stream provider
        public void CloseStream(StreamProviderOptions options)
        {
            // No resources to clean up in this simple implementation
        }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Path to the source Visio diagram
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Configure HTML export options
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                // Assign the custom stream provider to control resource URLs
                htmlOptions.StreamProvider = new CustomStreamProvider();
                // Optional: do not export hidden pages
                htmlOptions.ExportHiddenPage = false;

                // Path for the exported HTML file
                string outputPath = "output.html";

                // Save the diagram as HTML using the configured options
                diagram.Save(outputPath, htmlOptions);

                Console.WriteLine("Diagram exported to HTML with custom base URL.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}