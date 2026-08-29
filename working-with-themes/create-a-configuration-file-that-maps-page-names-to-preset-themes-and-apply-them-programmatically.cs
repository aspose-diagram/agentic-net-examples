using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramThemeApplier
{
    // Model representing a mapping entry in the configuration file
    public class PageThemeMapping
    {
        public string PageName { get; set; }
        public string Theme { get; set; }               // Name of PresetThemeValue enum (e.g., "Office")
        public string Variant { get; set; }              // Optional: name of PresetThemeVariantValue enum
        public string QuickStyle { get; set; }           // Optional: name of PresetQuickStyleValue enum
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths – adjust as needed
                string diagramPath = "input.vdx";
                string outputPath = "output.vdx";
                string configPath = "pageThemes.json";

                // Load configuration file (JSON)
                // Example content:
                // [
                //   { "PageName": "Page-1", "Theme": "Office", "Variant": "Variant1", "QuickStyle": "VariantStyle2" },
                //   { "PageName": "Page-2", "Theme": "Linear" }
                // ]
                List<PageThemeMapping> mappings = JsonSerializer.Deserialize<List<PageThemeMapping>>(
                    File.ReadAllText(configPath));

                // Load the diagram using Aspose.Diagram (lifecycle rule: load)
                Diagram diagram = new Diagram(diagramPath);

                // Apply themes based on configuration
                foreach (Page page in diagram.Pages)
                {
                    // Find a mapping for the current page name
                    PageThemeMapping map = mappings.Find(m => string.Equals(m.PageName, page.Name, StringComparison.OrdinalIgnoreCase));
                    if (map == null) continue; // No mapping – skip

                    // Set the preset theme
                    if (!string.IsNullOrWhiteSpace(map.Theme))
                    {
                        // Convert string to PresetThemeValue enum
                        if (Enum.TryParse<PresetThemeValue>(map.Theme, ignoreCase: true, out var themeValue))
                        {
                            page.PresetTheme = themeValue;
                        }
                    }

                    // Optional: set theme variant
                    if (!string.IsNullOrWhiteSpace(map.Variant))
                    {
                        if (Enum.TryParse<PresetThemeVariantValue>(map.Variant, ignoreCase: true, out var variantValue))
                        {
                            page.PresetThemeVariant = variantValue;
                        }
                    }

                    // Optional: set quick style
                    if (!string.IsNullOrWhiteSpace(map.QuickStyle))
                    {
                        if (Enum.TryParse<PresetQuickStyleValue>(map.QuickStyle, ignoreCase: true, out var quickStyleValue))
                        {
                            page.PresetThemeQuickStyle = quickStyleValue;
                        }
                    }
                }

                // Save the modified diagram (lifecycle rule: save)
                diagram.Save(outputPath, SaveFileFormat.Vdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}