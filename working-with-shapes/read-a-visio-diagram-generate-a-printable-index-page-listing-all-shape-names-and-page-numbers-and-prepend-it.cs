using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioIndexGenerator
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths
            string inputPath = @"C:\Diagrams\source.vsdx";
            string outputPath = @"C:\Diagrams\source_with_index.vsdx";

            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a new page that will serve as the index
            // (Assuming the Pages collection supports Add method)
            Page indexPage = new Page();
            diagram.Pages.Add(indexPage);

            // Set a recognizable name for the index page
            indexPage.Name = "Index";

            // Prepare layout variables for text placement
            double startX = 1.0;   // inches from left
            double startY = 9.0;   // top of the page (Visio default height is 11 inches)
            double lineHeight = 0.3; // vertical spacing between lines
            double textWidth = 8.0;
            double textHeight = 0.25;

            // Write header
            string header = "Shape Index – Name | Page";
            indexPage.AddText(startX, startY, textWidth, textHeight, header);
            startY -= lineHeight; // move down for next line

            // Iterate through all pages and their shapes
            foreach (Page page in diagram.Pages)
            {
                // Skip the index page itself (it may already be in the collection)
                if (page == indexPage) continue;

                // Get the page number (1‑based index in the collection)
                int pageNumber = diagram.Pages.IndexOf(page) + 1;

                foreach (Shape shape in page.Shapes)
                {
                    // Use the shape's Name property; fallback to NameU if needed
                    string shapeName = !string.IsNullOrEmpty(shape.Name) ? shape.Name : shape.NameU;

                    // Build the line text
                    string line = $"{shapeName} | {pageNumber}";

                    // Add the line as a text shape on the index page
                    indexPage.AddText(startX, startY, textWidth, textHeight, line);

                    // Move to next line position
                    startY -= lineHeight;

                    // If we reach the bottom of the page, stop adding (simple safety)
                    if (startY < 0.5) break;
                }

                // Simple page break handling – add extra space between pages
                startY -= lineHeight;
            }

            // Move the index page to the first position in the document
            // (Assuming MoveTo moves the page within the Pages collection)
            indexPage.MoveTo(0);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
