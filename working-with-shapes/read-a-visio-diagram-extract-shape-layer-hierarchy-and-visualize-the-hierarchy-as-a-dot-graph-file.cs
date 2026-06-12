using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Validate arguments
                if (args.Length < 1)
                {
                    Console.WriteLine("Usage: VisioLayerHierarchy <inputVisioFile> [outputDotFile]");
                    return;
                }

                string inputPath = args[0];
                string outputPath = args.Length > 1 ? args[1] : "layer_hierarchy.dot";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // StringBuilder for DOT content
                StringBuilder dotBuilder = new StringBuilder();
                dotBuilder.AppendLine("digraph G {");

                // Process each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Build a map of layer index to layer name for the current page
                    Dictionary<int, string> layerIndexToName = new Dictionary<int, string>();
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Ensure the layer has a valid name
                        string layerName = layer.Name.Value ?? $"Layer_{layer.IX}";
                        layerIndexToName[layer.IX] = layerName;

                        // Add a node for the layer
                        dotBuilder.AppendLine($"    \"L{layer.IX}\" [label=\"{EscapeLabel(layerName)}\"];");
                    }

                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Create a unique identifier for the shape node
                        string shapeNodeId = $"S{shape.ID}";
                        string shapeLabel = shape.NameU ?? $"Shape_{shape.ID}";
                        dotBuilder.AppendLine($"    \"{shapeNodeId}\" [label=\"{EscapeLabel(shapeLabel)}\"];");

                        // Retrieve layer membership string (semicolon separated indexes)
                        string layerMember = shape.LayerMem.LayerMember.Value;
                        if (!string.IsNullOrEmpty(layerMember))
                        {
                            string[] parts = layerMember.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string part in parts)
                            {
                                if (int.TryParse(part, out int layerIdx) && layerIndexToName.ContainsKey(layerIdx))
                                {
                                    // Add an edge from the layer to the shape
                                    dotBuilder.AppendLine($"    \"L{layerIdx}\" -> \"{shapeNodeId}\";");
                                }
                            }
                        }
                    }
                }

                dotBuilder.AppendLine("}");

                // Write the DOT file
                try
                {
                    File.WriteAllText(outputPath, dotBuilder.ToString());
                    Console.WriteLine($"DOT graph successfully written to '{outputPath}'.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error writing DOT file: {ex.Message}");
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }

        // Helper method to escape double quotes in labels
        private static string EscapeLabel(string label)
        {
            return label.Replace("\"", "\\\"");
        }
    }