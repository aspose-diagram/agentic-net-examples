using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Custom stream provider (required for HTML export scenarios)
    public class CustomStreamProvider : IStreamProvider
    {
        public void InitStream(StreamProviderOptions options)
        {
            // Example: set a custom base path for resources (e.g., images, CSS)
            // The DefaultPath property is read‑only; you can use it to infer the target location.
            // Here we simply ensure the directory exists.
            string directory = Path.GetDirectoryName(options.DefaultPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public void CloseStream(StreamProviderOptions options)
        {
            // No special cleanup required for this example.
        }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Configure HTML save options
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // Export hidden pages are disabled by default; set explicitly for clarity
                    ExportHiddenPage = false
                    // NOTE: Aspose.Diagram does NOT provide a property to set a custom CSS class prefix.
                    // The HTML output uses internal class names that cannot be overridden via the API.
                };

                // Assign the custom stream provider (optional, for resource handling)
                htmlOptions.StreamProvider = new CustomStreamProvider();

                // Save the diagram as HTML
                string outputPath = "output.html";
                diagram.Save(outputPath, htmlOptions);

                Console.WriteLine($"Diagram exported to HTML at: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}