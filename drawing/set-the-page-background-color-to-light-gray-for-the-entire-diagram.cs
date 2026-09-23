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

            // Paths for input and output diagrams
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);

            // Determine the maximum existing page ID to assign a unique ID to the new background page
            int maxPageId = 0;
            foreach (Page p in diagram.Pages)
            {
                if (p.ID > maxPageId)
                    maxPageId = p.ID;
            }

            // Create a new background page
            Page backgroundPage = new Page();
            backgroundPage.ID = maxPageId + 1;
            backgroundPage.Name = "Background";
            backgroundPage.Background = BOOL.True; // Mark as a background page

            // Add the background page to the diagram
            diagram.Pages.Add(backgroundPage);

            // Use dimensions from the first foreground page (assumes at least one page exists)
            Page referencePage = diagram.Pages[0];
            double pageWidth = referencePage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = referencePage.PageSheet.PageProps.PageHeight.Value;

            // Add a rectangle shape that spans the entire page
            // PinX and PinY are the center coordinates of the shape
            double pinX = pageWidth / 2.0;
            double pinY = pageHeight / 2.0;
            long rectShapeId = backgroundPage.AddShape(pinX, pinY, pageWidth, pageHeight, "Rectangle", false);
            Shape rectShape = backgroundPage.Shapes.GetShape(rectShapeId);

            // Set the rectangle fill to a solid light gray color
            rectShape.Fill.FillPattern.Value = 1;               // Solid fill
            rectShape.Fill.FillForegnd.Value = "#D3D3D3";       // Light gray (hex)

            // Remove any outline
            rectShape.Line.LinePattern.Value = 0;               // No line

            // Ensure the background shape is behind all other content
            rectShape.SendToBack();

            // Assign the background page to all foreground pages
            foreach (Page page in diagram.Pages)
            {
                if (page.Background != BOOL.True) // Skip the background page itself
                {
                    page.BackPage = backgroundPage;
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
