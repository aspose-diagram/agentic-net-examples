using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramThemeApplier
{
    // Represents theme settings for a specific shape master.
    public class ThemeConfig
    {
        public string Theme { get; set; } = "Bubble";
        public string Variant { get; set; } = "Variant1";
        public string QuickStyle { get; set; } = "VariantStyle1";
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths can be supplied via command‑line arguments or hard‑coded for simplicity.
                string diagramPath = "input.vsdx";
                string configPath = "themeConfig.json";
                string outputPath = "output.vsdx";

                if (args.Length >= 1) diagramPath = args[0];
                if (args.Length >= 2) configPath = args[1];
                if (args.Length >= 3) outputPath = args[2];

                // Load the diagram.
                Diagram diagram = new Diagram(diagramPath);

                // Load theme configuration from JSON file.
                Dictionary<string, ThemeConfig> themeMap = LoadThemeConfiguration(configPath);

                // Apply themes to shapes based on their master name.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has an associated master.
                        if (shape.Master == null) continue;

                        string masterName = shape.Master.Name;
                        if (!themeMap.TryGetValue(masterName, out ThemeConfig cfg)) continue;

                        // Parse and assign the theme enum values.
                        if (Enum.TryParse<PresetThemeValue>(cfg.Theme, out var themeEnum))
                        {
                            shape.PresetTheme = themeEnum;
                        }

                        if (Enum.TryParse<PresetThemeVariantValue>(cfg.Variant, out var variantEnum))
                        {
                            shape.PresetThemeVariant = variantEnum;
                        }

                        if (Enum.TryParse<PresetQuickStyleValue>(cfg.QuickStyle, out var quickStyleEnum))
                        {
                            shape.PresetThemeQuickStyle = quickStyleEnum;
                        }
                    }
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }

        // Reads the JSON configuration file into a dictionary.
        private static Dictionary<string, ThemeConfig> LoadThemeConfiguration(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"Configuration file '{path}' not found. No themes will be applied.");
                return new Dictionary<string, ThemeConfig>();
            }

            try
            {
                string json = File.ReadAllText(path);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<Dictionary<string, ThemeConfig>>(json, options)
                       ?? new Dictionary<string, ThemeConfig>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read configuration: {ex.Message}");
                return new Dictionary<string, ThemeConfig>();
            }
        }
    }
}