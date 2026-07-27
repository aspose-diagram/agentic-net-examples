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
            // Input MMD file path (first argument or default)
            string mmdPath = args.Length > 0 ? args[0] : "flowchart.mmd";
            if (!File.Exists(mmdPath))
            {
                Console.WriteLine($"MMD file not found: {mmdPath}");
                return;
            }

            // Read all lines from the MMD file
            string[] lines = File.ReadAllLines(mmdPath);

            // Create an empty diagram and add a page
            Diagram diagram = new Diagram();
            Page page = new Page();
            diagram.Pages.Add(page);

            // Dictionaries to keep track of node names and their shape IDs
            Dictionary<string, long> nodeShapeIds = new Dictionary<string, long>(StringComparer.OrdinalIgnoreCase);
            List<(string from, string to)> connections = new List<(string, string)>();

            // Simple parsing: look for lines containing "-->" or "->"
            foreach (string rawLine in lines)
            {
                string line = rawLine.Trim();
                if (string.IsNullOrEmpty(line) || line.StartsWith("%") || line.StartsWith("#"))
                    continue; // skip empty or comment lines

                string delimiter = null;
                if (line.Contains("-->"))
                    delimiter = "-->";
                else if (line.Contains("->"))
                    delimiter = "->";

                if (delimiter != null)
                {
                    string[] parts = line.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 2)
                    {
                        string fromNode = parts[0].Trim();
                        string toNode = parts[1].Trim();
                        connections.Add((fromNode, toNode));

                        // Ensure both nodes have shapes (will be created later)
                        if (!nodeShapeIds.ContainsKey(fromNode))
                            nodeShapeIds[fromNode] = 0;
                        if (!nodeShapeIds.ContainsKey(toNode))
                            nodeShapeIds[toNode] = 0;
                    }
                }
            }

            // Layout parameters for node shapes
            double startX = 2.0;          // inches from left
            double startY = 2.0;          // inches from top
            double verticalSpacing = 2.0; // inches between nodes
            double shapeWidth = 2.0;      // rectangle width
            double shapeHeight = 1.0;     // rectangle height

            // Create shapes for each unique node
            double currentY = startY;
            foreach (var node in new List<string>(nodeShapeIds.Keys))
            {
                long shapeId = page.AddShape(startX, currentY, shapeWidth, shapeHeight, "Rectangle", false);
                Shape shape = page.Shapes.GetShape(shapeId);
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt(node));

                // Store the generated shape ID
                nodeShapeIds[node] = shapeId;

                currentY += shapeHeight + verticalSpacing;
            }

            // Create connectors and link shapes
            foreach (var conn in connections)
            {
                if (!nodeShapeIds.TryGetValue(conn.from, out long fromId) ||
                    !nodeShapeIds.TryGetValue(conn.to, out long toId))
                {
                    Console.WriteLine($"Warning: missing shape for connection {conn.from} -> {conn.to}");
                    continue;
                }

                // Add a dynamic connector shape (size parameters are ignored for connectors)
                long connectorId = page.AddShape(0, 0, 0, 0, "Dynamic connector", false);
                Shape connector = page.Shapes.GetShape(connectorId);

                // Connect the two shapes using the connector
                page.ConnectShapesViaConnector(
                    fromId,
                    ConnectionPointPlace.Bottom,
                    toId,
                    ConnectionPointPlace.Top,
                    connectorId);
            }

            // Save the resulting diagram as Visio VSDX
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
    }