using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Get the first (default) page
        Page page = diagram.Pages[0];

        // Conversion factor from centimeters to inches (Aspose.Diagram uses inches)
        const double cmToInch = 0.393700787;

        // Desired position and size in centimeters
        double xCm = 5.0;          // X coordinate (center) in cm
        double yCm = 7.0;          // Y coordinate (center) in cm
        double radiusCm = 2.0;     // Circle radius in cm

        // Convert to inches for Aspose.Diagram
        double pinX = xCm * cmToInch;
        double pinY = yCm * cmToInch;
        double diameterInch = radiusCm * 2 * cmToInch;

        // Draw an ellipse (circle) at the specified position
        long shapeId = page.DrawEllipse(pinX, pinY, diameterInch, diameterInch);

        // Retrieve the shape object if further modifications are needed
        Shape circle = page.Shapes.GetShape((int)shapeId);

        // Example: set a fill color for the circle
        circle.Fill.FillForegnd.Value = "#FF0000"; // Red fill

        // Save the diagram to a VSDX file
        diagram.Save("CircleDiagram.vsdx", SaveFileFormat.Vsdx);
    }
}
