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

        // Add a new page to the diagram
        Page page = new Page();
        diagram.Pages.Add(page);

        // Define pentagon vertices (clockwise). The first point is repeated at the end to close the shape.
        double[] pentagonPoints = new double[]
        {
            2.0, 1.0,   // Vertex 1
            4.0, 1.0,   // Vertex 2
            5.0, 3.0,   // Vertex 3
            3.0, 5.0,   // Vertex 4
            1.0, 3.0,   // Vertex 5
            2.0, 1.0    // Close polygon (repeat Vertex 1)
        };

        // Draw the pentagon using a polyline. The method returns the shape ID (long).
        long pentagonShapeId = page.DrawPolyline(pentagonPoints);

        // Retrieve the shape object for further processing (if needed)
        Shape pentagonShape = page.Shapes.GetShape(pentagonShapeId);

        // Validation: ensure each vertex lies on integer grid positions (i.e., X and Y are whole numbers)
        const double tolerance = 1e-6;
        for (int i = 0; i < pentagonPoints.Length; i += 2)
        {
            double x = pentagonPoints[i];
            double y = pentagonPoints[i + 1];

            bool xIsInteger = Math.Abs(x - Math.Round(x)) < tolerance;
            bool yIsInteger = Math.Abs(y - Math.Round(y)) < tolerance;

            if (!xIsInteger || !yIsInteger)
            {
                throw new Exception($"Pentagon vertex at index {i / 2 + 1} does not align to the integer grid: ({x}, {y})");
            }
        }

        Console.WriteLine("All pentagon vertices are aligned to the expected integer grid positions.");

        // Save the diagram as a PNG image for visual verification
        ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
        diagram.Save("PentagonDiagram.png", pngOptions);
    }
}
