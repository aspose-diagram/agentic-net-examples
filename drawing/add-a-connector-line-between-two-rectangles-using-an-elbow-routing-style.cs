using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Get the active page where shapes will be added.
                Page page = diagram.ActivePage;

                // Add the first rectangle shape.
                // Parameters: pinX, pinY, width, height, master name, isCalculate.
                long rect1Id = page.AddShape(1.0, 5.0, 2.0, 1.0, "Rectangle", false);
                Shape rect1 = page.Shapes.GetShape(rect1Id);

                // Add the second rectangle shape.
                long rect2Id = page.AddShape(5.0, 5.0, 2.0, 1.0, "Rectangle", false);
                Shape rect2 = page.Shapes.GetShape(rect2Id);

                // Add a dynamic connector shape.
                // Using the overload that takes pinX, pinY, master name, isCalculate.
                long connectorId = page.AddShape(0.0, 0.0, "Dynamic connector", false);
                Shape connector = page.Shapes.GetShape(connectorId);

                // Set the connector routing style to elbow (right‑angle).
                connector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

                // Connect the two rectangles via the connector.
                // Connect from the right side of the first rectangle to the left side of the second.
                page.ConnectShapesViaConnector(
                    rect1Id,
                    ConnectionPointPlace.Right,
                    rect2Id,
                    ConnectionPointPlace.Left,
                    connectorId);

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}