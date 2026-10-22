using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputJsonPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Apply a preset theme to the first page (if any pages exist).
            if (diagram.Pages.Count > 0)
            {
                // The PresetTheme and PresetThemeVariant properties are write‑only.
                diagram.Pages[0].PresetTheme = PresetThemeValue.Bubble;
                diagram.Pages[0].PresetThemeVariant = PresetThemeVariantValue.Variant1;
            }

            // Extract the diagram's color palette after the theme is applied.
            var palette = new List<object>();
            foreach (ColorEntry entry in diagram.Colors)
            {
                // ColorEntry.Color returns Aspose.Drawing.Color.
                Aspose.Drawing.Color c = entry.Color;
                string hex = $"#{c.R:X2}{c.G:X2}{c.B:X2}";
                palette.Add(new { Index = entry.IX, Hex = hex });
            }

            // Serialize the palette to JSON.
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(palette, jsonOptions);

            // Write JSON to the specified output file.
            File.WriteAllText(outputPath, json);
            Console.WriteLine($"Theme color palette extracted to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}