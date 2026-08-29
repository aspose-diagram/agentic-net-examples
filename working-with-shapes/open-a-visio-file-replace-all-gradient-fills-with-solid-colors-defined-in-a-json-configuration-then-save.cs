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

                // Paths – adjust as needed
                string inputVisioPath = "input.vsdx";
                string outputVisioPath = "output.vsdx";
                string jsonConfigPath = "config.json";

                // Load JSON configuration: shape identifier (ID or NameU) -> solid color hex string
                Dictionary<string, string> colorMap = null;
                if (File.Exists(jsonConfigPath))
                {
                    string json = File.ReadAllText(jsonConfigPath);
                    colorMap = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                }

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputVisioPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape currently uses a gradient fill (FillPattern = 25)
                        if (shape.Fill.FillPattern.Value == 25)
                        {
                            // Determine the solid color to apply
                            string solidColor = "#FFFFFF"; // default fallback color
                            if (colorMap != null)
                            {
                                // Try to find a color by shape ID
                                if (colorMap.TryGetValue(shape.ID.ToString(), out string colorById))
                                {
                                    solidColor = colorById;
                                }
                                // If not found, try by universal name
                                else if (!string.IsNullOrEmpty(shape.NameU) && colorMap.TryGetValue(shape.NameU, out string colorByName))
                                {
                                    solidColor = colorByName;
                                }
                            }

                            // Replace gradient with solid fill
                            shape.Fill.FillPattern.Value = 1;                     // Solid fill pattern
                            shape.Fill.FillForegnd.Value = solidColor;           // Set foreground (fill) color
                            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.False; // Disable gradient
                            shape.Fill.GradientFill.GradientStops.Clear();      // Remove any existing gradient stops
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