using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the Visio diagram and the CSV file
                string diagramPath = "input.vsdx";
                string csvPath = "data.csv";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Read CSV data (expected format: ShapeId,Value)
                // Example:
                // 12345,High
                // 12346,Low
                var csvLines = File.ReadAllLines(csvPath);
                var shapeData = new Dictionary<long, string>();

                foreach (var line in csvLines)
                {
                    // Skip empty lines
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var parts = line.Split(',');
                    if (parts.Length < 2)
                        continue; // malformed line

                    if (long.TryParse(parts[0].Trim(), out long shapeId))
                    {
                        string value = parts[1].Trim();
                        shapeData[shapeId] = value;
                    }
                }

                // Process each entry and apply a theme based on the value
                // For this example we use two simple rules:
                //   - If value equals "High" -> apply Bubble theme Variant1 QuickStyle1
                //   - If value equals "Low"  -> apply Bubble theme Variant2 QuickStyle2
                // Adjust the logic as needed for your actual data.
                Page page = diagram.Pages[0]; // assuming all shapes are on the first page

                foreach (var kvp in shapeData)
                {
                    long shapeId = kvp.Key;
                    string value = kvp.Value;

                    // Retrieve the shape; GetShape returns null if not found
                    Shape shape = page.Shapes.GetShape(shapeId);
                    if (shape == null)
                    {
                        Console.WriteLine($"Shape with ID {shapeId} not found.");
                        continue;
                    }

                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                    {
                        Console.WriteLine($"Shape with ID {shapeId} is marked as deleted. Skipping.");
                        continue;
                    }

                    // Apply theme based on the CSV value
                    if (value.Equals("High", StringComparison.OrdinalIgnoreCase))
                    {
                        shape.PresetTheme = PresetThemeValue.Bubble;
                        shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                        shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;
                    }
                    else if (value.Equals("Low", StringComparison.OrdinalIgnoreCase))
                    {
                        shape.PresetTheme = PresetThemeValue.Bubble;
                        shape.PresetThemeVariant = PresetThemeVariantValue.Variant2;
                        shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;
                    }
                    else
                    {
                        // Default handling for other values
                        shape.PresetTheme = PresetThemeValue.Bubble;
                        shape.PresetThemeVariant = PresetThemeVariantValue.Variant3;
                        shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle3;
                    }

                    Console.WriteLine($"Applied theme to shape ID {shapeId} based on value '{value}'.");
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
    }