using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input file path from arguments or prompt the user
        string inputPath = args.Length > 0 ? args[0] : "";
        if (string.IsNullOrWhiteSpace(inputPath))
        {
            Console.Write("Enter the path to the Visio file: ");
            inputPath = Console.ReadLine();
        }

        // Guard against missing file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the diagram inside a try/catch block
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Iterate through each page in the document
        foreach (Page page in diagram.Pages)
        {
            // Iterate through each shape on the current page
            foreach (Shape shape in page.Shapes)
            {
                // Retrieve the layer membership string (semicolon‑separated indexes)
                string layerMember = shape.LayerMem.LayerMember.Value;
                if (string.IsNullOrEmpty(layerMember))
                    continue; // Shape is not assigned to any layer

                // Split the membership string into individual layer indexes
                string[] indexes = layerMember.Split(';', StringSplitOptions.RemoveEmptyEntries);

                // Process each layer index the shape belongs to
                foreach (string idxStr in indexes)
                {
                    if (!int.TryParse(idxStr, out int layerIdx))
                        continue; // Skip invalid entries

                    // Find the corresponding Layer object by its index (IX)
                    Layer targetLayer = null;
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        if (layer.IX == layerIdx)
                        {
                            targetLayer = layer;
                            break;
                        }
                    }

                    if (targetLayer == null)
                        continue; // Layer not found, move to next index

                    // Determine theme variant based on the layer's name
                    string layerName = targetLayer.Name.Value;
                    if (layerName.Equals("Layer1", StringComparison.OrdinalIgnoreCase))
                    {
                        shape.PresetTheme = PresetThemeValue.Bubble;
                        shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                    }
                    else if (layerName.Equals("Layer2", StringComparison.OrdinalIgnoreCase))
                    {
                        shape.PresetTheme = PresetThemeValue.Bubble;
                        shape.PresetThemeVariant = PresetThemeVariantValue.Variant2;
                    }
                    else
                    {
                        shape.PresetTheme = PresetThemeValue.Bubble;
                        shape.PresetThemeVariant = PresetThemeVariantValue.Variant3;
                    }

                    // Theme applied – no need to evaluate additional layers for this shape
                    break;
                }
            }
        }

        // Build output file path
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_Themed.vsdx");

        // Save the modified diagram
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}