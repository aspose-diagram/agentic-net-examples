using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramThemeApplier
{
    // Represents theme configuration for a specific shape master name
    public class ThemeConfig
    {
        public string Theme { get; set; } = string.Empty;          // PresetThemeValue name
        public string Variant { get; set; } = string.Empty;        // PresetThemeVariantValue name
        public string QuickStyle { get; set; } = string.Empty;    // PresetQuickStyleValue name
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {

                // Expected arguments: inputVisioPath configJsonPath outputVisioPath
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
                string configPath = args.Length > 1 ? args[1] : "themeConfig.json";
                string outputPath = args.Length > 2 ? args[2] : "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Load theme configuration from JSON file
                Dictionary<string, ThemeConfig> themeMap = new Dictionary<string, ThemeConfig>(StringComparer.OrdinalIgnoreCase);
                if (File.Exists(configPath))
                {
                    string json = File.ReadAllText(configPath);
                    var deserialized = JsonSerializer.Deserialize<Dictionary<string, ThemeConfig>>(json);
                    if (deserialized != null)
                    {
                        themeMap = deserialized;
                    }
                }
                else
                {
                    Console.WriteLine($"Configuration file not found: {configPath}");
                    return;
                }

                // Apply themes to shapes based on their master name
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Master == null) continue; // Skip shapes without a master

                        string masterName = shape.Master.Name;
                        if (themeMap.TryGetValue(masterName, out ThemeConfig cfg))
                        {
                            try
                            {
                                // Apply preset theme
                                shape.PresetTheme = Enum.Parse<PresetThemeValue>(cfg.Theme, true);
                                shape.PresetThemeVariant = Enum.Parse<PresetThemeVariantValue>(cfg.Variant, true);
                                shape.PresetThemeQuickStyle = Enum.Parse<PresetQuickStyleValue>(cfg.QuickStyle, true);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Failed to apply theme to shape ID {shape.ID} (Master: {masterName}): {ex.Message}");
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }
}