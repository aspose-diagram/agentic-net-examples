using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "output.vsdx";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new blank diagram
            Diagram diagram = new Diagram();

            // Get the first (active) page
            Page page = diagram.Pages[0];

            // Add first rectangle shape
            // Parameters: pinX, pinY, width, height, masterName, isCalculate
            long rect1Id = page.AddShape(2.0, 5.0, 2.0, 1.0, "Rectangle", false);
            Shape rect1 = page.Shapes.GetShape(rect1Id);
            // Optional: set some text on the rectangle
            rect1.Text.Value.Clear();
            rect1.Text.Value.Add(new Txt("Rect 1"));

            // Add second rectangle shape
            long rect2Id = page.AddShape(8.0, 5.0, 2.0, 1.0, "Rectangle", false);
            Shape rect2 = page.Shapes.GetShape(rect2Id);
            rect2.Text.Value.Clear();
            rect2.Text.Value.Add(new Txt("Rect 2"));

            // Add a dynamic connector shape (initially placed at origin)
            long connectorId = page.AddShape(0.0, 0.0, 0.0, 0.0, "Dynamic connector", false);
            Shape connector = page.Shapes.GetShape(connectorId);

            // Set connector routing style to elbow (right-angle)
            connector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;
            // Also set the connector type to right-angle lines
            connector.SetConnectorsType(ConnectorsTypeValue.RightAngle);

            // Connect the two rectangles via the connector
            // Use right side of first rectangle and left side of second rectangle
            page.ConnectShapesViaConnector(
                rect1Id,
                ConnectionPointPlace.Right,
                rect2Id,
                ConnectionPointPlace.Left,
                connectorId);

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