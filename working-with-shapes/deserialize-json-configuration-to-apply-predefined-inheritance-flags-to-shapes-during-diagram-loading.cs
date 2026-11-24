using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Model representing a single shape configuration from JSON
    private class ShapeConfig
    {
        public long ShapeId { get; set; }
        public int? BeginArrow { get; set; }
        public int? EndArrow { get; set; }
        // Additional inheritance-related flags can be added here
    }

    // Model representing the root of the JSON configuration
    private class ConfigRoot
    {
        public List<ShapeConfig> ShapeSettings { get; set; }
    }

    static void Main(string[] args)
    {
        // Expect two arguments: path to JSON config and path to input diagram
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <config.json> <inputDiagram.vsdx>");
            return;
        }

        string configPath = args[0];
        string diagramPath = args[1];
        string outputPath = Path.Combine(Path.GetDirectoryName(diagramPath) ?? "", "output_modified.vsdx");

        // Guard against missing files
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Deserialize JSON configuration
        ConfigRoot config;
        try
        {
            string json = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<ConfigRoot>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            if (config?.ShapeSettings == null)
            {
                Console.Error.WriteLine("Invalid configuration: ShapeSettings missing.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read or parse JSON configuration: {ex.Message}");
            return;
        }

        // Load diagram and apply settings
        try
        {
            using (Diagram diagram = new Diagram(diagramPath))
            {
                foreach (var shapeCfg in config.ShapeSettings)
                {
                    Shape shape = FindShapeById(diagram, shapeCfg.ShapeId);
                    if (shape == null)
                    {
                        Console.Error.WriteLine($"Shape with ID {shapeCfg.ShapeId} not found in diagram.");
                        continue;
                    }

                    // Apply BeginArrow if specified
                    if (shapeCfg.BeginArrow.HasValue)
                    {
                        // Arrow style values are integers; assign directly
                        shape.Line.BeginArrow.Value = shapeCfg.BeginArrow.Value;
                    }

                    // Apply EndArrow if specified
                    if (shapeCfg.EndArrow.HasValue)
                    {
                        shape.Line.EndArrow.Value = shapeCfg.EndArrow.Value;
                    }

                    // Additional inheritance flag handling can be added here
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Helper method to locate a shape by its ID across all pages
    private static Shape FindShapeById(Diagram diagram, long shapeId)
    {
        foreach (Page page in diagram.Pages)
        {
            try
            {
                // GetShape throws if not found; we catch and continue
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape != null)
                    return shape;
            }
            catch
            {
                // Ignore and continue searching other pages
            }
        }
        return null;
    }
}