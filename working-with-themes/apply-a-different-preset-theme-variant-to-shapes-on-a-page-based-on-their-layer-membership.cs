using System;
using System.IO;
using Aspose.Diagram;

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
            using Diagram diagram = new Diagram(inputPath);

            // Iterate over all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate over all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the layer membership string (e.g., "0;2")
                    string layerMember = shape.LayerMem.LayerMember.Value;

                    // If the shape is not assigned to any layer, skip it
                    if (string.IsNullOrWhiteSpace(layerMember))
                        continue;

                    // Split the membership string into individual layer indexes
                    string[] parts = layerMember.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    // Determine the variant based on the first layer the shape belongs to
                    PresetThemeVariantValue variant = PresetThemeVariantValue.Variant3; // default

                    foreach (string part in parts)
                    {
                        if (int.TryParse(part, out int layerIndex))
                        {
                            // Example mapping: layer 0 → Variant1, layer 1 → Variant2, others → Variant3
                            if (layerIndex == 0)
                                variant = PresetThemeVariantValue.Variant1;
                            else if (layerIndex == 1)
                                variant = PresetThemeVariantValue.Variant2;
                            else
                                variant = PresetThemeVariantValue.Variant3;

                            // Once a matching layer is found, stop checking further layers
                            break;
                        }
                    }

                    // Apply a base preset theme (required before setting a variant)
                    shape.PresetTheme = PresetThemeValue.Bubble;

                    // Apply the selected variant to the shape
                    shape.PresetThemeVariant = variant;
                }
            }

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}