using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Build a lookup of layer index -> layer name for the current page
                Dictionary<int, string> layerIndexToName = new Dictionary<int, string>();
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Store the layer's index (IX) and its display name (Name.Value)
                    layerIndexToName[layer.IX] = layer.Name.Value;
                }

                // Iterate through each shape on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the semicolon‑separated list of layer indexes the shape belongs to
                    string layerMember = shape.LayerMem?.LayerMember?.Value;
                    if (string.IsNullOrEmpty(layerMember))
                        continue; // Shape is not assigned to any layer

                    // Split the list into individual indexes
                    string[] indexes = layerMember.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string idxStr in indexes)
                    {
                        if (!int.TryParse(idxStr, out int idx))
                            continue; // Invalid index, skip

                        // Resolve the layer name using the lookup dictionary
                        if (!layerIndexToName.TryGetValue(idx, out string layerName))
                            continue; // Unknown layer, skip

                        // Determine theme settings based on the layer name
                        (PresetThemeValue theme, PresetThemeVariantValue variant, PresetQuickStyleValue style) = GetThemeForLayer(layerName);

                        // Apply the theme to the shape (write‑only properties)
                        shape.PresetTheme = theme;
                        shape.PresetThemeVariant = variant;
                        shape.PresetThemeQuickStyle = style;

                        // Once a matching layer is processed, stop further checks for this shape
                        break;
                    }
                }
            }

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Returns theme settings for a given layer name.
    // Extend this method to map additional layer names to desired themes.
    private static (PresetThemeValue, PresetThemeVariantValue, PresetQuickStyleValue) GetThemeForLayer(string layerName)
    {
        // Default theme settings (used when no specific mapping exists)
        PresetThemeValue defaultTheme = PresetThemeValue.Bubble;
        PresetThemeVariantValue defaultVariant = PresetThemeVariantValue.Variant1;
        PresetQuickStyleValue defaultStyle = PresetQuickStyleValue.VariantStyle1;

        // Example mappings for specific layer names
        switch (layerName.Trim())
        {
            case "Layer1":
                return (PresetThemeValue.Bubble, PresetThemeVariantValue.Variant1, PresetQuickStyleValue.VariantStyle1);
            case "Layer2":
                return (PresetThemeValue.Bubble, PresetThemeVariantValue.Variant2, PresetQuickStyleValue.VariantStyle2);
            case "Layer3":
                return (PresetThemeValue.Bubble, PresetThemeVariantValue.Variant3, PresetQuickStyleValue.VariantStyle3);
            default:
                // Use default theme for any other layer
                return (defaultTheme, defaultVariant, defaultStyle);
        }
    }
}