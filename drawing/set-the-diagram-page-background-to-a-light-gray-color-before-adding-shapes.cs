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

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Ensure there is at least one foreground page
            Page foregroundPage = diagram.Pages[0];

            // Create a background page
            Page backgroundPage = new Page();
            backgroundPage.Background = BOOL.True;

            // Match background page size to the foreground page size
            double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;
            backgroundPage.PageSheet.PageProps.PageWidth.Value = pageWidth;
            backgroundPage.PageSheet.PageProps.PageHeight.Value = pageHeight;

            // Add the background page to the diagram
            diagram.Pages.Add(backgroundPage);

            // Add a rectangle shape that covers the entire page on the background page
            // PinX and PinY are the center of the shape
            double pinX = pageWidth / 2.0;
            double pinY = pageHeight / 2.0;
            long rectShapeId = backgroundPage.AddShape(pinX, pinY, pageWidth, pageHeight, "Rectangle");
            Shape rectShape = backgroundPage.Shapes.GetShape(rectShapeId);

            // Set the rectangle fill to a light gray color (#D3D3D3) and solid fill pattern
            rectShape.Fill.FillPattern.Value = 1;               // Solid fill
            rectShape.Fill.FillForegnd.Value = "#D3D3D3";       // Light gray

            // Remove any outline by setting line pattern to none
            rectShape.Line.LinePattern.Value = 0;               // No line
            rectShape.Line.LineWeight.Value = 0;

            // Send the rectangle to the back and lock its selection
            rectShape.SendToBack();
            rectShape.Protection.LockSelect.Value = BOOL.True;

            // Link the foreground page to the background page
            foregroundPage.BackPage = backgroundPage;

            // Save the diagram (you can add more shapes before this step if needed)
            string outputPath = "DiagramWithBackground.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
