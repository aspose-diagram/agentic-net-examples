using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        // Configuration model matching the JSON file structure
        private class Config
        {
            public List<long> RetainFillShapeIds { get; set; } = new();
        }

        static void Main()
        {
            try
            {

                // Paths – adjust as needed
                string diagramPath = "input.vsdx";
                string configPath = "config.json";
                string outputPath = "output.vsdx";

                // Load configuration
                Config config = LoadConfig(configPath);

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Process each page and shape
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // If the shape ID is listed in the config, keep its fill
                        // and force line properties to inherit from the parent style
                        if (config.RetainFillShapeIds.Contains(shape.ID))
                        {
                            // Inherit line color
                            shape.Line.LineColor.Value = shape.InheritLine.LineColor.Value;

                            // Inherit line weight
                            shape.Line.LineWeight.Value = shape.InheritLine.LineWeight.Value;

                            // Inherit line pattern
                            shape.Line.LinePattern.Value = shape.InheritLine.LinePattern.Value;

                            // Inherit begin arrow
                            shape.Line.BeginArrow.Value = shape.InheritLine.BeginArrow.Value;

                            // Inherit end arrow
                            shape.Line.EndArrow.Value = shape.InheritLine.EndArrow.Value;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Helper method to read the JSON configuration file
        private static Config LoadConfig(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"Configuration file '{path}' not found. Using empty configuration.");
                return new Config();
            }

            try
            {
                string json = File.ReadAllText(path);
                Config? cfg = JsonSerializer.Deserialize<Config>(json);
                return cfg ?? new Config();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to parse configuration: {ex.Message}");
                return new Config();
            }
        }
    }