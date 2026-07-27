using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Ensure there is at least one page
        if (diagram.Pages.Count == 0)
        {
            diagram.Pages.Add(new Page());
        }

        // Use the first page
        Page page = diagram.Pages[0];

        // Define rectangle size (in inches)
        double rectWidth = 2.0;   // 2 inches wide
        double rectHeight = 1.0;  // 1 inch high

        // Initial position (pin point) in inches
        double initialPinX = 1.2;
        double initialPinY = 2.3;

        // Add a rectangle shape to the page
        long shapeId = page.DrawRectangle(initialPinX, initialPinY, rectWidth, rectHeight);

        // Retrieve the shape object for manipulation
        Shape rectangle = page.Shapes.GetShape(shapeId);

        // Grid spacing: 5 pixels. Assuming 96 DPI, convert to inches.
        const double dpi = 96.0;
        double gridSpacingInches = 5.0 / dpi; // ≈0.0520833 inches

        // Snap PinX to the nearest grid line
        double currentPinX = rectangle.XForm.PinX.Value;
        double snappedPinX = Math.Round(currentPinX / gridSpacingInches) * gridSpacingInches;
        rectangle.XForm.PinX.Value = snappedPinX;

        // Snap PinY to the nearest grid line
        double currentPinY = rectangle.XForm.PinY.Value;
        double snappedPinY = Math.Round(currentPinY / gridSpacingInches) * gridSpacingInches;
        rectangle.XForm.PinY.Value = snappedPinY;

        // Save the diagram to a VSDX file
        diagram.Save("SnappedRectangle.vsdx", SaveFileFormat.Vsdx);
    }
}
