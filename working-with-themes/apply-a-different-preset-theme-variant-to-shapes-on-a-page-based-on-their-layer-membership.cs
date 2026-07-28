using System.IO;
using System;
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
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the layer membership string (e.g., "0;1")
                        string layerMember = shape.LayerMem.LayerMember.Value;

                        // If the shape is not assigned to any layer, skip it
                        if (string.IsNullOrEmpty(layerMember))
                            continue;

                        // Use the first layer index for variant selection
                        string[] indices = layerMember.Split(';', StringSplitOptions.RemoveEmptyEntries);
                        if (indices.Length == 0)
                            continue;

                        if (!int.TryParse(indices[0], out int layerIndex))
                            continue;

                        // Determine the preset theme variant based on the layer index
                        PresetThemeVariantValue variant;
                        switch (layerIndex)
                        {
                            case 0:
                                variant = PresetThemeVariantValue.Variant1;
                                break;
                            case 1:
                                variant = PresetThemeVariantValue.Variant2;
                                break;
                            case 2:
                                variant = PresetThemeVariantValue.Variant3;
                                break;
                            default:
                                variant = PresetThemeVariantValue.Variant4;
                                break;
                        }

                        // Apply a preset theme and the selected variant to the shape
                        shape.PresetTheme = PresetThemeValue.Bubble;
                        shape.PresetThemeVariant = variant;
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
