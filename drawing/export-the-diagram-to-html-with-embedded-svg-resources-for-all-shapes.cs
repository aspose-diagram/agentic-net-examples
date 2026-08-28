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

            // Configure HTML save options to embed all resources (including SVG) into a single file
            var htmlOptions = new HTMLSaveOptions
            {
                // Embed images, SVGs, CSS, etc., into the generated HTML file
                SaveAsSingleFile = true,

                // Optional: set a title for the HTML page
                Title = "Exported Diagram"
            };

            // Save the diagram as HTML with embedded SVG resources for all shapes
            diagram.Save("output.html", htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
