using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    // Container for theme settings per shape type
    private class ThemeConfig
    {
        public PresetThemeValue PresetTheme { get; set; }
        public PresetThemeVariantValue? Variant { get; set; }
        public PresetQuickStyleValue? QuickStyle { get; set; }
    }

    static void Main(string[] args)
    {
        // Expect three arguments: diagram file, config file, output file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <configPath> <outputPath>");
            return;
        }

        string diagramPath = args[0];
        string configPath = args[1];
        string outputPath = args[2];

        // Guard against missing files
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Load the diagram inside a using block with error handling
        Diagram diagram;
        try
        {
            diagram = new Diagram(diagramPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Parse configuration file into a dictionary keyed by shape TypeValue
        // Expected line format (ignore empty lines and lines starting with '#'):
        // ShapeType=PresetTheme,Variant,QuickStyle
        var themeMap = new Dictionary<TypeValue, ThemeConfig>();
        try
        {
            foreach (string rawLine in File.ReadAllLines(configPath))
            {
                string line = rawLine.Trim();
                if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    continue; // Skip comments/blank lines

                string[] parts = line.Split('=', 2);
                if (parts.Length != 2)
                    continue; // Invalid line, skip

                string shapeTypeStr = parts[0].Trim();
                string[] values = parts[1].Split(',', StringSplitOptions.RemoveEmptyEntries);
                if (values.Length < 1)
                    continue; // No theme specified, skip

                // Parse shape type enum
                if (!Enum.TryParse(shapeTypeStr, true, out TypeValue shapeType))
                    continue; // Unknown shape type, skip

                // Parse preset theme (mandatory)
                if (!Enum.TryParse(values[0].Trim(), true, out PresetThemeValue presetTheme))
                    continue; // Invalid theme, skip

                // Parse variant (optional)
                PresetThemeVariantValue? variant = null;
                if (values.Length > 1 && Enum.TryParse(values[1].Trim(), true, out PresetThemeVariantValue parsedVariant))
                    variant = parsedVariant;

                // Parse quick style (optional)
                PresetQuickStyleValue? quickStyle = null;
                if (values.Length > 2 && Enum.TryParse(values[2].Trim(), true, out PresetQuickStyleValue parsedQuickStyle))
                    quickStyle = parsedQuickStyle;

                // Store in dictionary
                themeMap[shapeType] = new ThemeConfig
                {
                    PresetTheme = presetTheme,
                    Variant = variant,
                    QuickStyle = quickStyle
                };
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading configuration: {ex.Message}");
            return;
        }

        // Apply themes to shapes based on their TypeValue
        try
        {
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (themeMap.TryGetValue(shape.Type, out ThemeConfig cfg))
                    {
                        // Apply preset theme (write‑only)
                        shape.PresetTheme = cfg.PresetTheme;

                        // Apply variant if provided
                        if (cfg.Variant.HasValue)
                            shape.PresetThemeVariant = cfg.Variant.Value;

                        // Apply quick style if provided
                        if (cfg.QuickStyle.HasValue)
                            shape.PresetThemeQuickStyle = cfg.QuickStyle.Value;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error applying themes: {ex.Message}");
            return;
        }

        // Save the modified diagram
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}