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

                // Input Visio file path
                string diagramPath = "input.vsdx";

                // Output JSON file path
                string jsonPath = "themeColors.json";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Apply a preset theme to the first page (example: Bubble theme)
                // Note: PresetTheme and PresetThemeVariant are write‑only properties.
                if (diagram.Pages.Count > 0)
                {
                    diagram.Pages[0].PresetTheme = PresetThemeValue.Bubble;
                    diagram.Pages[0].PresetThemeVariant = PresetThemeVariantValue.Variant1;
                }

                // Extract the document's color palette after applying the theme
                var palette = new List<object>();
                foreach (var colorEntry in diagram.Colors)
                {
                    // ColorEntry does not expose individual fields; use ToString() for a readable representation
                    palette.Add(new
                    {
                        // Index is optional; if unavailable, it can be omitted
                        Value = colorEntry.ToString()
                    });
                }

                // Serialize the palette to JSON with indentation for readability
                string json = JsonSerializer.Serialize(palette, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to the specified file
                File.WriteAllText(jsonPath, json);

                Console.WriteLine($"Theme color palette has been written to '{jsonPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }