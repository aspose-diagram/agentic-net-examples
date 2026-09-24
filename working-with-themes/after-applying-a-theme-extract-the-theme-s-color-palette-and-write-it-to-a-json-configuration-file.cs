using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Apply a preset theme to the first page
            Page page = diagram.Pages[0];
            page.PresetTheme = PresetThemeValue.Bubble;
            page.PresetThemeVariant = PresetThemeVariantValue.Variant1;

            // (Optional) Save the diagram after applying the theme
            diagram.Save("themed_output.vsdx", SaveFileFormat.Vsdx);

            // Extract the color palette from the diagram
            var palette = new List<object>();
            foreach (ColorEntry entry in diagram.Colors)
            {
                Aspose.Drawing.Color aspColor = entry.Color;
                string hex = $"#{aspColor.R:X2}{aspColor.G:X2}{aspColor.B:X2}";
                palette.Add(new { Index = entry.IX, Hex = hex });
            }

            // Serialize the palette to JSON
            string json = JsonSerializer.Serialize(palette, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to a configuration file
            string outputJsonPath = "themeColors.json";
            File.WriteAllText(outputJsonPath, json);

            Console.WriteLine($"Theme color palette extracted to '{outputJsonPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
