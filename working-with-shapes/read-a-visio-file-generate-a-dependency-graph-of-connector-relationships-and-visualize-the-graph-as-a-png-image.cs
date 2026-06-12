using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args.Length > 1 ? args[1] : "graph.png";

        try
        {
            // Load the source diagram
            Diagram srcDiagram = new Diagram(inputPath);

            // Build a directed graph: node ID -> list of target node IDs
            Dictionary<long, List<long>> graph = new Dictionary<long, List<long>>();

            foreach (Page page in srcDiagram.Pages)
            {
                foreach (Connect conn in page.Connects)
                {
                    long fromId = conn.FromSheet;
                    long toId = conn.ToSheet;

                    Shape fromShape = page.Shapes.GetShape(fromId);
                    Shape toShape = page.Shapes.GetShape(toId);
                    if (fromShape == null || toShape == null) continue;
                    if (fromShape.Del == BOOL.True || toShape.Del == BOOL.True) continue;

                    if (!graph.ContainsKey(fromId))
                        graph[fromId] = new List<long>();
                    graph[fromId].Add(toId);

                    if (!graph.ContainsKey(toId))
                        graph[toId] = new List<long>();
                }
            }

            // Create a new diagram to visualize the graph
            Diagram visDiagram = new Diagram();
            Page visPage = new Page();
            visDiagram.Pages.Add(visPage);

            // Simple grid layout for node shapes
            Dictionary<long, long> nodeShapeIds = new Dictionary<long, long>();
            double startX = 2.0;
            double startY = 2.0;
            double stepX = 4.0;
            double stepY = 3.0;
            int columns = 5;
            int index = 0;

            foreach (long nodeId in graph.Keys)
            {
                int col = index % columns;
                int row = index / columns;
                double pinX = startX + col * stepX;
                double pinY = startY + row * stepY;

                // Draw a rectangle representing the node
                long shapeId = visPage.DrawRectangle(pinX, pinY, 2.0, 1.0);
                Shape shape = visPage.Shapes.GetShape(shapeId);
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt(nodeId.ToString()));
                nodeShapeIds[nodeId] = shapeId;

                index++;
            }

            // Add connectors between node shapes
            foreach (var kvp in graph)
            {
                long fromNodeId = kvp.Key;
                foreach (long toNodeId in kvp.Value)
                {
                    long fromShapeId = nodeShapeIds[fromNodeId];
                    long toShapeId = nodeShapeIds[toNodeId];

                    // Create a connector shape
                    long connectorId = visPage.AddShape(0, 0, 0, 0, "Dynamic connector");
                    // Connect the shapes
                    visPage.ConnectShapesViaConnector(
                        fromShapeId, ConnectionPointPlace.Bottom,
                        toShapeId, ConnectionPointPlace.Top,
                        connectorId);

                    // Optional: set routing style
                    Shape connector = visPage.Shapes.GetShape(connectorId);
                    connector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;
                }
            }

            // Save the visualization as PNG
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            visDiagram.Save(outputPath, saveOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}