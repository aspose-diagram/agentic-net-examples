using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: diagram path, CSV data path, output diagram path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <csvPath> <outputPath>");
            return;
        }

        string diagramPath = args[0];
        string csvPath = args[1];
        string outputPath = args[2];

        // Guard against missing files
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Load CSV data into a dictionary mapping shape IDs to values
        var shapeValueMap = new Dictionary<long, string>();
        try
        {
            // Simple CSV parsing: each line "shapeId,value"
            foreach (var line in File.ReadAllLines(csvPath))
            {
                // Skip empty lines
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Split by comma
                var parts = line.Split(',');
                if (parts.Length < 2)
                    continue; // malformed line

                // Parse shape ID
                if (long.TryParse(parts[0].Trim(), out long shapeId))
                {
                    string value = parts[1].Trim();
                    shapeValueMap[shapeId] = value;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading CSV file: {ex.Message}");
            return;
        }

        // Load the Visio diagram and apply themes based on CSV data
        try
        {
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                // Iterate shapes using indexer (Shapes collection)
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];

                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check if this shape ID has a corresponding CSV entry
                    if (shapeValueMap.TryGetValue(shape.ID, out string dataValue))
                    {
                        // Apply a theme based on the data value
                        // Example mapping: "High" => Bubble theme, "Medium" => Office theme, else default
                        if (string.Equals(dataValue, "High", StringComparison.OrdinalIgnoreCase))
                        {
                            // Apply Bubble theme with Variant3 and QuickStyle2
                            shape.PresetTheme = PresetThemeValue.Bubble;
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant3;
                            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;
                        }
                        else if (string.Equals(dataValue, "Medium", StringComparison.OrdinalIgnoreCase))
                        {
                            // Apply Office theme with Variant1 and QuickStyle1
                            shape.PresetTheme = PresetThemeValue.Office;
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;
                        }
                        else
                        {
                            // Apply a default theme (e.g., Simple theme)
                            shape.PresetTheme = PresetThemeValue.Simple;
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant2;
                            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle3;
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}