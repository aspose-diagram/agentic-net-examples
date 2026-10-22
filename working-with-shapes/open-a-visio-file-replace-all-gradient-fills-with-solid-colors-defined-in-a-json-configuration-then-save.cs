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
        // Expect three arguments: input Visio file, JSON config file, output Visio file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <configJsonPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string configPath = args[1];
        string outputPath = args[2];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input Visio file not found: {inputPath}");
            return;
        }

        // Guard against missing JSON configuration file
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration JSON file not found: {configPath}");
            return;
        }

        // Load color mapping from JSON (shape ID as string -> hex color)
        Dictionary<string, string> colorMap;
        try
        {
            string jsonContent = File.ReadAllText(configPath);
            colorMap = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent);
            if (colorMap == null)
            {
                Console.Error.WriteLine("Failed to deserialize JSON configuration.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading JSON configuration: {ex.Message}");
            return;
        }

        // Load the Visio diagram and process gradient fills
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has a Fill and GradientFill defined
                        if (shape.Fill == null || shape.Fill.GradientFill == null)
                            continue;

                        // Check if gradient fill is enabled
                        if (shape.Fill.GradientFill.GradientEnabled != null &&
                            shape.Fill.GradientFill.GradientEnabled.Value == BOOL.True)
                        {
                            // Determine the solid color to apply based on shape ID
                            string shapeIdKey = shape.ID.ToString();
                            if (colorMap.TryGetValue(shapeIdKey, out string hexColor))
                            {
                                // Set solid fill pattern (1 = solid)
                                shape.Fill.FillPattern.Value = 1;

                                // Apply the solid foreground color
                                shape.Fill.FillForegnd.Value = hexColor;

                                // Disable gradient fill
                                shape.Fill.GradientFill.GradientEnabled.Value = BOOL.False;

                                // Clear any existing gradient stops
                                shape.Fill.GradientFill.GradientStops.Clear();
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing Visio diagram: {ex.Message}");
        }
    }
}