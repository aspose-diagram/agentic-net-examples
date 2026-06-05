using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
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

        // Define rectangle size (in inches)
        double rectWidth = 2.0;   // 2 inches wide
        double rectHeight = 1.0;  // 1 inch high

        // Initial position (center of rectangle) – arbitrary values
        double initialPinX = 3.0;
        double initialPinY = 4.0;

        // Draw the rectangle; returns the shape ID
        long shapeId = page.DrawRectangle(initialPinX, initialPinY, rectWidth, rectHeight);

        // Retrieve the shape object for further manipulation
        Shape rect = page.Shapes.GetShape(shapeId);

        // Grid spacing: 5 pixels. Convert to inches (96 DPI is the default)
        const double pixelsPerInch = 96.0;
        double gridSizeInches = 5.0 / pixelsPerInch; // ≈0.0520833 inches

        // Snap PinX to the nearest grid line
        double currentPinX = rect.XForm.PinX.Value;
        double snappedPinX = Math.Round(currentPinX / gridSizeInches) * gridSizeInches;
        rect.XForm.PinX.Value = snappedPinX;

        // Snap PinY to the nearest grid line
        double currentPinY = rect.XForm.PinY.Value;
        double snappedPinY = Math.Round(currentPinY / gridSizeInches) * gridSizeInches;
        rect.XForm.PinY.Value = snappedPinY;

        // Save the diagram to a VSDX file
        diagram.Save("SnappedRectangle.vsdx", SaveFileFormat.Vsdx);
    }
}
