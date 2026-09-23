using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Ensure there is at least one page in the diagram
        if (diagram.Pages.Count == 0)
        {
            diagram.Pages.Add(new Page());
        }

        // Work with the first page
        Page page = diagram.Pages[0];

        // Define rectangle parameters (center at 2,2 inches, size 1x1 inch)
        double pinX = 2.0;
        double pinY = 2.0;
        double width = 1.0;
        double height = 1.0;

        // Add a rectangle shape; DrawRectangle returns the shape ID (long)
        long rectId = page.DrawRectangle(pinX, pinY, width, height);

        // Retrieve the shape object using its ID
        Shape rect = page.Shapes.GetShape(rectId);

        // Define grid spacing: 5 pixels (assuming 96 DPI => 5/96 inches)
        double gridSpacing = 5.0 / 96.0;

        // Snap the shape's center (PinX, PinY) to the nearest grid line
        double snappedPinX = Math.Round(rect.XForm.PinX.Value / gridSpacing) * gridSpacing;
        double snappedPinY = Math.Round(rect.XForm.PinY.Value / gridSpacing) * gridSpacing;

        rect.XForm.PinX.Value = snappedPinX;
        rect.XForm.PinY.Value = snappedPinY;

        // Save the diagram to a VSDX file
        diagram.Save("SnappedRectangle.vsdx", SaveFileFormat.Vsdx);
    }
}
