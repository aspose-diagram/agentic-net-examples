using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "output.vsdx";

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first page (a new diagram contains at least one page)
            Page page = diagram.Pages[0];

            // Add first rectangle shape
            long rect1Id = page.AddShape(1.0, 1.0, "Rectangle", false);
            Shape rect1 = page.Shapes.GetShape(rect1Id);

            // Add second rectangle shape
            long rect2Id = page.AddShape(5.0, 1.0, "Rectangle", false);
            Shape rect2 = page.Shapes.GetShape(rect2Id);

            // Add a dynamic connector shape
            long connectorId = page.AddShape(3.0, 1.0, "Dynamic connector", false);
            Shape connector = page.Shapes.GetShape(connectorId);

            // Configure the connector line style to a dashed pattern
            // LinePatternValue.Dash sets the line to a dashed style
            connector.Line.LinePattern.Value = LinePatternValue.Dash;

            // Optionally set line weight (example: 0.02 inches)
            connector.Line.LineWeight.Value = 0.02;

            // Connect the first rectangle to the second rectangle using the connector
            // Use ConnectionPointPlace.Bottom for the source and ConnectionPointPlace.Top for the target
            page.ConnectShapesViaConnector(
                rect1Id,
                ConnectionPointPlace.Bottom,
                rect2Id,
                ConnectionPointPlace.Top,
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