using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path. If not provided, create a new file name with suffix.
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_Themed.vsdx");

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define a mapping from layer index to a preset theme.
                // Only enum members that exist in the API are used.
                var layerThemeMap = new Dictionary<int, PresetThemeValue>
                {
                    { 0, PresetThemeValue.Bubble },   // Layer 0 -> Bubble theme
                    { 1, PresetThemeValue.Clouds }    // Layer 1 -> Clouds theme
                    // Add more mappings as needed, using valid PresetThemeValue members.
                };

                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the layer membership string (semicolon-separated indices).
                        string layerMember = shape.LayerMem.LayerMember.Value;
                        if (string.IsNullOrEmpty(layerMember))
                            continue;

                        // Split the membership string into individual layer indices.
                        string[] parts = layerMember.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string part in parts)
                        {
                            // Try to parse the layer index.
                            if (int.TryParse(part.Trim(), out int layerIndex))
                            {
                                // If a theme is defined for this layer, apply it to the shape.
                                if (layerThemeMap.TryGetValue(layerIndex, out PresetThemeValue theme))
                                {
                                    shape.PresetTheme = theme;
                                    // Once a theme is applied based on the first matching layer, exit the loop.
                                    break;
                                }
                            }
                        }
                    }
                }

                // Save the modified diagram to the output file using VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}