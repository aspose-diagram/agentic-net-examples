using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            Diagram diagram = new Diagram(inputPath);

            var themeMap = new Dictionary<long, (PresetThemeValue Theme, PresetThemeVariantValue Variant, PresetQuickStyleValue QuickStyle)>
            {
                { 5,  (PresetThemeValue.Bubble, PresetThemeVariantValue.Variant1, PresetQuickStyleValue.VariantStyle1) },
                { 12, (PresetThemeValue.Bubble, PresetThemeVariantValue.Variant2, PresetQuickStyleValue.VariantStyle3) },
                { 23, (PresetThemeValue.Bubble, PresetThemeVariantValue.Variant3, PresetQuickStyleValue.VariantStyle2) }
                // Add more mappings as needed
            };

            Page page = diagram.Pages[0];
            foreach (var kvp in themeMap)
            {
                long shapeId = kvp.Key;
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.WriteLine($"Shape with ID {shapeId} not found.");
                    continue;
                }

                shape.PresetTheme = kvp.Value.Theme;
                shape.PresetThemeVariant = kvp.Value.Variant;
                shape.PresetThemeQuickStyle = kvp.Value.QuickStyle;
            }

            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Theme mapping applied and diagram saved successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}