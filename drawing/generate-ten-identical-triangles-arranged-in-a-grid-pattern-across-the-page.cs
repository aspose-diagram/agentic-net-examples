using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Output file path
        string outputPath = "TrianglesGrid.vsdx";

        try
        {
            // Create a new empty diagram
            using (Diagram diagram = new Diagram())
            {
                // Get the first (and only) page
                Page page = diagram.Pages[0];

                // Define triangle size
                double triWidth = 2.0;   // inches
                double triHeight = 2.0;  // inches

                // Define grid layout (2 rows x 5 columns = 10 triangles)
                int rows = 2;
                int cols = 5;
                double hSpacing = 3.0; // horizontal spacing between triangles
                double vSpacing = 3.0; // vertical spacing between triangles
                double startX = 1.0;   // left margin
                double startY = 1.0;   // top margin

                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        // Calculate the top‑left corner of the current triangle
                        double x = startX + c * hSpacing;
                        double y = startY + r * vSpacing;

                        // Define the three vertices of the triangle and close the shape by returning to the first point
                        double[] points = new double[]
                        {
                            x, y,                                 // Vertex 1 (bottom‑left)
                            x + triWidth, y,                      // Vertex 2 (bottom‑right)
                            x + triWidth / 2.0, y + triHeight,    // Vertex 3 (top)
                            x, y                                  // Close polygon
                        };

                        // Draw the triangle as a polyline (closed polygon)
                        long shapeId = page.DrawPolyline(points);

                        // Retrieve the shape to apply formatting
                        Shape triangle = page.Shapes.GetShape(shapeId);

                        // Set fill color (light blue) and pattern
                        triangle.Fill.FillPattern.Value = 1; // Solid fill
                        triangle.Fill.FillForegnd.Value = "#ADD8E6";

                        // Set line weight and pattern
                        triangle.Line.LineWeight.Value = 0.03; // 0.03 inches
                        triangle.Line.LinePattern.Value = LinePatternValue.Solid;
                    }
                }

                // Save the diagram to VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}