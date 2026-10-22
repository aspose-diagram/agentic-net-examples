using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "pentagon.vsdx";

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                // Add a new page if none exist
                diagram.Pages.Add(new Page());
            }

            // Get the first page
            Page page = diagram.Pages[0];

            // Define pentagon vertices (custom size and position)
            // Center at (5,5) inches, radius 2 inches
            double centerX = 5.0;
            double centerY = 5.0;
            double radius = 2.0;
            double angleStep = 72.0 * Math.PI / 180.0; // 72 degrees in radians

            // Build a flat double array of points (x1, y1, x2, y2, ..., xn, yn, x1, y1)
            double[] points = new double[12]; // 5 points + repeat first = 6 points * 2 coordinates
            for (int i = 0; i < 5; i++)
            {
                double angle = -Math.PI / 2 + i * angleStep; // start at top
                points[i * 2] = centerX + radius * Math.Cos(angle);
                points[i * 2 + 1] = centerY + radius * Math.Sin(angle);
            }
            // Close the polygon by repeating the first point
            points[10] = points[0];
            points[11] = points[1];

            // Draw the pentagon shape on the page
            long shapeId = page.DrawPolyline(points);

            // Retrieve the shape to optionally set additional properties (e.g., text)
            Shape pentagonShape = page.Shapes.GetShape(shapeId);
            // Example: add a label inside the pentagon
            pentagonShape.Text.Value.Clear();
            pentagonShape.Text.Value.Add(new Txt("Pentagon"));

            // Save the diagram to a VSDX file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Pentagon diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}