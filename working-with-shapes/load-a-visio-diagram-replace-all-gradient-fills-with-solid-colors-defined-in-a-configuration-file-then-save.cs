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
            // Expect three arguments: input Visio file, configuration JSON, output Visio file
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: GradientToSolid <input.vsdx> <config.json> <output.vsdx>");
                return;
            }

            string inputPath = args[0];
            string configPath = args[1];
            string outputPath = args[2];

            // Load configuration: mapping of shape ID (as string) to solid fill color (hex string)
            Dictionary<string, string> colorMap = new Dictionary<string, string>();
            try
            {
                string json = File.ReadAllText(configPath);
                colorMap = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read configuration file: {ex.Message}");
                return;
            }

            // Load the Visio diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape uses a gradient fill (FillPattern value 25)
                    if (shape.Fill != null && shape.Fill.FillPattern != null && shape.Fill.FillPattern.Value == 25)
                    {
                        // Determine the solid color to apply
                        string shapeIdKey = shape.ID.ToString();
                        string solidColor;
                        if (!colorMap.TryGetValue(shapeIdKey, out solidColor))
                        {
                            // Default to white if no mapping is found
                            solidColor = "#FFFFFF";
                        }

                        // Replace gradient with solid fill
                        shape.Fill.FillPattern.Value = 1; // Solid fill pattern
                        shape.Fill.FillForegnd.Value = solidColor; // Set solid color
                        shape.Fill.GradientFill.GradientEnabled.Value = BOOL.False; // Disable gradient
                        shape.Fill.GradientFill.GradientStops.Clear(); // Remove any gradient stops
                    }
                }
            }

            // Save the modified diagram
            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save diagram: {ex.Message}");
            }
        }
    }