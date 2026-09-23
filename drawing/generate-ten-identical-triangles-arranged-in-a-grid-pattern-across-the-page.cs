using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Access the first page
        Page page = diagram.Pages[0];

        // Optional: set page dimensions (in inches)
        page.PageSheet.PageProps.PageWidth.Value = 11;
        page.PageSheet.PageProps.PageHeight.Value = 8.5;

        // Grid parameters
        double startX = 1.5;      // X coordinate of first triangle center
        double startY = 1.5;      // Y coordinate of first triangle center
        double spacingX = 2.5;    // Horizontal distance between triangle centers
        double spacingY = 2.5;    // Vertical distance between triangle centers
        double shapeWidth = 1.0;  // Width of each triangle
        double shapeHeight = 1.0; // Height of each triangle

        // Points defining an equilateral triangle within the shape bounds (closed polygon)
        double[] trianglePoints = new double[] { 0, 0, 1, 0, 0.5, 0.866, 0, 0 };

        // Create 2 rows × 5 columns = 10 triangles
        for (int row = 0; row < 2; row++)
        {
            for (int col = 0; col < 5; col++)
            {
                double pinX = startX + col * spacingX;
                double pinY = startY + row * spacingY;

                // Draw the triangle and obtain its shape ID
                long shapeId = page.DrawPolyline(pinX, pinY, shapeWidth, shapeHeight, trianglePoints);

                // Retrieve the shape to apply formatting
                Shape shape = page.Shapes.GetShape(shapeId);
                shape.Fill.FillForegnd.Value = "#FF0000"; // Red fill
                shape.Fill.FillPattern.Value = 1;        // Solid fill
                shape.Line.LineColor.Value = "#000000"; // Black border
                shape.Line.LineWeight.Value = 0.02;     // Thin line
            }
        }

        // Save the diagram as VSDX
        diagram.Save("Triangles.vsdx", SaveFileFormat.Vsdx);
    }
}
