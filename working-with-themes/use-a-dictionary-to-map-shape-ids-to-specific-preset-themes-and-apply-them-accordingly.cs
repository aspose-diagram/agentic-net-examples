using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (use first argument if provided)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path (use second argument if provided)
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        Diagram diagram;
        try
        {
            // Load the diagram from the specified file
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Map of shape IDs to the preset theme that should be applied
        var themeMap = new Dictionary<long, PresetThemeValue>
        {
            // Example mappings – replace with actual IDs and themes as needed
            { 1, PresetThemeValue.Bubble },
            { 2, PresetThemeValue.Bubble },
            { 5, PresetThemeValue.Bubble }
        };

        // Apply the preset theme to each shape identified in the dictionary
        foreach (var kvp in themeMap)
        {
            long shapeId = kvp.Key;
            PresetThemeValue theme = kvp.Value;

            try
            {
                // Retrieve the shape from the first page (adjust if shapes are on other pages)
                Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found.");
                    continue;
                }

                // Apply the preset theme to the shape (write‑only property)
                shape.PresetTheme = theme;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing shape ID {shapeId}: {ex.Message}");
            }
        }

        try
        {
            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}