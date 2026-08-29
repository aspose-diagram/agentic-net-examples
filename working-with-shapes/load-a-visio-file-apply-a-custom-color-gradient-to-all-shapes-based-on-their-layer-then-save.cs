using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> [outputVisioPath]");
            return;
        }

        // Input Visio file path
        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine output path (optional second argument or default)
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", Path.GetFileNameWithoutExtension(inputPath) + "_gradient.vsdx");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Build a map of layer index to gradient colors
                // For demonstration, use a few preset color pairs; extra layers reuse the last pair
                var layerColors = new System.Collections.Generic.Dictionary<int, (string start, string end)>();
                string[] startColors = { "#FF0000", "#00FF00", "#0000FF", "#FFFF00", "#FF00FF", "#00FFFF" };
                string[] endColors   = { "#800000", "#008000", "#000080", "#808000", "#800080", "#008080" };
                int colorIdx = 0;

                // Populate the dictionary with layer indices
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Use the layer's index (IX) as the key
                    int layerIdx = layer.IX;
                    // Assign a color pair, cycling if there are more layers than colors
                    layerColors[layerIdx] = (startColors[colorIdx % startColors.Length], endColors[colorIdx % endColors.Length]);
                    colorIdx++;
                }

                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True) continue;

                    // Retrieve the layer membership string (e.g., "0;2")
                    string layerMember = shape.LayerMem.LayerMember.Value ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(layerMember)) continue; // shape not assigned to any layer

                    // Use the first listed layer index for gradient selection
                    string[] layerIndices = layerMember.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (layerIndices.Length == 0) continue;

                    // Parse the first layer index
                    if (!int.TryParse(layerIndices[0], out int firstLayerIdx)) continue;

                    // Determine gradient colors for this layer; fallback to a default if missing
                    if (!layerColors.TryGetValue(firstLayerIdx, out var colors))
                    {
                        colors = ("#CCCCCC", "#EEEEEE"); // default light gray gradient
                    }

                    // Apply gradient fill to the shape
                    shape.Fill.FillPattern.Value = 25; // gradient fill pattern
                    shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True; // enable gradient
                    shape.Fill.GradientFill.GradientDir.Value = 0; // horizontal direction
                    shape.Fill.GradientFill.GradientStops.Clear(); // clear existing stops
                    // Add start color at position 0
                    shape.Fill.GradientFill.GradientStops.Add(
                        new DoubleValue(0, MeasureConst.NUM),
                        new ColorValue(colors.start, MeasureConst.Undefined));
                    // Add end color at position 1
                    shape.Fill.GradientFill.GradientStops.Add(
                        new DoubleValue(1, MeasureConst.NUM),
                        new ColorValue(colors.end, MeasureConst.Undefined));
                }
            }

            // Save the modified diagram to the output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with gradients: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}