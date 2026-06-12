using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Paths – adjust as needed
            string inputVisioPath = "input.vsdx";
            string outputVisioPath = "output.vsdx";
            string jsonConfigPath = "config.json";

            // Load the JSON configuration: shape ID (as string) -> solid color hex (e.g., "#FF0000")
            Dictionary<string, string> colorConfig = null;
            if (File.Exists(jsonConfigPath))
            {
                string json = File.ReadAllText(jsonConfigPath);
                colorConfig = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            }
            else
            {
                Console.WriteLine($"Configuration file not found: {jsonConfigPath}");
                return;
            }

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputVisioPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Detect a gradient fill (pattern 25 and gradient enabled)
                    if (shape.Fill.FillPattern.Value == 25 &&
                        shape.Fill.GradientFill.GradientEnabled.Value == BOOL.True)
                    {
                        // Determine the solid color to apply
                        string solidColor;
                        if (colorConfig != null && colorConfig.TryGetValue(shape.ID.ToString(), out solidColor))
                        {
                            // Replace gradient with solid fill
                            shape.Fill.FillPattern.Value = 1; // solid fill pattern
                            shape.Fill.FillForegnd.Value = solidColor; // set foreground color

                            // Disable gradient and clear any existing stops
                            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.False;
                            shape.Fill.GradientFill.GradientStops.Clear();

                            Console.WriteLine($"Shape ID {shape.ID} gradient replaced with color {solidColor}");
                        }
                        else
                        {
                            Console.WriteLine($"No color mapping found for shape ID {shape.ID}; gradient left unchanged.");
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputVisioPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputVisioPath}");
        }
    }