using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Ensure there is at least one page to work with
        if (diagram.Pages.Count == 0)
        {
            diagram.Pages.Add(new Page());
        }

        // Use the first page
        Page page = diagram.Pages[0];

        // Define a rectangle (pin position, width, height) in inches
        double initialPinX = 1.2;   // inches
        double initialPinY = 2.3;   // inches
        double rectWidth = 2.0;    // inches
        double rectHeight = 1.0;   // inches

        // Draw the rectangle; returns the shape ID
        long rectShapeId = page.DrawRectangle(initialPinX, initialPinY, rectWidth, rectHeight);

        // Retrieve the shape object for further manipulation
        Shape rectShape = page.Shapes.GetShape(rectShapeId);

        // Grid spacing: 5 pixels. Visio uses 96 DPI by default.
        const double dpi = 96.0;
        const double gridSpacingPixels = 5.0;
        double gridSpacingInches = gridSpacingPixels / dpi; // ≈0.0520833 inches

        // Snap PinX to the nearest grid line
        double originalPinX = rectShape.XForm.PinX.Value;
        double snappedPinX = Math.Round(originalPinX / gridSpacingInches) * gridSpacingInches;
        rectShape.XForm.PinX.Value = snappedPinX;

        // Snap PinY to the nearest grid line
        double originalPinY = rectShape.XForm.PinY.Value;
        double snappedPinY = Math.Round(originalPinY / gridSpacingInches) * gridSpacingInches;
        rectShape.XForm.PinY.Value = snappedPinY;

        // Save the diagram to a VSDX file
        diagram.Save("SnappedRectangle.vsdx", SaveFileFormat.Vsdx);
    }
}
