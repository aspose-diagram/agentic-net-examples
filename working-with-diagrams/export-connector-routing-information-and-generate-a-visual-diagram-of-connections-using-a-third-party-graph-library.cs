using System;
using System.IO;
using System.Collections.Generic;
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

        try
        {
            // Load the source diagram inside a using block as required.
            using (Diagram sourceDiagram = new Diagram(inputPath))
            {
                // Create a new diagram that will hold the visual representation of the connections.
                using (Diagram visualDiagram = new Diagram())
                {
                    // Ensure there is at least one page in the new diagram.
                    visualDiagram.Pages.Add(new Page());
                    Page visualPage = visualDiagram.Pages[0];

                    // Mapping from original shape IDs to newly created shape IDs.
                    Dictionary<long, long> idMap = new Dictionary<long, long>();

                    // Simple grid layout parameters.
                    const double startX = 1.0;
                    const double startY = 1.0;
                    const double stepX = 2.5;
                    const double stepY = 2.5;
                    const double shapeWidth = 1.0;
                    const double shapeHeight = 0.5;
                    int columnCount = 5;

                    // Iterate over all pages of the source diagram.
                    for (int pageIndex = 0; pageIndex < sourceDiagram.Pages.Count; pageIndex++)
                    {
                        Page sourcePage = sourceDiagram.Pages[pageIndex];

                        // Iterate over all shapes on the source page.
                        for (int i = 0; i < sourcePage.Shapes.Count; i++)
                        {
                            Shape srcShape = sourcePage.Shapes[i];
                            long srcId = srcShape.ID;

                            // Calculate grid position for the new shape.
                            int row = i / columnCount;
                            int col = i % columnCount;
                            double pinX = startX + col * stepX;
                            double pinY = startY + row * stepY;

                            // Add a rectangle shape to represent the node.
                            long newShapeId = visualPage.AddShape(pinX, pinY, shapeWidth, shapeHeight, "Rectangle", false);
                            Shape newShape = visualPage.Shapes.GetShape(newShapeId);

                            // Set the shape's text to the original shape ID for identification.
                            newShape.Text.Value.Clear();
                            newShape.Text.Value.Add(new Txt(srcId.ToString()));

                            // Store the mapping.
                            idMap[srcId] = newShapeId;
                        }

                        // After all node shapes are created, process connectors.
                        for (int c = 0; c < sourcePage.Connects.Count; c++)
                        {
                            Connect conn = sourcePage.Connects[c];
                            long fromSrcId = conn.FromSheet;
                            long toSrcId = conn.ToSheet;

                            // Ensure both ends have corresponding visual shapes.
                            if (!idMap.ContainsKey(fromSrcId) || !idMap.ContainsKey(toSrcId))
                                continue;

                            long fromVisId = idMap[fromSrcId];
                            long toVisId = idMap[toSrcId];

                            // Add a dynamic connector shape.
                            long connectorId = visualPage.AddShape(0, 0, 0, 0, "Dynamic connector", false);
                            Shape connectorShape = visualPage.Shapes.GetShape(connectorId);

                            // Connect the two node shapes using the connector.
                            visualPage.ConnectShapesViaConnector(
                                fromVisId,
                                ConnectionPointPlace.Bottom,
                                toVisId,
                                ConnectionPointPlace.Top,
                                connectorId);

                            // Set routing style to right‑angle for clarity.
                            connectorShape.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;
                        }

                        // Only process the first page for this example.
                        break;
                    }

                    // Save the visual diagram to the specified output path.
                    visualDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Visual diagram saved to: {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or IO exceptions and report them.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}