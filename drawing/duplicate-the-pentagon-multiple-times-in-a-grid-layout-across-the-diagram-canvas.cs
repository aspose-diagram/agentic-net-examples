using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Output file path
        string outputPath = "PentagonGrid.vsdx";

        // Guard: ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a single page to the diagram
            diagram.Pages.Add(new Page());
            Page page = diagram.Pages[0];

            // Grid configuration
            int rows = 5;               // number of rows
            int cols = 5;               // number of columns
            double shapeWidth = 1.0;    // width of each pentagon (in inches)
            double shapeHeight = 1.0;   // height of each pentagon (in inches)
            double spacingX = 0.5;      // horizontal spacing between shapes
            double spacingY = 0.5;      // vertical spacing between shapes

            // Radius for the pentagon (inscribed circle)
            double radius = Math.Min(shapeWidth, shapeHeight) / 2.0;

            // Loop through grid positions and draw a pentagon at each cell
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    // Calculate the center point for the current pentagon
                    double centerX = col * (shapeWidth + spacingX) + shapeWidth / 2.0;
                    double centerY = row * (shapeHeight + spacingY) + shapeHeight / 2.0;

                    // Generate the five vertices of the pentagon
                    // The points are stored in a flat double array: x1, y1, x2, y2, ..., xn, yn
                    double[] points = new double[12]; // 5 vertices + closing vertex = 6 points * 2 coordinates
                    double startAngle = -Math.PI / 2; // start at the top vertex
                    for (int i = 0; i < 5; i++)
                    {
                        double angle = startAngle + i * 2 * Math.PI / 5;
                        double x = centerX + radius * Math.Cos(angle);
                        double y = centerY + radius * Math.Sin(angle);
                        points[i * 2] = x;
                        points[i * 2 + 1] = y;
                    }
                    // Close the polygon by repeating the first vertex
                    points[10] = points[0];
                    points[11] = points[1];

                    // Draw the pentagon shape on the page
                    // DrawPolyline returns the shape ID (long)
                    long shapeId = page.DrawPolyline(points);

                    // Optional: retrieve the shape to set additional formatting (e.g., line color)
                    // Shape pentagon = page.Shapes.GetShape(shapeId);
                    // pentagon.Line.LineColor.Value = "#0000FF"; // blue line
                }
            }

            // Save the diagram to VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}