using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        // Simple representation of a flowchart node parsed from MMD
        class Node
        {
            public string Id;
            public string Text;
            public string Type; // e.g., "process", "decision"
        }

        // Simple representation of a connection between nodes
        class Edge
        {
            public string FromId;
            public string ToId;
        }

        static void Main(string[] args)
        {
            try
            {

                // Path to the input MMD file and output Visio file
                string inputMmdPath = "flowchart.mmd";
                string outputVisioPath = "flowchart.vdx";

                // Parse the MMD file (very basic parser for Mermaid flowchart syntax)
                List<Node> nodes = new List<Node>();
                List<Edge> edges = new List<Edge>();
                ParseMmd(File.ReadAllLines(inputMmdPath), nodes, edges);

                // Create an empty Visio diagram
                Diagram diagram = new Diagram();

                // Add a default page (Visio creates a default page automatically)
                int pageIndex = 0;

                // Mapping from node Id to shape PinX,PinY for connector placement
                Dictionary<string, (double X, double Y)> nodePositions = new Dictionary<string, (double, double)>();

                // Layout parameters
                double startX = 2.0;   // inches
                double startY = 5.0;   // inches
                double horizontalSpacing = 3.0;
                double verticalSpacing = 2.0;

                // Place nodes on the page
                for (int i = 0; i < nodes.Count; i++)
                {
                    Node node = nodes[i];
                    double pinX = startX + (i % 3) * horizontalSpacing;
                    double pinY = startY - (i / 3) * verticalSpacing;

                    // Choose master shape based on node type
                    string masterName = node.Type == "decision" ? "Decision" : "Process";

                    // Add the shape to the diagram
                    diagram.AddShape(pinX, pinY, masterName, pageIndex);

                    // Store position for later connector creation
                    nodePositions[node.Id] = (pinX, pinY);
                }

                // Add connectors between nodes
                foreach (Edge edge in edges)
                {
                    if (nodePositions.TryGetValue(edge.FromId, out var fromPos) &&
                        nodePositions.TryGetValue(edge.ToId, out var toPos))
                    {
                        // Add a dynamic connector shape
                        // The AddShape overload with coordinates creates a shape with given PinX,PinY,Width,Height.
                        // For connectors we use a very small width/height and rely on the "Dynamic connector" master.
                        double connectorPinX = (fromPos.X + toPos.X) / 2;
                        double connectorPinY = (fromPos.Y + toPos.Y) / 2;
                        diagram.AddShape(connectorPinX, connectorPinY, 0.1, 0.1, "Dynamic connector", pageIndex);
                    }
                }

                // Save the diagram in Visio VDX format
                diagram.Save(outputVisioPath, SaveFileFormat.Vdx);

                // Clean up
                diagram.Dispose();

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Very simple parser for Mermaid flowchart syntax:
        // Example lines:
        //   A[Start] --> B{Decision}
        //   B --> C[Process]
        //   B --> D[End]
        static void ParseMmd(string[] lines, List<Node> nodes, List<Edge> edges)
        {
            int nodeCounter = 0;
            var nodeMap = new Dictionary<string, Node>();

            foreach (string rawLine in lines)
            {
                string line = rawLine.Trim();

                // Skip empty lines and the graph declaration (e.g., "graph TD")
                if (string.IsNullOrEmpty(line) || line.StartsWith("graph", StringComparison.OrdinalIgnoreCase))
                    continue;

                // Split on connector arrow
                string[] parts = line.Split(new[] { "-->", "->" }, StringSplitOptions.None);
                if (parts.Length != 2)
                    continue; // Not a simple edge definition

                string left = parts[0].Trim();
                string right = parts[1].Trim();

                // Parse left node
                Node leftNode = ParseNode(left, ref nodeCounter);
                if (!nodeMap.ContainsKey(leftNode.Id))
                {
                    nodes.Add(leftNode);
                    nodeMap[leftNode.Id] = leftNode;
                }

                // Parse right node
                Node rightNode = ParseNode(right, ref nodeCounter);
                if (!nodeMap.ContainsKey(rightNode.Id))
                {
                    nodes.Add(rightNode);
                    nodeMap[rightNode.Id] = rightNode;
                }

                // Record edge
                edges.Add(new Edge { FromId = leftNode.Id, ToId = rightNode.Id });
            }
        }

        // Parses a node token like "A[Start]" or "B{Decision}"
        static Node ParseNode(string token, ref int counter)
        {
            // Identify the identifier (before any bracket)
            int bracketIdx = token.IndexOfAny(new[] { '[', '{', '(' });
            string id = bracketIdx > 0 ? token.Substring(0, bracketIdx).Trim() : token.Trim();

            // Determine type based on surrounding brackets
            string type = "process"; // default
            string text = id; // fallback

            if (bracketIdx > 0 && bracketIdx < token.Length - 1)
            {
                char open = token[bracketIdx];
                char close = token[token.Length - 1];
                int closeIdx = token.LastIndexOf(close);
                if (closeIdx > bracketIdx)
                {
                    text = token.Substring(bracketIdx + 1, closeIdx - bracketIdx - 1);
                }

                if (open == '{' && close == '}')
                    type = "decision";
                else if (open == '[' && close == ']')
                    type = "process";
                else if (open == '(' && close == ')')
                    type = "process";
            }

            // Ensure a unique identifier for Visio shapes
            string uniqueId = $"Node{counter++}";

            return new Node
            {
                Id = uniqueId,
                Text = text,
                Type = type
            };
        }
    }