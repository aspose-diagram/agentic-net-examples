using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define the output file path
        string outputPath = "GroupedDiagram.vsdx";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Create a new empty diagram
            using (Diagram diagram = new Diagram())
            {
                // Get the first (default) page
                Page page = diagram.Pages[0];

                // ------------------------------------------------------------
                // Add a pentagon shape using DrawPolyline.
                // The points are defined in a flat double array (x1, y1, x2, y2, ...).
                // The last point repeats the first to close the polygon.
                // ------------------------------------------------------------
                double[] pentagonPoints = new double[]
                {
                    5.0, 3.0,   // Point 1
                    6.9, 4.5,   // Point 2
                    6.1, 7.0,   // Point 3
                    3.9, 7.0,   // Point 4
                    3.1, 4.5,   // Point 5
                    5.0, 3.0    // Close back to Point 1
                };
                long pentagonId = page.DrawPolyline(pentagonPoints);
                Shape pentagonShape = page.Shapes.GetShape(pentagonId);

                // ------------------------------------------------------------
                // Add a square shape using DrawRectangle.
                // Parameters: pinX, pinY (center), width, height.
                // ------------------------------------------------------------
                long squareId = page.DrawRectangle(2.0, 2.0, 2.0, 2.0);
                Shape squareShape = page.Shapes.GetShape(squareId);

                // ------------------------------------------------------------
                // Group the pentagon and square together.
                // The Group method expects an array of Shape objects.
                // ------------------------------------------------------------
                Shape groupShape = page.Shapes.Group(new Shape[] { pentagonShape, squareShape });

                // Assign a friendly name to the group (NameU is a plain string property)
                if (groupShape != null)
                {
                    groupShape.NameU = "PentagonSquareGroup";
                }

                // Save the diagram with the grouped shapes
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}