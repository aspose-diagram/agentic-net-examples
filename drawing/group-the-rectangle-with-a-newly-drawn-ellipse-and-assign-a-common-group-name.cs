using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "GroupedDiagram.vsdx";

        // Ensure the output directory exists
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

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Draw a rectangle at (2,2) with width 3 and height 2 inches
            long rectId = page.DrawRectangle(2.0, 2.0, 3.0, 2.0);

            // Draw an ellipse at (6,2) with width 3 and height 2 inches
            long ellipseId = page.DrawEllipse(6.0, 2.0, 3.0, 2.0);

            // Retrieve the shape objects using their IDs
            Shape rectShape = page.Shapes.GetShape(rectId);
            Shape ellipseShape = page.Shapes.GetShape(ellipseId);

            // Group the rectangle and ellipse together
            Shape[] shapesToGroup = new Shape[] { rectShape, ellipseShape };
            Shape groupShape = page.Shapes.Group(shapesToGroup);

            // Assign a common name to the group
            groupShape.Name = "MyRectangleEllipseGroup";

            // Save the diagram to VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}