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

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // StringBuilder to construct DOT graph content
            StringBuilder dotBuilder = new StringBuilder();
            dotBuilder.AppendLine("digraph G {");

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Build a map of layer index to layer name for quick lookup
                var layerIndexToName = new System.Collections.Generic.Dictionary<int, string>();

                // Process layers on the current page
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Use layer index (IX) as unique identifier
                    int layerIndex = layer.IX;
                    string layerName = layer.Name.Value ?? $"Layer{layerIndex}";
                    string layerNodeId = $"L{layerIndex}";

                    // Store mapping for later edge creation
                    layerIndexToName[layerIndex] = layerNodeId;

                    // Add layer node to DOT graph
                    dotBuilder.AppendLine($"    {layerNodeId} [label=\"{EscapeDotLabel(layerName)}\"];");
                }

                // Process shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve shape's layer membership string (semicolon-separated indexes)
                    string layerMembership = shape.LayerMem?.LayerMember?.Value ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(layerMembership))
                    {
                        // Shape does not belong to any layer; skip it for hierarchy visualization
                        continue;
                    }

                    // Create a unique node identifier for the shape
                    string shapeNodeId = $"S{shape.ID}";

                    // Determine shape label: use shape text if available, otherwise fallback to ID
                    string shapeLabel = !string.IsNullOrWhiteSpace(shape.Text?.Value?.ToString())
                        ? shape.Text.Value.ToString()
                        : $"Shape{shape.ID}";
                    dotBuilder.AppendLine($"    {shapeNodeId} [label=\"{EscapeDotLabel(shapeLabel)}\"];");

                    // Split the membership string and create edges from each layer to this shape
                    string[] parts = layerMembership.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string part in parts)
                    {
                        if (int.TryParse(part, out int layerIdx) && layerIndexToName.TryGetValue(layerIdx, out string layerNodeId))
                        {
                            dotBuilder.AppendLine($"    {layerNodeId} -> {shapeNodeId};");
                        }
                    }
                }
            }

            dotBuilder.AppendLine("}");

            // Write the DOT content to the output file
            File.WriteAllText(outputPath, dotBuilder.ToString());
            Console.WriteLine($"DOT graph successfully generated at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
        }
    }

    // Helper method to escape double quotes and backslashes in DOT labels
    private static string EscapeDotLabel(string label)
    {
        if (label == null) return string.Empty;
        return label.Replace("\\", "\\\\").Replace("\"", "\\\"");
    }
}