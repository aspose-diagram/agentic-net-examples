using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (assumes at least one page exists)
            Page page = diagram.Pages[0];

            // Add first rectangle shape
            // Parameters: pinX, pinY, width, height, master name, isCalculate
            long rect1Id = page.AddShape(2.0, 2.0, 2.0, 2.0, "Rectangle", false);
            Shape rect1 = page.Shapes.GetShape(rect1Id);

            // Add second rectangle shape
            long rect2Id = page.AddShape(5.0, 5.0, 2.0, 2.0, "Rectangle", false);
            Shape rect2 = page.Shapes.GetShape(rect2Id);

            // Add a dynamic connector shape (initially zero size)
            long connectorId = page.AddShape(0.0, 0.0, 0.0, 0.0, "Dynamic connector", false);
            Shape connector = page.Shapes.GetShape(connectorId);

            // Configure the connector's line style to a dashed pattern
            connector.Line.LinePattern.Value = LinePatternValue.Dash;

            // Connect the two rectangles via the connector
            // Use bottom of the first shape and top of the second shape as connection points
            page.ConnectShapesViaConnector(
                rect1Id,
                ConnectionPointPlace.Bottom,
                rect2Id,
                ConnectionPointPlace.Top,
                connectorId);

            // Save the modified diagram to the output file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}