using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: config file path, input diagram path, output diagram path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <config.json> <input.vsdx> <output.vsdx>");
            return;
        }

        string configPath = args[0];
        string inputDiagramPath = args[1];
        string outputDiagramPath = args[2];

        // Guard against missing files
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }
        if (!File.Exists(inputDiagramPath))
        {
            Console.Error.WriteLine($"Input diagram file not found: {inputDiagramPath}");
            return;
        }

        // Load configuration mapping shape type names to preset theme names
        Dictionary<string, string> themeMap;
        try
        {
            string json = File.ReadAllText(configPath);
            themeMap = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            if (themeMap == null)
            {
                Console.Error.WriteLine("Configuration file is empty or invalid.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read or parse configuration file: {ex.Message}");
            return;
        }

        // Load the Visio diagram
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputDiagramPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Iterate through all pages and shapes, applying themes based on shape type
        try
        {
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Determine the shape type as string (e.g., "Rectangle", "Ellipse")
                    string shapeTypeName = shape.Type.ToString();

                    // Check if a theme is defined for this shape type
                    if (themeMap.TryGetValue(shapeTypeName, out string themeName))
                    {
                        // Parse the theme name to the PresetThemeValue enum
                        if (Enum.TryParse<PresetThemeValue>(themeName, out PresetThemeValue presetTheme))
                        {
                            // Apply the preset theme to the shape
                            shape.PresetTheme = presetTheme;

                            // Optionally set a default variant (can be customized later)
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                        }
                        else
                        {
                            Console.Error.WriteLine($"Invalid theme name '{themeName}' for shape type '{shapeTypeName}'.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while applying themes: {ex.Message}");
            return;
        }

        // Save the modified diagram
        try
        {
            diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputDiagramPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}