using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Read all lines from the CSV (assumes UTF-8 encoding)
            string[] csvLines = File.ReadAllLines(csvPath);

            // Process each line (skip header if present)
            for (int i = 0; i < csvLines.Length; i++)
            {
                string line = csvLines[i].Trim();
                if (string.IsNullOrEmpty(line))
                    continue; // Skip empty lines

                // Simple heuristic: if first line contains non-numeric first token, treat as header
                string[] parts = line.Split(',');
                if (parts.Length < 2)
                    continue; // Not enough data

                // Header detection
                if (i == 0 && !long.TryParse(parts[0], out _))
                    continue; // Skip header row

                // Parse shape ID
                if (!long.TryParse(parts[0], out long shapeId))
                {
                    Console.Error.WriteLine($"Invalid ShapeId at line {i + 1}: {parts[0]}");
                    continue;
                }

                // Expected CSV columns: ShapeId, Theme, Variant, QuickStyle
                string themeStr = parts.Length > 1 ? parts[1].Trim() : string.Empty;
                string variantStr = parts.Length > 2 ? parts[2].Trim() : string.Empty;
                string quickStyleStr = parts.Length > 3 ? parts[3].Trim() : string.Empty;

                // Locate the shape by ID across all pages
                Shape shape = FindShapeById(diagram, shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found in diagram.");
                    continue;
                }

                // Apply Preset Theme if provided
                if (!string.IsNullOrEmpty(themeStr) &&
                    Enum.TryParse<PresetThemeValue>(themeStr, true, out var themeVal))
                {
                    shape.PresetTheme = themeVal;
                }

                // Apply Preset Theme Variant if provided
                if (!string.IsNullOrEmpty(variantStr) &&
                    Enum.TryParse<PresetThemeVariantValue>(variantStr, true, out var variantVal))
                {
                    shape.PresetThemeVariant = variantVal;
                }

                // Apply Preset Quick Style if provided
                if (!string.IsNullOrEmpty(quickStyleStr) &&
                    Enum.TryParse<PresetQuickStyleValue>(quickStyleStr, true, out var quickStyleVal))
                {
                    shape.PresetThemeQuickStyle = quickStyleVal;
                }
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Searches all pages of the diagram for a shape with the specified ID.
    /// Returns null if not found.
    /// </summary>
    private static Shape FindShapeById(Diagram diagram, long shapeId)
    {
        foreach (Page page in diagram.Pages)
        {
            // Shapes collection provides GetShape(long id)
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape != null)
                return shape;
        }
        return null;
    }
}