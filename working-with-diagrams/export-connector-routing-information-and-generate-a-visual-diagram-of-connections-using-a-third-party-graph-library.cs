using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (change as needed)
                string visioPath = "input.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Map shape IDs to readable names (or fallback to ID)
                var shapeNames = new Dictionary<long, string>();
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        string name = string.IsNullOrWhiteSpace(shape.Name) ? $"Shape_{shape.ID}" : shape.Name;
                        shapeNames[shape.ID] = name;
                    }
                }

                // Collect connector relationships (edges)
                var edges = new List<(long fromId, long toId)>();
                foreach (Page page in diagram.Pages)
                {
                    foreach (Connect connection in page.Connects)
                    {
                        // Connect objects expose FromSheet and ToSheet IDs
                        edges.Add((connection.FromSheet, connection.ToSheet));
                    }
                }

                // Build GraphViz DOT representation
                var dotLines = new List<string>();
                dotLines.Add("digraph VisioConnections {");
                dotLines.Add("    rankdir=LR;"); // left‑to‑right layout

                // Define nodes
                foreach (var kvp in shapeNames)
                {
                    string nodeId = $"node{kvp.Key}";
                    string label = kvp.Value.Replace("\"", "\\\"");
                    dotLines.Add($"    {nodeId} [label=\"{label}\"];");
                }

                // Define edges
                foreach (var edge in edges)
                {
                    string fromNode = $"node{edge.fromId}";
                    string toNode = $"node{edge.toId}";
                    dotLines.Add($"    {fromNode} -> {toNode};");
                }

                dotLines.Add("}");

                // Write DOT file
                string outputDotPath = "connections.dot";
                File.WriteAllLines(outputDotPath, dotLines);
                Console.WriteLine($"Connector routing exported to '{outputDotPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }