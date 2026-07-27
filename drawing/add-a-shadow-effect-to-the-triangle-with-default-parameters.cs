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

        // Define points for a triangle (closed polygon)
        // Points: (2,2) -> (4,2) -> (3,4) -> back to (2,2)
        double[] trianglePoints = new double[] { 2, 2, 4, 2, 3, 4, 2, 2 };

        // Draw the triangle; returns the shape ID (long)
        long triangleId = page.DrawPolyline(trianglePoints);

        // Retrieve the shape object using the ID
        Shape triangle = page.Shapes.GetShape((int)triangleId);

        // Apply default shadow effect
        // Simple shadow type with default color (black) and default offsets
        triangle.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
        triangle.Fill.ShdwForegnd.Value = "#000000";          // Shadow color: black
        triangle.Fill.ShdwForegndTrans.Value = 0.3;          // 30% transparency
        triangle.Fill.ShapeShdwOffsetX.Value = 0.1;          // Horizontal offset
        triangle.Fill.ShapeShdwOffsetY.Value = 0.1;          // Vertical offset

        // Save the diagram to a VSDX file
        diagram.Save("TriangleWithShadow.vsdx", SaveFileFormat.Vsdx);
    }
}
