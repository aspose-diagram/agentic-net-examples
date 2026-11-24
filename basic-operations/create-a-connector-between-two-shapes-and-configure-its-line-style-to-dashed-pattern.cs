using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Determine output file path (optional command‑line argument)
        string outputPath = args.Length > 0 ? args[0] : "output.vsdx";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Use the first (default) page
            Page page = diagram.Pages[0];

            // Add the first rectangle shape
            // Parameters: pinX, pinY, width, height, master name, isCalculate (bool)
            long rect1Id = page.AddShape(2.0, 2.0, 2.0, 1.0, "Rectangle", false);
            Shape rect1 = page.Shapes.GetShape(rect1Id);

            // Add the second rectangle shape
            long rect2Id = page.AddShape(6.0, 2.0, 2.0, 1.0, "Rectangle", false);
            Shape rect2 = page.Shapes.GetShape(rect2Id);

            // Add a dynamic connector shape (4‑argument overload)
            long connectorId = page.AddShape(4.0, 4.0, "Dynamic connector", false);
            Shape connector = page.Shapes.GetShape(connectorId);

            // Set the connector's line style to a dashed pattern
            connector.Line.LinePattern.Value = LinePatternValue.Dash;

            // Connect the two rectangles via the connector
            page.ConnectShapesViaConnector(
                rect1Id,
                ConnectionPointPlace.Right,
                rect2Id,
                ConnectionPointPlace.Left,
                connectorId);

            // Save the diagram to the specified file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}