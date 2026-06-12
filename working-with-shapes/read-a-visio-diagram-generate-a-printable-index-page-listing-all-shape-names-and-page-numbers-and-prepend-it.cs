using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioIndexGenerator
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            const string inputPath = "input.vsdx";
            const string outputPath = "output.vsdx";

            // Load the existing diagram
            using (var diagram = new Diagram(inputPath))
            {
                // Create a new page that will hold the index
                var indexPage = new Page();
                indexPage.Name = "Index";

                // Add the index page to the document and move it to the first position
                diagram.Pages.Add(indexPage);
                indexPage.MoveTo(0); // ensures the index page is the first page

                // Prepare layout parameters for the index entries
                double startX = 1.0;          // inches from the left edge
                double startY = 1.0;          // inches from the top edge
                double lineHeight = 0.25;     // vertical spacing between entries
                double entryWidth = 5.0;      // width of the text shape
                double entryHeight = 0.2;     // height of the text shape

                double currentY = startY;

                // Iterate through all pages (skip the newly added index page)
                for (int p = 1; p < diagram.Pages.Count; p++) // pages are 0‑based; index page is at 0
                {
                    var page = diagram.Pages[p];
                    string pageName = page.Name;

                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the shape's universal name; fallback to ID if name is empty
                        string shapeName = !string.IsNullOrEmpty(shape.NameU) ? shape.NameU : $"Shape_{shape.ID}";

                        // Build the index line: "ShapeName - Page PageName"
                        string entryText = $"{shapeName} - Page {pageName}";

                        // Add a text shape to the index page
                        indexPage.AddText(startX, currentY, entryWidth, entryHeight, entryText);

                        // Move to the next line
                        currentY += lineHeight;
                    }
                }

                // Save the modified diagram back to a file (VDX format)
                diagram.Save(outputPath, SaveFileFormat.Vdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
