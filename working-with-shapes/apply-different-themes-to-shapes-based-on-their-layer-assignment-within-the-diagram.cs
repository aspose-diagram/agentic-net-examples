using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Build a lookup of layer index -> layer name for the current page
                    Dictionary<int, string> layerIndexToName = new Dictionary<int, string>();
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Layer.IX is the zero‑based index of the layer
                        layerIndexToName[layer.IX] = layer.Name.Value;
                    }

                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Get the layer membership string (e.g., "0;2")
                        string layerMember = shape.LayerMem.LayerMember.Value;
                        if (string.IsNullOrWhiteSpace(layerMember))
                            continue; // Shape is not assigned to any layer

                        // Determine the first layer the shape belongs to
                        string[] parts = layerMember.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length == 0)
                            continue;

                        // Parse the first layer index
                        if (!int.TryParse(parts[0], out int layerIdx))
                            continue;

                        // Retrieve the layer name; if not found, skip
                        if (!layerIndexToName.TryGetValue(layerIdx, out string layerName))
                            continue;

                        // Apply a theme based on the layer name (example logic)
                        // You can extend this mapping as needed
                        if (layerName.Equals("RedLayer", StringComparison.OrdinalIgnoreCase))
                        {
                            shape.PresetTheme = PresetThemeValue.Bubble;
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;
                        }
                        else if (layerName.Equals("BlueLayer", StringComparison.OrdinalIgnoreCase))
                        {
                            shape.PresetTheme = PresetThemeValue.Bubble;
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant2;
                            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;
                        }
                        else if (layerName.Equals("GreenLayer", StringComparison.OrdinalIgnoreCase))
                        {
                            shape.PresetTheme = PresetThemeValue.Bubble;
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant3;
                            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle3;
                        }
                        else
                        {
                            // Default theme for other layers
                            shape.PresetTheme = PresetThemeValue.Bubble;
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant4;
                            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle4;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }