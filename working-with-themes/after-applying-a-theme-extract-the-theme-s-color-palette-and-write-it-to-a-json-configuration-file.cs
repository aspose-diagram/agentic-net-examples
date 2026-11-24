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

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Apply a preset theme to the first page (example: Bubble theme, Variant1).
            if (diagram.Pages.Count > 0)
            {
                Page firstPage = diagram.Pages[0];
                firstPage.PresetTheme = PresetThemeValue.Bubble;
                firstPage.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            }

            // Extract the diagram's color palette after applying the theme.
            var palette = new List<Dictionary<string, string>>();

            foreach (ColorEntry entry in diagram.Colors)
            {
                // Use the safe ToString() representation of the color.
                string colorString = entry.Color.ToString();

                var colorInfo = new Dictionary<string, string>
                {
                    { "Color", colorString }
                };
                palette.Add(colorInfo);
            }

            // Serialize the palette to JSON.
            string json = JsonSerializer.Serialize(palette, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to the specified output file.
            File.WriteAllText(outputPath, json);
            Console.WriteLine($"Theme color palette has been written to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}