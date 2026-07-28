using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Apply a preset theme to the first page (example: Bubble theme)
                if (diagram.Pages.Count > 0)
                {
                    Page page = diagram.Pages[0];
                    page.PresetTheme = PresetThemeValue.Bubble;
                }

                // Extract the document's color palette after applying the theme
                List<string> palette = new List<string>();
                foreach (var color in diagram.Colors)
                {
                    // Safe string representation of the color entry
                    palette.Add(color.ToString());
                }

                // Serialize the palette to JSON with indentation
                string json = JsonSerializer.Serialize(palette, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to a configuration file
                string outputJsonPath = "themeColors.json";
                File.WriteAllText(outputJsonPath, json);

                // Optional: Save the diagram with the applied theme
                string outputDiagramPath = "output.vsdx";
                diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Theme applied, palette extracted to " + outputJsonPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }