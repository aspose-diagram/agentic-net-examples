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

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Define which PresetThemeVariant to apply for each layer index
                Dictionary<int, PresetThemeVariantValue> layerVariantMap = new Dictionary<int, PresetThemeVariantValue>
                {
                    { 0, PresetThemeVariantValue.Variant1 },
                    { 1, PresetThemeVariantValue.Variant2 },
                    { 2, PresetThemeVariantValue.Variant3 }
                    // Add more mappings as needed
                };

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the layer membership string (e.g., "0;1")
                        string layerMember = shape.LayerMem.LayerMember.Value;

                        if (string.IsNullOrEmpty(layerMember))
                            continue; // Shape is not assigned to any layer

                        // Split the membership string into individual layer indexes
                        string[] parts = layerMember.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                        // Determine the first matching layer that has a variant mapping
                        foreach (string part in parts)
                        {
                            if (int.TryParse(part, out int layerIndex) && layerVariantMap.TryGetValue(layerIndex, out PresetThemeVariantValue variant))
                            {
                                // Apply a preset theme and the corresponding variant to the shape
                                shape.PresetTheme = PresetThemeValue.Bubble;
                                shape.PresetThemeVariant = variant;
                                // Once a variant is applied, stop checking other layers for this shape
                                break;
                            }
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