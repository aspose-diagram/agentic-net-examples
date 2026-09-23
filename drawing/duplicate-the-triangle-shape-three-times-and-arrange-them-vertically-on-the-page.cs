using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new blank diagram
        using (Diagram diagram = new Diagram())
        {
            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Define triangle vertices (in inches)
            double x1 = 2.0, y1 = 2.0;   // Bottom left
            double x2 = 4.0, y2 = 2.0;   // Bottom right
            double x3 = 3.0, y3 = 4.0;   // Top

            // Helper to draw a triangle at a given vertical offset
            void DrawTriangle(double offsetY)
            {
                // Points are defined in a flat double array; repeat the first point to close the shape
                double[] points = new double[]
                {
                    x1, y1 + offsetY,
                    x2, y2 + offsetY,
                    x3, y3 + offsetY,
                    x1, y1 + offsetY
                };

                // Draw the polyline (triangle) and obtain its shape ID
                long shapeId = page.DrawPolyline(points);

                // Retrieve the shape object if further modifications are needed
                Shape shape = page.Shapes.GetShape(shapeId);
                // Example: set a fill color for visibility
                shape.Fill.FillForegnd.Value = "#FFCC00"; // Light orange
                shape.Line.LineColor.Value = "#000000";   // Black border
            }

            // Draw the original triangle
            DrawTriangle(0);

            // Define vertical spacing between triangles (in inches)
            double verticalSpacing = 3.0;

            // Duplicate the triangle three times, arranging them vertically
            for (int i = 1; i <= 3; i++)
            {
                DrawTriangle(i * verticalSpacing);
            }

            // Save the diagram to a VSDX file
            string outputPath = "TriangleDiagram.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }
}
