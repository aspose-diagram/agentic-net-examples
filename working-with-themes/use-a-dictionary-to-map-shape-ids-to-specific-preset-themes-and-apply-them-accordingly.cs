using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for save options and enums

class Program
{
    static void Main(string[] args)
    {
        // Define input file path
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Diagram diagram;
        try
        {
            // Load the existing Visio diagram
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            // Report any loading errors and exit
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Dictionary mapping shape IDs to preset themes
        Dictionary<long, PresetThemeValue> themeMap = new Dictionary<long, PresetThemeValue>
        {
            { 1, PresetThemeValue.Bubble },
            { 2, PresetThemeValue.Bubble }
            // Add more mappings as needed
        };

        try
        {
            // Apply the preset theme to each shape based on the dictionary
            foreach (KeyValuePair<long, PresetThemeValue> kvp in themeMap)
            {
                long shapeId = kvp.Key;
                PresetThemeValue theme = kvp.Value;

                // Retrieve the shape from the first page (adjust page index if necessary)
                Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);
                if (shape != null)
                {
                    // Apply the preset theme (write‑only property)
                    shape.PresetTheme = theme;
                }
                else
                {
                    Console.WriteLine($"Shape with ID {shapeId} not found.");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during theme application
            Console.Error.WriteLine($"Error applying themes: {ex.Message}");
            return;
        }

        try
        {
            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Report any errors that occur during saving
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}