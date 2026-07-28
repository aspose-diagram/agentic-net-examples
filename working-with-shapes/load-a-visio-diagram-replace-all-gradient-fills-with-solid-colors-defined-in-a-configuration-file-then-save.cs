using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "output.vsdx";
                // Configuration file path (JSON: { "shapeId": "#FF0000", ... })
                string configPath = "config.json";

                // Load color configuration
                Dictionary<string, string> colorMap = LoadColorConfiguration(configPath);

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if the shape uses a gradient fill (pattern value 25)
                        if (shape.Fill.FillPattern.Value == 25)
                        {
                            string shapeKey = shape.ID.ToString();

                            // Determine the replacement solid color
                            if (colorMap.TryGetValue(shapeKey, out string solidColor))
                            {
                                // Replace gradient with solid fill
                                shape.Fill.FillPattern.Value = 1; // Solid fill pattern
                                shape.Fill.FillForegnd.Value = solidColor; // Hex color string

                                // Disable gradient and clear any existing gradient stops
                                shape.Fill.GradientFill.GradientEnabled.Value = BOOL.False;
                                shape.Fill.GradientFill.GradientStops.Clear();
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Helper method to read the JSON configuration file into a dictionary
        private static Dictionary<string, string> LoadColorConfiguration(string configPath)
        {
            if (!File.Exists(configPath))
                throw new FileNotFoundException($"Configuration file not found: {configPath}");

            string jsonContent = File.ReadAllText(configPath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent, options)
                   ?? new Dictionary<string, string>();
        }
    }