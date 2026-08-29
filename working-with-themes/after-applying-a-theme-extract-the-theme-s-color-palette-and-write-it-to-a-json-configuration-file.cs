using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths (replace with actual file locations as needed)
                string diagramPath = "input.vsdx";
                string jsonOutputPath = "themeColors.json";
                string savedDiagramPath = "output_with_theme.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Apply a preset theme to the first page (example: Bubble theme)
                if (diagram.Pages.Count > 0)
                {
                    diagram.Pages[0].PresetTheme = PresetThemeValue.Bubble;
                }

                // Extract the document's color palette
                List<string> palette = new List<string>();
                foreach (var color in diagram.Colors)
                {
                    // Safe string representation of each color entry
                    palette.Add(color.ToString());
                }

                // Prepare an object for JSON serialization
                var themeConfig = new
                {
                    ThemeColors = palette
                };

                // Serialize to formatted JSON
                string json = JsonSerializer.Serialize(themeConfig, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to file
                File.WriteAllText(jsonOutputPath, json);

                // Optionally save the diagram with the applied theme
                diagram.Save(savedDiagramPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }