using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path – first argument or default.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output DOT file path – second argument or default.
        string outputPath = args.Length > 1 ? args[1] : "output.dot";
        // Guard: ensure the directory for the output file exists.
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Use the first page for layer extraction (layers are page‑specific).
            Page page = diagram.Pages[0];

            // Build a map from layer index to its name.
            var layerNames = new System.Collections.Generic.Dictionary<int, string>();
            foreach (Layer layer in page.PageSheet.Layers)
            {
                // Layer index is stored in the IX property.
                int idx = layer.IX;
                // Layer name is a string wrapper; access via .Value.
                string name = layer.Name.Value;
                layerNames[idx] = name;
            }

            // Build a map from layer index to the shapes that belong to it.
            var layerToShapes = new System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<Shape>>();
            foreach (Shape shape in page.Shapes)
            {
                // Retrieve the semicolon‑separated list of layer indexes for the shape.
                string memberStr = shape.LayerMem?.LayerMember?.Value ?? string.Empty;
                if (string.IsNullOrWhiteSpace(memberStr))
                    continue; // Shape is not assigned to any layer.

                // Split the string and parse each index.
                string[] parts = memberStr.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string part in parts)
                {
                    if (int.TryParse(part, out int layerIdx))
                    {
                        if (!layerToShapes.ContainsKey(layerIdx))
                            layerToShapes[layerIdx] = new System.Collections.Generic.List<Shape>();
                        layerToShapes[layerIdx].Add(shape);
                    }
                }
            }

            // Begin constructing the DOT graph content.
            var dotLines = new System.Text.StringBuilder();
            dotLines.AppendLine("digraph G {");
            dotLines.AppendLine("    rankdir=LR;"); // Layout left‑to‑right for readability.

            // Create a node for each layer.
            foreach (var kvp in layerNames)
            {
                int layerIdx = kvp.Key;
                string layerLabel = kvp.Value.Replace("\"", "\\\""); // Escape quotes.
                string layerNodeId = $"layer_{layerIdx}";
                dotLines.AppendLine($"    {layerNodeId} [label=\"{layerLabel}\", shape=box, style=filled, fillcolor=lightgray];");
            }

            // Create nodes for shapes and edges from their layer to the shape.
            foreach (var kvp in layerToShapes)
            {
                int layerIdx = kvp.Key;
                string layerNodeId = $"layer_{layerIdx}";
                foreach (Shape shape in kvp.Value)
                {
                    // Use shape ID as a unique identifier.
                    long shapeId = shape.ID;
                    string shapeNodeId = $"shape_{shapeId}";
                    // Prefer the universal name; fall back to the numeric ID.
                    string shapeLabel = !string.IsNullOrWhiteSpace(shape.NameU) ? shape.NameU : shapeId.ToString();
                    shapeLabel = shapeLabel.Replace("\"", "\\\"");
                    dotLines.AppendLine($"    {shapeNodeId} [label=\"{shapeLabel}\"];");
                    dotLines.AppendLine($"    {layerNodeId} -> {shapeNodeId};");
                }
            }

            dotLines.AppendLine("}"); // End of graph.

            // Write the DOT content to the output file.
            File.WriteAllText(outputPath, dotLines.ToString());
            Console.WriteLine($"DOT graph generated successfully at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any unexpected errors from Aspose.Diagram or IO operations.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}