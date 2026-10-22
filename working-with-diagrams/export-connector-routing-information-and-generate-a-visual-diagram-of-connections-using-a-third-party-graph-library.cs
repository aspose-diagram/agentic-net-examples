using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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

        // Load the source diagram inside a try/catch block.
        try
        {
            using (Diagram sourceDiagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page.
                if (sourceDiagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The source diagram contains no pages.");
                    return;
                }

                // Work with the first page of the source diagram.
                Page sourcePage = sourceDiagram.Pages[0];

                // Create a new blank page that will hold the visual representation.
                Page visualPage = new Page();
                sourceDiagram.Pages.Add(visualPage);
                int visualPageIndex = sourceDiagram.Pages.Count - 1;

                // Map original shape IDs to the IDs of the visual placeholder shapes.
                Dictionary<long, long> shapeIdMap = new Dictionary<long, long>();

                // Simple grid layout parameters for the placeholder shapes.
                const int columns = 5;
                const double startX = 1.0;
                const double startY = 1.0;
                const double offsetX = 2.5;
                const double offsetY = 2.0;
                int shapeCounter = 0;

                // Create a rectangle placeholder for each shape on the source page.
                foreach (Shape srcShape in sourcePage.Shapes)
                {
                    // Calculate grid position.
                    double posX = startX + (shapeCounter % columns) * offsetX;
                    double posY = startY + (shapeCounter / columns) * offsetY;

                    // Add a rectangle shape to the visual page.
                    // The last argument (isCalculate) is a plain bool, not BOOL.
                    long newShapeId = visualPage.AddShape(
                        posX,               // pinX
                        posY,               // pinY
                        1.0,                // width
                        0.5,                // height
                        "Rectangle",        // master name (must exist in the stencil)
                        false);             // isCalculate

                    // Retrieve the newly added shape to set its label.
                    Shape visualShape = visualPage.Shapes.GetShape(newShapeId);
                    visualShape.Text.Value.Clear();
                    visualShape.Text.Value.Add(new Txt(srcShape.ID.ToString()));

                    // Store the mapping from source shape ID to visual shape ID.
                    shapeIdMap[srcShape.ID] = newShapeId;

                    shapeCounter++;
                }

                // Iterate over all connectors on the source page and recreate them on the visual page.
                foreach (Connect conn in sourcePage.Connects)
                {
                    long fromOldId = conn.FromSheet;
                    long toOldId = conn.ToSheet;

                    // Output connector information to the console.
                    Console.WriteLine($"Connector: From Shape ID {fromOldId} To Shape ID {toOldId}");

                    // Verify that both endpoint shapes were mapped.
                    if (!shapeIdMap.ContainsKey(fromOldId) || !shapeIdMap.ContainsKey(toOldId))
                    {
                        Console.Error.WriteLine($"Warning: Unable to map connector endpoints for IDs {fromOldId} or {toOldId}.");
                        continue;
                    }

                    // Create a dynamic connector shape.
                    Shape connectorShape = new Shape();
                    long connectorId = sourceDiagram.AddShape(connectorShape, "Dynamic connector", visualPageIndex);

                    // Set the routing style of the connector to right‑angle.
                    Shape visualConnector = visualPage.Shapes.GetShape(connectorId);
                    visualConnector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

                    // Connect the visual placeholder shapes using the connector.
                    visualPage.ConnectShapesViaConnector(
                        shapeIdMap[fromOldId],
                        ConnectionPointPlace.Bottom,
                        shapeIdMap[toOldId],
                        ConnectionPointPlace.Top,
                        connectorId);
                }

                // Save the modified diagram with the visualized connectors.
                try
                {
                    sourceDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved successfully to: {outputPath}");
                }
                catch (Exception saveEx)
                {
                    Console.Error.WriteLine($"Error saving diagram: {saveEx.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}