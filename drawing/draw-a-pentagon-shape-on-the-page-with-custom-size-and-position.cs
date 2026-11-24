using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "PentagonDiagram.vsdx";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first page (default page)
            Page page = diagram.Pages[0];

            // Define custom size and position for the pentagon
            // We'll draw a pentagon centered at (5,5) with a width and height of 4 units
            double centerX = 5.0;
            double centerY = 5.0;
            double radius = 2.0; // half of the desired width/height

            // Calculate the five vertices of the pentagon (clockwise)
            double[] points = new double[12]; // 5 points + repeat first point to close shape
            for (int i = 0; i < 5; i++)
            {
                double angleDeg = 90 + i * 72; // start at top (90°) and step 72°
                double angleRad = angleDeg * Math.PI / 180.0;
                double x = centerX + radius * Math.Cos(angleRad);
                double y = centerY + radius * Math.Sin(angleRad);
                points[i * 2] = x;
                points[i * 2 + 1] = y;
            }
            // Close the polygon by repeating the first point
            points[10] = points[0];
            points[11] = points[1];

            // Draw the pentagon using the flat double[] overload
            long shapeId = page.DrawPolyline(points);

            // Retrieve the shape object to modify its properties (e.g., fill color)
            Shape pentagon = page.Shapes.GetShape(shapeId);

            // Set a simple fill color (light blue) using the FillForegnd cell
            pentagon.Fill.FillForegnd.Value = "#ADD8E6";

            // Optionally, set line color (black) and line weight
            pentagon.Line.LineColor.Value = "#000000";
            pentagon.Line.LineWeight.Value = 0.02; // inches

            // Save the diagram to VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Pentagon diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}