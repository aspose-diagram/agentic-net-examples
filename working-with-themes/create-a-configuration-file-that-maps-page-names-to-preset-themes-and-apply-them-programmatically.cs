using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Simple configuration entry representing a theme assignment for a page.
    private class ThemeConfig
    {
        public PresetThemeValue Theme { get; set; }
        public PresetThemeVariantValue Variant { get; set; }
    }

    static void Main(string[] args)
    {
        // Expected arguments: <diagramPath> <configPath> <outputPath>
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <configPath> <outputPath>");
            return;
        }

        string diagramPath = args[0];
        string configPath = args[1];
        string outputPath = args[2];

        // Guard against missing files.
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

        // Load configuration mapping page names to theme settings.
        Dictionary<string, ThemeConfig> themeMap = LoadConfiguration(configPath);
        if (themeMap == null)
        {
            // LoadConfiguration already reported the error.
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern).
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate over all pages and apply themes based on the configuration.
                foreach (Page page in diagram.Pages)
                {
                    // Page names are stored directly in the Name property.
                    string pageName = page.Name ?? string.Empty;

                    if (themeMap.TryGetValue(pageName, out ThemeConfig config))
                    {
                        // Apply the preset theme and variant to the page.
                        page.PresetTheme = config.Theme;
                        page.PresetThemeVariant = config.Variant;
                        Console.WriteLine($"Applied theme {config.Theme} (variant {config.Variant}) to page '{pageName}'.");
                    }
                    else
                    {
                        Console.WriteLine($"No theme mapping found for page '{pageName}'. Skipping.");
                    }
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Loads a simple key=value configuration file.
    // Expected format per line: PageName=ThemeName,VariantName
    // Example: "Intro=Bubble,Variant2"
    private static Dictionary<string, ThemeConfig> LoadConfiguration(string configPath)
    {
        var map = new Dictionary<string, ThemeConfig>(StringComparer.OrdinalIgnoreCase);
        try
        {
            foreach (string rawLine in File.ReadAllLines(configPath))
            {
                string line = rawLine.Trim();

                // Skip empty lines and comments.
                if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    continue;

                // Split into key and value.
                int eqIndex = line.IndexOf('=');
                if (eqIndex <= 0 || eqIndex == line.Length - 1)
                {
                    Console.Error.WriteLine($"Invalid config line (missing '='): {line}");
                    continue;
                }

                string pageName = line.Substring(0, eqIndex).Trim();
                string valuePart = line.Substring(eqIndex + 1).Trim();

                // Split value into theme and variant.
                string[] parts = valuePart.Split(',');
                if (parts.Length != 2)
                {
                    Console.Error.WriteLine($"Invalid config line (expected 'Theme,Variant'): {line}");
                    continue;
                }

                string themeStr = parts[0].Trim();
                string variantStr = parts[1].Trim();

                // Parse enums safely.
                if (!Enum.TryParse<PresetThemeValue>(themeStr, true, out PresetThemeValue theme))
                {
                    Console.Error.WriteLine($"Unknown theme '{themeStr}' in line: {line}");
                    continue;
                }

                if (!Enum.TryParse<PresetThemeVariantValue>(variantStr, true, out PresetThemeVariantValue variant))
                {
                    Console.Error.WriteLine($"Unknown variant '{variantStr}' in line: {line}");
                    continue;
                }

                map[pageName] = new ThemeConfig { Theme = theme, Variant = variant };
            }

            return map;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration file: {ex.Message}");
            return null;
        }
    }
}