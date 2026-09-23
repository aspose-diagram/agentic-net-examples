using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for SaveFileFormat enum

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Define points for a pentagon (closed by repeating the first point)
            double[] pentagonPoints = new double[]
            {
                2.0, 2.0,   // Point 1
                3.0, 1.0,   // Point 2
                4.0, 2.0,   // Point 3
                3.5, 3.0,   // Point 4
                2.5, 3.0,   // Point 5
                2.0, 2.0    // Close the polygon
            };

            // Add the pentagon shape to the page
            long pentagonId = page.DrawPolyline(pentagonPoints);
            Shape pentagonShape = page.Shapes.GetShape(pentagonId);

            // Add a square shape to the page (pinX, pinY, width, height)
            long squareId = page.DrawRectangle(5.0, 5.0, 2.0, 2.0);
            Shape squareShape = page.Shapes.GetShape(squareId);

            // Group the pentagon and square together
            Shape[] shapesToGroup = new Shape[] { pentagonShape, squareShape };
            Shape groupShape = page.Shapes.Group(shapesToGroup);

            // Set a name for the group (NameU is a string property, not a cell)
            groupShape.NameU = "PentagonSquareGroup";

            // Save the diagram to a VSDX file
            string outputPath = "GroupedShapes.vsdx";
            if (!Directory.Exists(Path.GetDirectoryName(outputPath) ?? ".")) // Guard for output directory
            {
                Console.Error.WriteLine($"Output directory does not exist: {Path.GetDirectoryName(outputPath)}");
                return;
            }
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}