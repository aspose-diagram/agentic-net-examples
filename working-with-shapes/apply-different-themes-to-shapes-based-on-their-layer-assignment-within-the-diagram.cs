using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Map layer indexes to preset themes (use only supported enum members)
            var layerThemeMap = new Dictionary<int, PresetThemeValue>
            {
                { 0, PresetThemeValue.Bubble },   // Layer 0 → Bubble theme
                { 1, PresetThemeValue.Clouds }    // Layer 1 → Clouds theme
                // Add more mappings here if additional themes are needed
            };

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Get the layer membership string (e.g., "0;1")
                    string layerMember = shape.LayerMem.LayerMember.Value;

                    if (string.IsNullOrEmpty(layerMember))
                        continue; // Shape is not assigned to any layer

                    // Split the layer indexes and process each one
                    string[] parts = layerMember.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string part in parts)
                    {
                        if (int.TryParse(part, out int layerIndex))
                        {
                            // If a theme is defined for this layer, apply it
                            if (layerThemeMap.TryGetValue(layerIndex, out PresetThemeValue theme))
                            {
                                shape.PresetTheme = theme;
                                // Optionally set a variant (write‑only)
                                shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                                // Theme applied; stop processing further layers for this shape
                                break;
                            }
                        }
                    }
                }
            }

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}