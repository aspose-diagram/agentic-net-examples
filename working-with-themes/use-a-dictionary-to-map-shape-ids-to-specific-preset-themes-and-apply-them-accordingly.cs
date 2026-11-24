using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (can be passed as command‑line arguments)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the source Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // -----------------------------------------------------------------
            // Define a mapping from shape IDs to the desired preset theme settings.
            // The tuple contains: PresetTheme, PresetThemeVariant, PresetQuickStyle.
            // -----------------------------------------------------------------
            var themeMap = new Dictionary<long, (PresetThemeValue theme,
                                                 PresetThemeVariantValue variant,
                                                 PresetQuickStyleValue quickStyle)>
            {
                // Example mappings – adjust IDs and theme values as needed
                { 1, (PresetThemeValue.Bubble, PresetThemeVariantValue.Variant1, PresetQuickStyleValue.VariantStyle1) },
                { 2, (PresetThemeValue.Bubble, PresetThemeVariantValue.Variant2, PresetQuickStyleValue.VariantStyle2) }
            };

            // -----------------------------------------------------------------
            // Apply the mapped themes to the corresponding shapes.
            // -----------------------------------------------------------------
            foreach (var kvp in themeMap)
            {
                long shapeId = kvp.Key;
                var (theme, variant, quickStyle) = kvp.Value;

                Shape targetShape = null;

                // Search all pages for the shape with the given ID
                foreach (Page page in diagram.Pages)
                {
                    // GetShape returns null if the ID is not present on this page
                    targetShape = page.Shapes.GetShape(shapeId);
                    if (targetShape != null)
                        break;
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Shape ID {shapeId} not found in any page.");
                    continue;
                }

                // Apply the preset theme, variant, and quick style (write‑only properties)
                targetShape.PresetTheme = theme;
                targetShape.PresetThemeVariant = variant;
                targetShape.PresetThemeQuickStyle = quickStyle;

                // Optionally set additional style matrices for richer appearance
                // (Style2 and Color7 are example values; adjust as required)
                targetShape.SetPresetThemeStyleMatrics(PresetStyleMatricsValue.Style2,
                                                      PresetColorMatricsValue.Color7);
            }

            // -----------------------------------------------------------------
            // Save the modified diagram to the output path.
            // -----------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}