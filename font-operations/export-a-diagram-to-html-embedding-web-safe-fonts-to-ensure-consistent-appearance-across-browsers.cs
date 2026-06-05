using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportDiagramToHtml
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            var diagram = new Diagram("input.vsdx");

            // Configure HTML save options
            var htmlOptions = new HTMLSaveOptions
            {
                // Use a web‑safe font to avoid missing‑font issues in browsers
                DefaultFont = "Arial",
                // Embed all resources (images, CSS, fonts) into a single HTML file
                SaveAsSingleFile = true,
                // Optional: set a title for the generated HTML page
                Title = "Exported Diagram"
            };

            // Export the diagram to HTML using the configured options
            diagram.Save("output.html", htmlOptions);

            // Clean up
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
