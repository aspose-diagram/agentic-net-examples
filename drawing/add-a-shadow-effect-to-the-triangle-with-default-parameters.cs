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
            Shape triangle = page.Shapes.GetShape(triangleId);

            // Apply a simple shadow with default parameters
            // Enable shadow
            triangle.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
            // Default shadow color (black)
            triangle.Fill.ShdwForegnd.Value = "#000000";
            // Default shadow transparency (30% transparent)
            triangle.Fill.ShdwForegndTrans.Value = 0.3;
            // Default shadow offsets
            triangle.Fill.ShapeShdwOffsetX.Value = 0.1;
            triangle.Fill.ShapeShdwOffsetY.Value = 0.1;

            // Save the diagram to a VSDX file
            diagram.Save("TriangleWithShadow.vsdx", SaveFileFormat.Vsdx);
        }
    }