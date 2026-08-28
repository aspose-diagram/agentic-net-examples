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
        // Input Visio file path (required)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output diagram visualizing the connections
        string outputDiagramPath = args.Length > 1 ? args[1] : "connections.vsdx";

        // Output CSV file containing routing information
        string outputCsvPath = args.Length > 2 ? args[2] : "routing_info.csv";

        // Load the source diagram inside a try/catch block
        Diagram sourceDiagram;
        try
        {
            sourceDiagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Collection to hold extracted connection data
        var connections = new List<(long sourceId, long targetId, ShapeRouteStyleValue routeStyle, ConLineJumpStyleValue jumpStyle)>();

        // Iterate each page to extract connector routing information
        foreach (Page page in sourceDiagram.Pages)
        {
            // Build a lookup of connector shapes (1‑D shapes)
            var connectorShapes = new Dictionary<long, Shape>();
            foreach (Shape shape in page.Shapes)
            {
                if (shape.OneD) // 1‑D shapes are connectors
                {
                    connectorShapes[shape.ID] = shape;
                }
            }

            // For each connector, determine its source and target via the Connects collection
            foreach (Connect connect in page.Connects)
            {
                // A connection where FromSheet is a shape and ToSheet is a connector
                if (connectorShapes.ContainsKey(connect.ToSheet) && !connectorShapes.ContainsKey(connect.FromSheet))
                {
                    long connectorId = connect.ToSheet;
                    long sourceShapeId = connect.FromSheet;

                    // Find the matching second connection (connector -> target)
                    foreach (Connect second in page.Connects)
                    {
                        if (second.FromSheet == connectorId && !connectorShapes.ContainsKey(second.ToSheet))
                        {
                            long targetShapeId = second.ToSheet;

                            Shape connector = connectorShapes[connectorId];
                            // Retrieve routing style (default is RightAngle)
                            ShapeRouteStyleValue routeStyle = connector.Layout.ShapeRouteStyle.Value;
                            // Retrieve line jump style (default is PageDefault)
                            ConLineJumpStyleValue jumpStyle = connector.Layout.ConLineJumpStyle.Value;

                            connections.Add((sourceShapeId, targetShapeId, routeStyle, jumpStyle));
                            break;
                        }
                    }
                }
            }
        }

        // Write routing information to CSV
        try
        {
            using (var writer = new StreamWriter(outputCsvPath))
            {
                writer.WriteLine("SourceId,TargetId,ShapeRouteStyle,ConLineJumpStyle");
                foreach (var c in connections)
                {
                    writer.WriteLine($"{c.sourceId},{c.targetId},{c.routeStyle},{c.jumpStyle}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write CSV: {ex.Message}");
            // Continue – visual diagram can still be generated
        }

        // Create a new diagram to visualize the extracted graph
        Diagram visualDiagram = new Diagram();
        // Add a blank page
        visualDiagram.Pages.Add(new Page());

        Page visualPage = visualDiagram.Pages[0];

        // Determine unique node IDs
        var nodeIds = new HashSet<long>();
        foreach (var c in connections)
        {
            nodeIds.Add(c.sourceId);
            nodeIds.Add(c.targetId);
        }

        // Simple grid layout parameters
        int nodeCount = nodeIds.Count;
        int columns = (int)Math.Ceiling(Math.Sqrt(nodeCount));
        int rows = (int)Math.Ceiling((double)nodeCount / columns);
        double hSpacing = 2.0; // inches between columns
        double vSpacing = 1.5; // inches between rows
        double nodeWidth = 1.0;
        double nodeHeight = 0.6;

        // Map original shape IDs to newly created node shape IDs
        var nodeMap = new Dictionary<long, long>();
        int index = 0;
        foreach (long originalId in nodeIds)
        {
            int col = index % columns;
            int row = index / columns;
            double pinX = (col + 1) * hSpacing;
            double pinY = (row + 1) * vSpacing;

            // Draw a rectangle representing the node
            long nodeShapeId = visualPage.DrawRectangle(pinX, pinY, nodeWidth, nodeHeight);
            Shape nodeShape = visualPage.Shapes.GetShape(nodeShapeId);
            // Clear any default text and add custom label
            nodeShape.Text.Value.Clear();
            nodeShape.Text.Value.Add(new Txt($"Node {originalId}"));
            // Store mapping
            nodeMap[originalId] = nodeShapeId;
            index++;
        }

        // Add connectors based on extracted routing data
        foreach (var c in connections)
        {
            // Retrieve the newly created node shape IDs
            if (!nodeMap.TryGetValue(c.sourceId, out long srcNodeId) ||
                !nodeMap.TryGetValue(c.targetId, out long tgtNodeId))
                continue; // safety check

            // Add a dynamic connector shape (isCalculate = false)
            long connectorId = visualPage.AddShape(0, 0, 0, 0, "Dynamic connector", false);
            Shape connectorShape = visualPage.Shapes.GetShape(connectorId);

            // Apply original routing style
            connectorShape.Layout.ShapeRouteStyle.Value = c.routeStyle;
            connectorShape.Layout.ConLineJumpStyle.Value = c.jumpStyle;

            // Connect the two node shapes using the connector
            visualPage.ConnectShapesViaConnector(
                srcNodeId,
                ConnectionPointPlace.Bottom,
                tgtNodeId,
                ConnectionPointPlace.Top,
                connectorId);
        }

        // Save the visual diagram (VSDX format) with proper save options
        try
        {
            visualDiagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save visual diagram: {ex.Message}");
        }
    }
}