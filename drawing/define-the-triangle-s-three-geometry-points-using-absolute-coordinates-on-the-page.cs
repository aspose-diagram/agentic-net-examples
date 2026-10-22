using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "Triangle.vsdx";

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Define triangle vertices using absolute coordinates (in inches)
            // Points: (2,2), (5,2), (3.5,5) and close the shape by returning to the first point
            double[] trianglePoints = new double[]
            {
                2.0, 2.0,   // Vertex 1
                5.0, 2.0,   // Vertex 2
                3.5, 5.0,   // Vertex 3
                2.0, 2.0    // Close the polygon
            };

            // Draw the triangle as a polyline shape on the page
            long shapeId = page.DrawPolyline(trianglePoints);

            // Optionally, retrieve the shape to modify its properties (e.g., fill color)
            Shape triangleShape = page.Shapes.GetShape(shapeId);
            // Set fill color to light blue (hex format)
            triangleShape.Fill.FillForegnd.Value = "#ADD8E6";
            // Set line color to black
            triangleShape.Line.LineColor.Value = "#000000";

            // Save the diagram to a VSDX file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Triangle diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}