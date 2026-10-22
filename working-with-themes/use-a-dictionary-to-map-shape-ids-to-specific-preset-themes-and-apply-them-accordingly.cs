using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output Visio file path
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Dictionary mapping shape IDs (long) to desired preset themes
            // Example entries – replace with actual IDs and themes as needed
            var shapeThemeMap = new Dictionary<long, PresetThemeValue>
            {
                { 1, PresetThemeValue.Bubble },
                { 2, PresetThemeValue.Bubble } // using the same theme for demo; adjust as required
            };

            // Iterate over the mapping and apply themes to corresponding shapes
            foreach (var kvp in shapeThemeMap)
            {
                long shapeId = kvp.Key;
                PresetThemeValue theme = kvp.Value;

                // Retrieve the shape by ID from the first page
                Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on page 0.");
                    continue;
                }

                // Apply the preset theme to the shape
                shape.PresetTheme = theme;

                // Optionally set a variant (using Variant1 as default)
                shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            }

            // Save the modified diagram to the output file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}