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
        // Input MMD file path (adjust as needed)
        string mmdPath = "flowchart.mmd";
        if (!File.Exists(mmdPath))
        {
            Console.WriteLine($"MMD file not found: {mmdPath}");
            return;
        }

        // Read all lines from the MMD file
        string[] lines = File.ReadAllLines(mmdPath);

        // Simple parser for Mermaid flowchart syntax:
        //   node1 --> node2
        //   node2 --> node3
        // Collect nodes and edges
        var edges = new List<(string from, string to)>();
        var nodes = new HashSet<string>();

        foreach (string rawLine in lines)
        {
            string line = rawLine.Trim();

            // Skip empty lines and lines that are not connections
            if (string.IsNullOrEmpty(line) || !line.Contains("-->"))
                continue;

            // Split on the connection operator
            string[] parts = line.Split(new[] { "-->" }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
                continue;

            string from = parts[0].Trim();
            string to = parts[1].Trim();

            nodes.Add(from);
            nodes.Add(to);
            edges.Add((from, to));
        }

        // Create an empty diagram
        Diagram diagram = new Diagram();

        // Use the first page (always exists in a new diagram)
        Page page = diagram.Pages[0];

        // Layout parameters
        double startX = 1.0;      // inches from left
        double startY = 1.0;      // inches from top
        double shapeWidth = 1.5;  // inches
        double shapeHeight = 0.8; // inches
        double hSpacing = 2.0;    // horizontal spacing between nodes
        double vSpacing = 1.5;    // vertical spacing (not used in this simple layout)

        // Assign a position for each node (simple left‑to‑right layout)
        var nodePositions = new Dictionary<string, (double x, double y)>();
        int index = 0;
        foreach (string node in nodes)
        {
            double x = startX + index * hSpacing;
            double y = startY;
            nodePositions[node] = (x, y);
            index++;
        }

        // Create shapes for each node and store their IDs
        var nodeShapeIds = new Dictionary<string, long>();
        foreach (var kvp in nodePositions)
        {
            string nodeName = kvp.Key;
            double pinX = kvp.Value.x;
            double pinY = kvp.Value.y;

            // Draw a rectangle representing the node
            long shapeId = page.DrawRectangle(pinX, pinY, shapeWidth, shapeHeight);
            Shape shape = page.Shapes.GetShape((int)shapeId);

            // Add the node label
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt(nodeName));

            nodeShapeIds[nodeName] = shapeId;
        }

        // Ensure the "Dynamic connector" master is available.
        // Most Visio installations include it in the built‑in stencil.
        // Add a connector shape for each edge and connect the nodes.
        foreach (var edge in edges)
        {
            if (!nodeShapeIds.ContainsKey(edge.from) || !nodeShapeIds.ContainsKey(edge.to))
                continue;

            long fromId = nodeShapeIds[edge.from];
            long toId = nodeShapeIds[edge.to];

            // Add a connector shape (dynamic connector)
            long connectorId = page.AddShape(0, 0, 0, 0, "Dynamic connector", false);
            // Connect the two shapes via the connector
            page.ConnectShapesViaConnector(
                fromId,
                ConnectionPointPlace.Right,
                toId,
                ConnectionPointPlace.Left,
                connectorId);
        }

        // Save the diagram as Visio VSDX
        string outputPath = "flowchart.vsdx";
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
        Console.WriteLine($"Diagram saved to {outputPath}");
    }
}
