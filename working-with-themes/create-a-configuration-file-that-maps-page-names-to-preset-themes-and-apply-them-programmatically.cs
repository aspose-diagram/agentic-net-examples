using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    // Represents a theme configuration for a page.
    private class ThemeConfig
    {
        public string Theme { get; set; }
        public string Variant { get; set; }
    }

    static void Main(string[] args)
    {
        // Expect three arguments: diagram file, config file, output file.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <configPath> <outputPath>");
            return;
        }

        string diagramPath = args[0];
        string configPath = args[1];
        string outputPath = args[2];

        // Guard: ensure diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Guard: ensure configuration file exists.
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Load theme configuration from JSON.
        Dictionary<string, ThemeConfig> themeMap;
        try
        {
            string json = File.ReadAllText(configPath);
            themeMap = JsonSerializer.Deserialize<Dictionary<string, ThemeConfig>>(json);
            if (themeMap == null)
            {
                Console.Error.WriteLine("Failed to deserialize configuration file.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading configuration: {ex.Message}");
            return;
        }

        // Load the Visio diagram, apply themes, and save the result.
        try
        {
            // Diagram must be instantiated inside a using block.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate over all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Page names are stored directly in page.Name.
                    string pageName = page.Name ?? string.Empty;

                    if (themeMap.TryGetValue(pageName, out ThemeConfig cfg))
                    {
                        // Parse the theme enum value.
                        if (Enum.TryParse<PresetThemeValue>(cfg.Theme, true, out var themeEnum))
                        {
                            page.PresetTheme = themeEnum;
                        }
                        else
                        {
                            Console.Error.WriteLine($"Invalid theme '{cfg.Theme}' for page '{pageName}'. Skipping.");
                            continue;
                        }

                        // Parse the variant enum value.
                        if (Enum.TryParse<PresetThemeVariantValue>(cfg.Variant, true, out var variantEnum))
                        {
                            page.PresetThemeVariant = variantEnum;
                        }
                        else
                        {
                            Console.Error.WriteLine($"Invalid variant '{cfg.Variant}' for page '{pageName}'. Skipping variant.");
                        }
                    }
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved with applied themes to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}