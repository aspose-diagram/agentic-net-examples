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

            // Load an existing Visio diagram
            using (Diagram diagram = new Diagram("input.vsdx"))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the layer membership string (e.g., "0;2")
                        string layerMember = shape.LayerMem.LayerMember.Value;

                        if (string.IsNullOrEmpty(layerMember))
                            continue; // Shape is not assigned to any layer

                        // Use the first layer index to decide the theme variant
                        string[] indices = layerMember.Split(';', StringSplitOptions.RemoveEmptyEntries);
                        if (indices.Length == 0)
                            continue;

                        int layerIndex;
                        if (!int.TryParse(indices[0], out layerIndex))
                            continue; // Invalid layer index format

                        // Apply a preset theme and variant based on the layer index
                        shape.PresetTheme = PresetThemeValue.Bubble; // Choose a base theme

                        switch (layerIndex)
                        {
                            case 0:
                                shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                                break;
                            case 1:
                                shape.PresetThemeVariant = PresetThemeVariantValue.Variant2;
                                break;
                            case 2:
                                shape.PresetThemeVariant = PresetThemeVariantValue.Variant3;
                                break;
                            default:
                                shape.PresetThemeVariant = PresetThemeVariantValue.Variant4;
                                break;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
