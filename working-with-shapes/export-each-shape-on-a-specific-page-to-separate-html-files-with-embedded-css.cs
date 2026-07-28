using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportShapesToHtml
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from file
            Diagram diagram = new Diagram("input.vsdx");

            // Index of the page whose shapes will be exported (0‑based)
            int pageIndex = 0;

            // Get the specified page
            Page page = diagram.Pages[pageIndex];

            // Iterate through all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Create HTML save options; default options embed CSS in the output
                HTMLSaveOptions options = new HTMLSaveOptions();

                // Ensure each shape is saved as a single HTML file (optional but keeps CSS inline)
                options.SaveAsSingleFile = true;

                // Build a unique file name for the shape (using its ID)
                string htmlFileName = $"Shape_{shape.ID}.html";

                // Export the shape to an HTML file with the specified options
                shape.ToHTML(htmlFileName, options);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
