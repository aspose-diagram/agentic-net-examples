using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportShapesToHtml
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            var diagram = new Diagram("input.vsdx");

            // Index of the page whose shapes will be exported (0‑based)
            int pageIndex = 0; // modify as needed

            // Get the target page
            var page = diagram.Pages[pageIndex];

            // Create output folder for the HTML files
            string outputFolder = "ShapeHtml";
            Directory.CreateDirectory(outputFolder);

            // Export each shape on the page to a separate HTML file
            foreach (Shape shape in page.Shapes)
            {
                // Configure HTML save options to embed CSS and resources in a single file
                var htmlOptions = new HTMLSaveOptions
                {
                    SaveAsSingleFile = true
                };

                // Build a unique file name for the shape (using its ID)
                string htmlPath = Path.Combine(outputFolder, $"Shape_{shape.ID}.html");

                // Generate the HTML for the shape
                shape.ToHTML(htmlPath, htmlOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
