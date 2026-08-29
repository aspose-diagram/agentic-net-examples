using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramThemeApplier
{
    // Represents theme settings for a specific shape type
    public class ThemeConfig
    {
        public string Theme { get; set; }
        public string Variant { get; set; }
        public string QuickStyle { get; set; }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            // Expected arguments: <inputVisioPath> <outputVisioPath> <configJsonPath>
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: DiagramThemeApplier <inputVisioPath> <outputVisioPath> <configJsonPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];
            string configPath = args[2];

            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            if (!File.Exists(configPath))
            {
                Console.WriteLine($"Config file not found: {configPath}");
                return;
            }

            // Load configuration mapping shape master names to theme settings
            Dictionary<string, ThemeConfig> themeMap;
            try
            {
                string json = File.ReadAllText(configPath);
                themeMap = JsonSerializer.Deserialize<Dictionary<string, ThemeConfig>>(json);
                if (themeMap == null)
                {
                    Console.WriteLine("Configuration file is empty or invalid.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read configuration: {ex.Message}");
                return;
            }

            // Load the Visio diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Iterate through all pages and shapes, applying themes based on the configuration
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has an associated master (i.e., it is not a group or guide)
                    if (shape.Master == null)
                        continue;

                    string masterName = shape.Master.Name;
                    if (string.IsNullOrEmpty(masterName))
                        continue;

                    if (!themeMap.TryGetValue(masterName, out ThemeConfig cfg))
                        continue; // No theme defined for this shape type

                    // Parse enum values safely; if parsing fails, skip applying that part
                    if (Enum.TryParse<PresetThemeValue>(cfg.Theme, true, out var themeValue))
                    {
                        shape.PresetTheme = themeValue;
                    }

                    if (Enum.TryParse<PresetThemeVariantValue>(cfg.Variant, true, out var variantValue))
                    {
                        shape.PresetThemeVariant = variantValue;
                    }

                    if (Enum.TryParse<PresetQuickStyleValue>(cfg.QuickStyle, true, out var quickStyleValue))
                    {
                        shape.PresetThemeQuickStyle = quickStyleValue;
                    }
                }
            }

            // Save the modified diagram
            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save diagram: {ex.Message}");
            }
        }
    }
}