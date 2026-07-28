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
            string sourcePath = "input.vsdx";
            string destinationPath = "output.vsdx";

            // Load the existing Visio diagram
            using (Diagram diagram = new Diagram(sourcePath))
            {
                // Create a new page that will serve as the index
                Page indexPage = new Page();
                indexPage.Name = "Index";

                // Add the index page to the document
                diagram.Pages.Add(indexPage);

                // Move the newly added page to the first position (prepend)
                // Page indices are zero‑based; MoveTo(0) places it at the front
                indexPage.MoveTo(0);

                // Add a title text shape to the index page
                // Parameters: pinX, pinY, width, height, text
                indexPage.AddText(1.0, 10.0, 5.0, 0.5, "Index of Shapes");

                // Vertical offset for subsequent entries
                double currentY = 9.0;
                double lineHeight = 0.4;
                double startX = 1.0;

                // Iterate through all pages and their shapes to build the index
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the shape's name (use NameU for universal name)
                        string shapeName = shape.NameU;

                        // Compose the index line: "ShapeName - Page N"
                        string line = $"{shapeName} - Page {pageIndex + 1}";

                        // Add the line as a text shape on the index page
                        indexPage.AddText(startX, currentY, 5.0, lineHeight, line);

                        // Move down for the next entry
                        currentY -= lineHeight;
                    }
                }

                // Save the modified diagram back to a file (VDX format)
                diagram.Save(destinationPath, SaveFileFormat.Vdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
