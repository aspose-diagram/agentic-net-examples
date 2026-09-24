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
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: GradientToSolid <inputVisio> <configJson> <outputVisio>");
                return;
            }

            string inputPath = args[0];
            string jsonPath = args[1];
            string outputPath = args[2];

            // Load JSON configuration: shape ID (as string) -> solid color hex (e.g., "#FF0000")
            Dictionary<string, string> colorMap;
            try
            {
                string jsonContent = File.ReadAllText(jsonPath);
                colorMap = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent);
                if (colorMap == null)
                {
                    Console.WriteLine("JSON configuration is empty or invalid.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read or parse JSON config: {ex.Message}");
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
                Console.WriteLine($"Failed to load Visio file: {ex.Message}");
                return;
            }

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Identify gradient fill (FillPattern value 25 indicates gradient)
                    if (shape.Fill.FillPattern.Value == 25)
                    {
                        string shapeIdKey = shape.ID.ToString();
                        if (colorMap.TryGetValue(shapeIdKey, out string solidColor))
                        {
                            // Replace gradient with solid fill
                            shape.Fill.FillPattern.Value = 1; // Solid fill
                            shape.Fill.FillForegnd.Value = solidColor;

                            // Disable gradient fill
                            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.False;
                            shape.Fill.GradientFill.GradientStops.Clear();

                            Console.WriteLine($"Shape ID {shape.ID} gradient replaced with solid color {solidColor}.");
                        }
                        else
                        {
                            Console.WriteLine($"Shape ID {shape.ID} has gradient but no mapping in JSON; left unchanged.");
                        }
                    }
                }
            }

            // Save the modified diagram
            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save diagram: {ex.Message}");
            }
        }
    }