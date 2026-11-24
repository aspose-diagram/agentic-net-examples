using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, JSON config file, output Visio file
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisio> <configJson> <outputVisio>");
            return;
        }

        string inputPath = args[0];
        string jsonPath = args[1];
        string outputPath = args[2];

        // Guard against missing files
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input Visio file not found: {inputPath}");
            return;
        }
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON configuration file not found: {jsonPath}");
            return;
        }

        // Load JSON configuration into a dictionary of ShapeId -> ColorHex
        Dictionary<long, string> colorMap = new Dictionary<long, string>();
        try
        {
            string jsonContent = File.ReadAllText(jsonPath);
            using (JsonDocument doc = JsonDocument.Parse(jsonContent))
            {
                if (doc.RootElement.ValueKind != JsonValueKind.Array)
                {
                    Console.Error.WriteLine("JSON configuration must be an array of objects.");
                    return;
                }

                foreach (JsonElement element in doc.RootElement.EnumerateArray())
                {
                    if (element.TryGetProperty("ShapeId", out JsonElement idElem) &&
                        element.TryGetProperty("Color", out JsonElement colorElem))
                    {
                        long shapeId = idElem.GetInt64();
                        string colorHex = colorElem.GetString();
                        if (!string.IsNullOrWhiteSpace(colorHex))
                        {
                            // Ensure the color string starts with '#'
                            colorHex = colorHex.Trim();
                            if (!colorHex.StartsWith("#"))
                                colorHex = "#" + colorHex;
                            colorMap[shapeId] = colorHex;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error parsing JSON configuration: {ex.Message}");
            return;
        }

        // Load the Visio diagram and process shapes
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

                        // Check if the shape currently has a gradient fill (pattern value 25)
                        if (shape.Fill.FillPattern.Value == 25)
                        {
                            // Determine the replacement color: use config if present
                            if (!colorMap.TryGetValue(shape.ID, out string replacementColor))
                            {
                                // No specific color defined for this shape; skip replacement
                                continue;
                            }

                            // Set solid fill pattern
                            shape.Fill.FillPattern.Value = 1; // Solid fill

                            // Apply the solid color (hex string)
                            shape.Fill.FillForegnd.Value = replacementColor;
                        }
                    }
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing Visio diagram: {ex.Message}");
            return;
        }

        Console.WriteLine("Gradient fills have been replaced successfully.");
    }
}