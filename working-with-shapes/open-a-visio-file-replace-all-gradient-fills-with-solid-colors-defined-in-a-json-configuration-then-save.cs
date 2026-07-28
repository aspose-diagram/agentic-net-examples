using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputVisioPath = "input.vsdx";
                // JSON configuration file path (shape ID -> solid color hex)
                string jsonConfigPath = "colorConfig.json";
                // Output Visio file path
                string outputVisioPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputVisioPath);

                // Read and deserialize the JSON configuration
                if (!File.Exists(jsonConfigPath))
                    throw new FileNotFoundException($"JSON configuration file not found: {jsonConfigPath}");

                string jsonContent = File.ReadAllText(jsonConfigPath);
                Dictionary<string, string> colorMap = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if the shape uses a gradient fill (FillPattern value 25)
                        if (shape.Fill != null && shape.Fill.FillPattern != null && shape.Fill.FillPattern.Value == 25)
                        {
                            string shapeIdKey = shape.ID.ToString();

                            // If a solid color is defined for this shape ID, replace the gradient
                            if (colorMap != null && colorMap.TryGetValue(shapeIdKey, out string solidColor))
                            {
                                // Set fill pattern to solid (value 1)
                                shape.Fill.FillPattern.Value = 1;
                                // Apply the solid foreground color
                                shape.Fill.FillForegnd.Value = solidColor;

                                // Disable gradient and clear any existing gradient stops
                                if (shape.Fill.GradientFill != null)
                                {
                                    shape.Fill.GradientFill.GradientEnabled.Value = BOOL.False;
                                    shape.Fill.GradientFill.GradientStops.Clear();
                                }
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputVisioPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }