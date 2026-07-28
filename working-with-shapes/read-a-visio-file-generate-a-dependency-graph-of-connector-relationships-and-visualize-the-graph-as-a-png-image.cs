using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output PNG file path
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <program> <inputVisioPath> <outputPngPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source Visio diagram
            Diagram sourceDiagram = new Diagram(inputPath);

            // Collect all unique shape IDs (nodes) from the first page
            Page sourcePage = sourceDiagram.Pages[0];
            var nodeIds = new HashSet<long>();
            foreach (Shape shape in sourcePage.Shapes)
            {
                // Exclude connector shapes (1-D) from node collection
                if (!shape.OneD)
                {
                    nodeIds.Add(shape.ID);
                }
            }

            // Collect connector relationships (edges) from the Connects collection
            var edges = new List<(long From, long To)>();
            foreach (Connect connect in sourcePage.Connects)
            {
                // Only consider connections where both ends are non-connector shapes
                if (nodeIds.Contains(connect.FromSheet) && nodeIds.Contains(connect.ToSheet))
                {
                    edges.Add((connect.FromSheet, connect.ToSheet));
                }
            }

            // Create a new diagram to visualize the dependency graph
            Diagram graphDiagram = new Diagram();
            Page graphPage = new Page();
            graphDiagram.Pages.Add(graphPage);

            // Layout nodes in a simple circle
            int nodeCount = nodeIds.Count;
            double centerX = 5.0; // inches
            double centerY = 5.0; // inches
            double radius = 4.0;  // inches
            double nodeWidth = 1.0; // inches
            double nodeHeight = 0.5; // inches

            var nodeIdToShapeId = new Dictionary<long, long>();
            int index = 0;
            foreach (long originalId in nodeIds)
            {
                double angle = 2 * Math.PI * index / nodeCount;
                double pinX = centerX + radius * Math.Cos(angle);
                double pinY = centerY + radius * Math.Sin(angle);

                // Draw a rectangle representing the node
                long shapeId = graphPage.DrawRectangle(pinX, pinY, nodeWidth, nodeHeight);
                Shape nodeShape = graphPage.Shapes.GetShape(shapeId); // retrieve shape by long ID

                // Add label (use original shape ID as simple label)
                nodeShape.Text.Value.Clear();
                nodeShape.Text.Value.Add(new Txt($"ID:{originalId}"));

                nodeIdToShapeId[originalId] = shapeId;
                index++;
            }

            // Add connectors for each edge
            foreach (var edge in edges)
            {
                if (!nodeIdToShapeId.ContainsKey(edge.From) || !nodeIdToShapeId.ContainsKey(edge.To))
                    continue;

                long fromShapeId = nodeIdToShapeId[edge.From];
                long toShapeId = nodeIdToShapeId[edge.To];

                // Create a dynamic connector shape (isCalculate = false)
                long connectorId = graphPage.AddShape(0, 0, "Dynamic connector", false);

                // Connect the two node shapes via the connector
                graphPage.ConnectShapesViaConnector(
                    fromShapeId,
                    ConnectionPointPlace.Bottom,
                    toShapeId,
                    ConnectionPointPlace.Top,
                    connectorId);
            }

            // Export the visualized graph as PNG
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            graphDiagram.Save(outputPath, pngOptions);

            Console.WriteLine($"Graph saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}