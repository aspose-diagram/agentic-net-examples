using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expected arguments: [0] input Visio file, [1] CSV file, [2] output Visio file
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: DiagramCsvThemeMapper <input.vsdx> <data.csv> <output.vsdx>");
                return;
            }

            string diagramPath = args[0];
            string csvPath = args[1];
            string outputPath = args[2];

            // Load the Visio diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(diagramPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Read CSV data into a dictionary: shapeId -> value
            var shapeValues = new Dictionary<long, double>();
            try
            {
                foreach (var line in File.ReadAllLines(csvPath))
                {
                    // Skip empty lines
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var parts = line.Split(',');
                    if (parts.Length < 2)
                        continue; // Invalid line

                    if (long.TryParse(parts[0].Trim(), out long shapeId) &&
                        double.TryParse(parts[1].Trim(), out double value))
                    {
                        shapeValues[shapeId] = value;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read CSV: {ex.Message}");
                return;
            }

            // Process each shape based on CSV values
            // Assuming we work on the first page; adjust if needed
            if (diagram.Pages.Count == 0)
            {
                Console.WriteLine("Diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            foreach (var kvp in shapeValues)
            {
                long shapeId = kvp.Key;
                double value = kvp.Value;

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
                    Console.WriteLine($"Shape ID {shapeId} is marked as deleted; skipping.");
                    continue;
                }

                // Apply a theme based on the value
                // Example logic: value >= 50 => Variant1, else Variant2
                if (value >= 50)
                {
                    shape.PresetTheme = PresetThemeValue.Bubble;
                    shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                }
                else
                {
                    shape.PresetTheme = PresetThemeValue.Bubble;
                    shape.PresetThemeVariant = PresetThemeVariantValue.Variant2;
                }

                Console.WriteLine($"Applied theme to shape ID {shapeId} based on value {value}.");
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