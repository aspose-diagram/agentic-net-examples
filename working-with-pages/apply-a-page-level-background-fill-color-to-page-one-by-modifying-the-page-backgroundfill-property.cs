using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new diagram (empty Visio document)
        using (Diagram diagram = new Diagram())
        {
            // Ensure there is at least one foreground page
            if (diagram.Pages.Count == 0)
                diagram.Pages.Add(new Page());

            // Get the first (foreground) page
            Page foregroundPage = diagram.Pages[0];

            // Retrieve page dimensions (in inches)
            double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;

            // -----------------------------------------------------------------
            // Create a dedicated background page
            // -----------------------------------------------------------------
            Page backgroundPage = new Page();
            backgroundPage.Name = "BackgroundPage";
            backgroundPage.Background = BOOL.True; // Mark as a background page

            // Set background page size to match the foreground page
            backgroundPage.PageSheet.PageProps.PageWidth.Value = pageWidth;
            backgroundPage.PageSheet.PageProps.PageHeight.Value = pageHeight;

            // Add the background page to the diagram
            diagram.Pages.Add(backgroundPage);

            // -----------------------------------------------------------------
            // Add a rectangle shape that spans the entire background page
            // -----------------------------------------------------------------
            double centerX = pageWidth / 2.0;
            double centerY = pageHeight / 2.0;

            // DrawRectangle(pinX, pinY, width, height) returns the shape ID (long)
            long rectShapeId = backgroundPage.DrawRectangle(centerX, centerY, pageWidth, pageHeight);

            // Retrieve the shape object to modify its fill properties
            Shape rectShape = backgroundPage.Shapes.GetShape(rectShapeId);

            // Set a solid fill pattern (1) and a light blue background color
            rectShape.Fill.FillPattern.Value = 1;               // Solid fill
            rectShape.Fill.FillForegnd.Value = "#ADD8E6";       // Light blue (hex)

            // Remove the outline by setting line pattern to 0 (no line)
            rectShape.Line.LinePattern.Value = 0;

            // -----------------------------------------------------------------
            // Link the background page to the foreground page
            // -----------------------------------------------------------------
            foregroundPage.BackPage = backgroundPage;

            // -----------------------------------------------------------------
            // Save the diagram to a VSDX file
            // -----------------------------------------------------------------
            diagram.Save("OutputWithBackground.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
