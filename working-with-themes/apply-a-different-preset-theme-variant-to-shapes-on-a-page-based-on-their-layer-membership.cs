using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Assume we work with the first page. Adjust as needed.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            // Build a mapping from layer index to the desired theme variant.
            // Example: Layer named "LayerA" -> Variant1, "LayerB" -> Variant2, others -> Variant3.
            var layerVariantMap = new Dictionary<int, PresetThemeVariantValue>();

            foreach (Layer layer in page.PageSheet.Layers)
            {
                // Retrieve the layer name as a plain string.
                string layerName = layer.Name.Value ?? string.Empty;

                // Determine which variant to assign based on the layer name.
                PresetThemeVariantValue variant = PresetThemeVariantValue.Variant3; // default

                if (string.Equals(layerName, "LayerA", StringComparison.OrdinalIgnoreCase))
                {
                    variant = PresetThemeVariantValue.Variant1;
                }
                else if (string.Equals(layerName, "LayerB", StringComparison.OrdinalIgnoreCase))
                {
                    variant = PresetThemeVariantValue.Variant2;
                }

                // Store the mapping using the layer's zero‑based index (IX).
                layerVariantMap[layer.IX] = variant;
            }

            // Iterate over all shapes on the page.
            foreach (Shape shape in page.Shapes)
            {
                // Retrieve the semicolon‑separated list of layer indices the shape belongs to.
                string layerMember = shape.LayerMem.LayerMember.Value;

                if (string.IsNullOrEmpty(layerMember))
                {
                    // Shape does not belong to any layer; skip.
                    continue;
                }

                // Split the string into individual indices.
                string[] parts = layerMember.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                // Determine the first matching layer that has a variant mapping.
                PresetThemeVariantValue? targetVariant = null;
                foreach (string part in parts)
                {
                    if (int.TryParse(part.Trim(), out int layerIndex) && layerVariantMap.TryGetValue(layerIndex, out PresetThemeVariantValue variant))
                    {
                        targetVariant = variant;
                        break; // Apply the first matching variant.
                    }
                }

                if (targetVariant.HasValue)
                {
                    // Apply a preset theme (using a common theme) and the selected variant to the shape.
                    shape.PresetTheme = PresetThemeValue.Bubble; // Choose any valid preset theme.
                    shape.PresetThemeVariant = targetVariant.Value;
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}