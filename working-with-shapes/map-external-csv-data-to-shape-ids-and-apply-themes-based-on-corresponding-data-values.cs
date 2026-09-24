using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths – adjust as needed
            string diagramPath = "input.vsdx";
            string csvPath = "mapping.csv";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Read CSV data (expected format: ShapeId,ThemeVariant)
            var mappings = LoadCsvMappings(csvPath);

            // Apply themes based on CSV values
            foreach (var mapping in mappings)
            {
                long shapeId = mapping.Key;
                PresetThemeVariantValue variant = mapping.Value;

                // Find the shape by ID on the first page (adjust if needed)
                Shape shape = null;
                foreach (Page page in diagram.Pages)
                {
                    shape = page.Shapes.GetShape(shapeId);
                    if (shape != null)
                        break;
                }

                if (shape == null)
                {
                    Console.WriteLine($"Shape with ID {shapeId} not found.");
                    continue;
                }

                // Apply a preset theme (Bubble) and the variant from CSV
                shape.PresetTheme = PresetThemeValue.Bubble;
                shape.PresetThemeVariant = variant;
                // Optional: set a quick style
                shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Csv);
            Console.WriteLine("Diagram saved to " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Loads CSV and returns a dictionary of ShapeId -> ThemeVariant
    private static Dictionary<long, PresetThemeVariantValue> LoadCsvMappings(string csvFilePath)
    {
        var result = new Dictionary<long, PresetThemeVariantValue>();

        if (!File.Exists(csvFilePath))
        {
            Console.WriteLine("CSV file not found: " + csvFilePath);
            return result;
        }

        var lines = File.ReadAllLines(csvFilePath);
        foreach (var line in lines)
        {
            // Skip empty lines and header (assumes header contains non-numeric first token)
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var parts = line.Split(',');
            if (parts.Length < 2)
                continue;

            // Try parsing shape ID
            if (!long.TryParse(parts[0].Trim(), out long shapeId))
                continue;

            // Try parsing variant number (1‑4)
            if (!int.TryParse(parts[1].Trim(), out int variantNum))
                continue;

            PresetThemeVariantValue variant = GetVariantFromNumber(variantNum);
            result[shapeId] = variant;
        }

        return result;
    }

    // Maps integer 1‑4 to the corresponding PresetThemeVariantValue enum
    private static PresetThemeVariantValue GetVariantFromNumber(int number)
    {
        return number switch
        {
            1 => PresetThemeVariantValue.Variant1,
            2 => PresetThemeVariantValue.Variant2,
            3 => PresetThemeVariantValue.Variant3,
            4 => PresetThemeVariantValue.Variant4,
            _ => PresetThemeVariantValue.Variant1 // default fallback
        };
    }
}
