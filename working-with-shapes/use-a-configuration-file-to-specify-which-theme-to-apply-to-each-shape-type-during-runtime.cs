using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramThemeApplier
{
    // Configuration model matching the JSON structure
    public class ShapeThemeConfig
    {
        public string Theme { get; set; }
        public int Style { get; set; }          // Corresponds to PresetStyleMatricsValue (1‑6)
        public int Color { get; set; }          // Corresponds to PresetColorMatricsValue (200‑206)
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths – adjust as needed
                string diagramPath = "input.vsdx";
                string configPath = "themeConfig.json";
                string outputPath = "output.vsdx";

                // Load the diagram (lifecycle rule: load)
                Diagram diagram = new Diagram(diagramPath);

                // Read and deserialize the configuration file
                var configJson = File.ReadAllText(configPath);
                var themeMap = JsonSerializer.Deserialize<Dictionary<string, ShapeThemeConfig>>(configJson);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Use the shape's universal name (NameU) as the key to look up its theme
                        if (shape.NameU != null && themeMap.TryGetValue(shape.NameU, out ShapeThemeConfig cfg))
                        {
                            // Apply the preset theme
                            if (Enum.TryParse<PresetThemeValue>(cfg.Theme, out var themeEnum))
                            {
                                shape.PresetTheme = themeEnum;
                            }

                            // Apply the style matrix (style row + color column)
                            // Convert integer values to the corresponding enum members
                            PresetStyleMatricsValue styleEnum = cfg.Style switch
                            {
                                1 => PresetStyleMatricsValue.Style1,
                                2 => PresetStyleMatricsValue.Style2,
                                3 => PresetStyleMatricsValue.Style3,
                                4 => PresetStyleMatricsValue.Style4,
                                5 => PresetStyleMatricsValue.Style5,
                                6 => PresetStyleMatricsValue.Style6,
                                _ => PresetStyleMatricsValue.Style1
                            };

                            PresetColorMatricsValue colorEnum = cfg.Color switch
                            {
                                200 => PresetColorMatricsValue.Color1,
                                201 => PresetColorMatricsValue.Color2,
                                202 => PresetColorMatricsValue.Color3,
                                203 => PresetColorMatricsValue.Color4,
                                204 => PresetColorMatricsValue.Color5,
                                205 => PresetColorMatricsValue.Color6,
                                206 => PresetColorMatricsValue.Color7,
                                _ => PresetColorMatricsValue.Color1
                            };

                            shape.SetPresetThemeStyleMatrics(styleEnum, colorEnum);
                        }
                    }
                }

                // Save the modified diagram (lifecycle rule: save)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}