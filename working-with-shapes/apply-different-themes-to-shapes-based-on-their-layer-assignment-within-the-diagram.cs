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

                // Load the Visio diagram from a file
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Define theme settings for specific layer names
                var layerThemes = new Dictionary<string, (PresetThemeValue Theme, PresetThemeVariantValue Variant, PresetQuickStyleValue QuickStyle)>
                {
                    { "Layer1", (PresetThemeValue.Bubble, PresetThemeVariantValue.Variant1, PresetQuickStyleValue.VariantStyle1) },
                    { "Layer2", (PresetThemeValue.Clouds, PresetThemeVariantValue.Variant2, PresetQuickStyleValue.VariantStyle2) },
                    // Add more layer-to-theme mappings as needed
                };

                // Default theme if a shape's layer is not in the dictionary
                var defaultTheme = (Theme: PresetThemeValue.Bubble, Variant: PresetThemeVariantValue.Variant3, QuickStyle: PresetQuickStyleValue.VariantStyle3);

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Build a lookup of layer index (as string) to layer name for the current page
                    var layerIndexToName = new Dictionary<string, string>();
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Layer.IX is an integer index; convert to string for comparison
                        string ixStr = layer.IX.ToString();
                        string name = layer.Name.Value;
                        layerIndexToName[ixStr] = name;
                    }

                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the layer membership string (e.g., "0;2")
                        string layerMember = shape.LayerMem.LayerMember.Value;

                        // Determine which theme to apply
                        (PresetThemeValue Theme, PresetThemeVariantValue Variant, PresetQuickStyleValue QuickStyle) selectedTheme = defaultTheme;

                        if (!string.IsNullOrEmpty(layerMember))
                        {
                            // Split the membership string into individual layer indexes
                            string[] memberIndexes = layerMember.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                            // Check each assigned layer for a matching theme
                            foreach (string idx in memberIndexes)
                            {
                                if (layerIndexToName.TryGetValue(idx, out string layerName))
                                {
                                    if (layerThemes.TryGetValue(layerName, out var themeInfo))
                                    {
                                        selectedTheme = themeInfo;
                                        // Stop at the first matching layer
                                        break;
                                    }
                                }
                            }
                        }

                        // Apply the selected theme to the shape
                        shape.PresetTheme = selectedTheme.Theme;
                        shape.PresetThemeVariant = selectedTheme.Variant;
                        shape.PresetThemeQuickStyle = selectedTheme.QuickStyle;
                    }
                }

                // Save the modified diagram to a new file
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }