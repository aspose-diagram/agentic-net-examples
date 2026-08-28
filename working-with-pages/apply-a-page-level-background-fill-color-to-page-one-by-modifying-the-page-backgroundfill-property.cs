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

            // Create a new diagram (empty document)
            using (Diagram diagram = new Diagram())
            {
                // Ensure there is at least one foreground page
                Page foregroundPage = diagram.Pages[0];

                // Create a background page
                Page backgroundPage = new Page();
                backgroundPage.Background = BOOL.True; // Mark as background page

                // Copy page dimensions from the foreground page
                backgroundPage.PageSheet.PageProps.PageWidth.Value = foregroundPage.PageSheet.PageProps.PageWidth.Value;
                backgroundPage.PageSheet.PageProps.PageHeight.Value = foregroundPage.PageSheet.PageProps.PageHeight.Value;

                // Add a rectangle shape that spans the entire page
                // PinX and PinY are the center of the shape; for a full‑page rectangle,
                // they are set to half the width/height.
                double pageWidth = backgroundPage.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = backgroundPage.PageSheet.PageProps.PageHeight.Value;
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Add the rectangle shape using the built‑in "Rectangle" master
                long rectShapeId = backgroundPage.AddShape(pinX, pinY, pageWidth, pageHeight, "Rectangle");
                Shape rectShape = backgroundPage.Shapes.GetShape(rectShapeId);

                // Apply solid fill with a light blue color
                rectShape.Fill.FillPattern.Value = 1;               // Solid fill
                rectShape.Fill.FillForegnd.Value = "#ADD8E6";       // Light blue (hex)

                // Remove the outline
                rectShape.Line.LinePattern.Value = 0;               // No line

                // Send the rectangle to the back so other shapes appear above it
                rectShape.SendToBack();

                // Attach the background page to the foreground page
                foregroundPage.BackPage = backgroundPage;

                // Add the background page to the diagram's page collection
                diagram.Pages.Add(backgroundPage);

                // Save the diagram to a VSDX file
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
