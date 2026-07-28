using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Determine input Visio file path
            string inputPath;
            if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            {
                inputPath = args[0];
            }
            else
            {
                Console.Write("Enter the path to the Visio file: ");
                inputPath = Console.ReadLine();
            }

            if (string.IsNullOrWhiteSpace(inputPath) || !File.Exists(inputPath))
            {
                Console.WriteLine("Invalid input file path.");
                return;
            }

            // Determine output DOT file path
            string outputPath;
            if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            {
                outputPath = args[1];
            }
            else
            {
                outputPath = Path.ChangeExtension(inputPath, ".dot");
            }

            // Load the Visio diagram
            using (var diagram = new Diagram(inputPath))
            {
                // Prepare DOT content
                var sb = new StringBuilder();
                sb.AppendLine("digraph VisioLayers {");
                sb.AppendLine("    rankdir=LR;"); // left‑to‑right layout for readability

                // Collect layer definitions (index -> name)
                var layerIndexToName = new Dictionary<int, string>();
                foreach (Page page in diagram.Pages)
                {
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Layer.IX is the zero‑based index
                        int ix = layer.IX;
                        string layerName = layer.Name.Value;
                        if (!layerIndexToName.ContainsKey(ix))
                        {
                            layerIndexToName[ix] = layerName;
                            // Declare layer node
                            sb.AppendLine($"    \"{Escape(layerName)}\" [shape=box, style=filled, color=lightgray];");
                        }
                    }
                }

                // Process shapes and create edges to their layers
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Determine a readable shape identifier
                        string shapeLabel = !string.IsNullOrWhiteSpace(shape.NameU) ? shape.NameU : $"Shape_{shape.ID}";

                        // Declare shape node
                        sb.AppendLine($"    \"{Escape(shapeLabel)}\" [shape=ellipse];");

                        // Retrieve layer membership string (e.g., "0;2")
                        string layerMember = shape.LayerMem?.LayerMember?.Value;
                        if (string.IsNullOrWhiteSpace(layerMember))
                            continue; // shape not assigned to any layer

                        string[] parts = layerMember.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string part in parts)
                        {
                            if (int.TryParse(part, out int layerIdx) && layerIndexToName.TryGetValue(layerIdx, out string layerName))
                            {
                                // Create edge from layer to shape
                                sb.AppendLine($"    \"{Escape(layerName)}\" -> \"{Escape(shapeLabel)}\";");
                            }
                        }
                    }
                }

                sb.AppendLine("}"); // end of digraph

                // Write DOT file
                try
                {
                    File.WriteAllText(outputPath, sb.ToString());
                    Console.WriteLine($"DOT graph successfully written to: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to write DOT file: {ex.Message}");
                }
            }
        }

        // Helper to escape double quotes in DOT identifiers
        private static string Escape(string text)
        {
            return text?.Replace("\"", "\\\"") ?? string.Empty;
        }
    }