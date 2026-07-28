using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            using (Diagram diagram = new Diagram())
            {
                // Get the first (default) page
                Page page = diagram.Pages[0];

                // Create a background page
                Page backgroundPage = new Page();
                backgroundPage.Background = BOOL.True; // Mark as background

                // Use the same dimensions as the foreground page
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Add a rectangle that covers the entire page
                long bgShapeId = backgroundPage.AddShape(0, 0, pageWidth, pageHeight, "Rectangle");
                Shape bgShape = backgroundPage.Shapes.GetShape(bgShapeId);

                // Apply a diagonal hatch pattern as the watermark
                bgShape.Fill.FillPattern.Value = 25;               // Diagonal lines pattern
                bgShape.Fill.FillForegnd.Value = "#CCCCCC";        // Light gray lines
                bgShape.Fill.FillBkgnd.Value = "#FFFFFF";          // White background

                // Send the shape to the back and make it non‑selectable
                bgShape.SendToBack();
                bgShape.Protection.LockSelect.Value = BOOL.True;

                // Assign the background page to the foreground page
                page.BackPage = backgroundPage;

                // Save the diagram
                diagram.Save("WatermarkDiagram.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
