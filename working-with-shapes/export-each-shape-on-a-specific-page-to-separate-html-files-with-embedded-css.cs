using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Index of the page whose shapes will be exported (0‑based)
            int pageIndex = 0; // adjust as needed

            // Get the target page
            Page page = diagram.Pages[pageIndex];

            // Export each shape on the page to its own HTML file
            foreach (Shape shape in page.Shapes)
            {
                // Create a unique file name for the shape (using its ID)
                string htmlFileName = $"Shape_{shape.ID}.html";

                // Configure HTML save options to embed all resources (CSS, images) in a single file
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    SaveAsSingleFile = true   // embeds CSS directly into the HTML
                };

                // Generate the HTML for the shape
                shape.ToHTML(htmlFileName, htmlOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
