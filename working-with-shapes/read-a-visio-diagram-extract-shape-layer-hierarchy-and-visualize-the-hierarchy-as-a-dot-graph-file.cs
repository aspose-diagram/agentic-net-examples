using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputDotPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // StringBuilder for DOT content
            StringBuilder dotBuilder = new StringBuilder();
            dotBuilder.AppendLine("digraph G {");

            // Dictionaries to keep track of defined nodes
            var definedLayerNodes = new System.Collections.Generic.HashSet<int>();
            var definedShapeNodes = new System.Collections.Generic.HashSet<long>();

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Build a map of layer index to layer name for the current page
                var layerIndexToName = new System.Collections.Generic.Dictionary<int, string>();
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    int layerIx = layer.IX;
                    string layerName = layer.Name?.Value ?? $"Layer_{layerIx}";
                    layerIndexToName[layerIx] = layerName;

                    // Define layer node if not already defined
                    if (!definedLayerNodes.Contains(layerIx))
                    {
                        string safeLayerName = layerName.Replace("\"", "\\\"");
                        dotBuilder.AppendLine($"    L{layerIx} [label=\"{safeLayerName}\"];");
                        definedLayerNodes.Add(layerIx);
                    }
                }

                // Iterate through each shape on the page
                foreach (Shape shape in page.Shapes)
                {
                    long shapeId = shape.ID;
                    // Define shape node if not already defined
                    if (!definedShapeNodes.Contains(shapeId))
                    {
                        // Use shape text as label if available, otherwise use ID
                        string shapeLabel = shape.Text?.Value?.ToString() ?? $"Shape_{shapeId}";
                        shapeLabel = shapeLabel.Replace("\"", "\\\"");
                        dotBuilder.AppendLine($"    S{shapeId} [label=\"{shapeLabel}\"];");
                        definedShapeNodes.Add(shapeId);
                    }

                    // Retrieve layer membership string
                    string layerMember = shape.LayerMem?.LayerMember?.Value;
                    if (string.IsNullOrEmpty(layerMember))
                    {
                        continue; // Shape does not belong to any layer
                    }

                    // Split semicolon-separated layer indices
                    string[] parts = layerMember.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string part in parts)
                    {
                        if (int.TryParse(part.Trim(), out int layerIdx) && layerIndexToName.ContainsKey(layerIdx))
                        {
                            // Create an edge from layer to shape
                            dotBuilder.AppendLine($"    L{layerIdx} -> S{shapeId};");
                        }
                    }
                }
            }

            dotBuilder.AppendLine("}");

            // Write DOT content to the output file
            File.WriteAllText(outputPath, dotBuilder.ToString());
            Console.WriteLine($"DOT graph successfully written to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}