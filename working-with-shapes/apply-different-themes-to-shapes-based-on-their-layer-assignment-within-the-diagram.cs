using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio diagram
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Build a lookup of layer index to desired theme settings
                    Dictionary<int, (PresetThemeValue theme, PresetThemeVariantValue variant, PresetQuickStyleValue quickStyle)> layerThemeMap
                        = new Dictionary<int, (PresetThemeValue, PresetThemeVariantValue, PresetQuickStyleValue)>();

                    // Populate the map based on existing layers
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Example mapping: assign different variants per layer index
                        // You can customize this mapping as needed
                        PresetThemeVariantValue variant = PresetThemeVariantValue.Variant1;
                        PresetQuickStyleValue quickStyle = PresetQuickStyleValue.VariantStyle1;

                        switch (layer.IX)
                        {
                            case 0:
                                variant = PresetThemeVariantValue.Variant1;
                                quickStyle = PresetQuickStyleValue.VariantStyle1;
                                break;
                            case 1:
                                variant = PresetThemeVariantValue.Variant2;
                                quickStyle = PresetQuickStyleValue.VariantStyle2;
                                break;
                            case 2:
                                variant = PresetThemeVariantValue.Variant3;
                                quickStyle = PresetQuickStyleValue.VariantStyle3;
                                break;
                            default:
                                variant = PresetThemeVariantValue.Variant4;
                                quickStyle = PresetQuickStyleValue.VariantStyle4;
                                break;
                        }

                        // All layers use the same base theme (Bubble) in this example
                        layerThemeMap[layer.IX] = (PresetThemeValue.Bubble, variant, quickStyle);
                    }

                    // Iterate through each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the layer membership string (e.g., "0;2")
                        string layerMember = shape.LayerMem.LayerMember.Value;

                        if (string.IsNullOrEmpty(layerMember))
                        {
                            // Shape is not assigned to any layer; skip or apply a default theme if desired
                            continue;
                        }

                        // Split the membership string into individual layer indexes
                        string[] layerIndexes = layerMember.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                        // Apply the first matching layer's theme to the shape
                        foreach (string idxStr in layerIndexes)
                        {
                            if (int.TryParse(idxStr, out int layerIdx) && layerThemeMap.ContainsKey(layerIdx))
                            {
                                var themeInfo = layerThemeMap[layerIdx];

                                // Apply the theme to the shape
                                shape.PresetTheme = themeInfo.theme;
                                shape.PresetThemeVariant = themeInfo.variant;
                                shape.PresetThemeQuickStyle = themeInfo.quickStyle;

                                // Once a theme is applied based on a layer, stop checking further layers for this shape
                                break;
                            }
                        }
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }