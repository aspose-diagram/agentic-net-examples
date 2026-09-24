using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output PNG file path
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: DiagramDependencyGraph <inputVisioPath> <outputPngPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the source Visio diagram
        Diagram sourceDiagram = new Diagram(inputPath);

        // Use the first page for analysis (adjust if needed)
        Page sourcePage = sourceDiagram.Pages[0];

        // Collect all connector relationships (edges) from the Connects collection
        var edges = new List<(long fromId, long toId)>();
        var nodeIds = new HashSet<long>();

        foreach (Connect conn in sourcePage.Connects)
        {
            long from = conn.FromSheet;
            long to = conn.ToSheet;
            edges.Add((from, to));
            nodeIds.Add(from);
            nodeIds.Add(to);
        }

        // Map each node ID to an index for layout purposes
        var nodeIdList = new List<long>(nodeIds);
        var nodeIndexMap = new Dictionary<long, int>();
        for (int i = 0; i < nodeIdList.Count; i++)
        {
            nodeIndexMap[nodeIdList[i]] = i;
        }

        // Create a new diagram to visualize the dependency graph
        Diagram graphDiagram = new Diagram();
        // Add a blank page
        Page graphPage = new Page();
        graphDiagram.Pages.Add(graphPage);

        // Simple grid layout parameters
        int nodeCount = nodeIdList.Count;
        int columns = (int)Math.Ceiling(Math.Sqrt(nodeCount));
        double spacing = 2.0; // inches between nodes
        double startX = spacing;
        double startY = spacing;
        double nodeWidth = 1.5;
        double nodeHeight = 0.8;

        // Store mapping from original node ID to the newly created shape ID
        var visualNodeShapeIds = new Dictionary<long, long>();

        // Create visual nodes (rectangles with labels)
        for (int i = 0; i < nodeCount; i++)
        {
            long originalId = nodeIdList[i];
            Shape originalShape = sourcePage.Shapes.GetShape(originalId);
            string label = originalShape.NameU ?? $"Node{originalId}";

            int col = i % columns;
            int row = i / columns;
            double pinX = startX + col * spacing;
            double pinY = startY + row * spacing;

            // Draw rectangle representing the node
            long rectShapeId = graphPage.DrawRectangle(pinX, pinY, nodeWidth, nodeHeight);
            Shape rectShape = graphPage.Shapes.GetShape(rectShapeId);
            rectShape.Text.Value.Clear();
            rectShape.Text.Value.Add(new Txt(label));

            visualNodeShapeIds[originalId] = rectShapeId;
        }

        // Create connectors between visual nodes
        foreach (var edge in edges)
        {
            long fromOriginal = edge.fromId;
            long toOriginal = edge.toId;

            // Ensure both nodes exist in the visual map
            if (!visualNodeShapeIds.ContainsKey(fromOriginal) || !visualNodeShapeIds.ContainsKey(toOriginal))
                continue;

            long fromShapeId = visualNodeShapeIds[fromOriginal];
            long toShapeId = visualNodeShapeIds[toOriginal];

            // Add a dynamic connector shape
            long connectorId = graphPage.AddShape(0, 0, 0, 0, "Dynamic connector", false);
            // Connect the shapes using bottom of source to top of target
            graphPage.ConnectShapesViaConnector(
                fromShapeId,
                ConnectionPointPlace.Bottom,
                toShapeId,
                ConnectionPointPlace.Top,
                connectorId);
        }

        // Export the visualized graph to PNG
        ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
        graphDiagram.Save(outputPath, saveOptions);

        Console.WriteLine($"Dependency graph saved to: {outputPath}");
    }
}
