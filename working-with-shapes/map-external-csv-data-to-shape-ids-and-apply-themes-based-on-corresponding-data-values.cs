using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        // Entry point
        static void Main(string[] args)
        {
            // Expect three arguments: diagram file path, CSV file path, output diagram path
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: CsvShapeThemeMapper <diagramPath> <csvPath> <outputPath>");
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
            var shapeValueMap = new Dictionary<long, string>();
            try
            {
                foreach (var line in File.ReadLines(csvPath))
                {
                    // Skip empty lines
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Assume CSV format: ShapeId,Value
                    var parts = line.Split(',');
                    if (parts.Length < 2)
                        continue; // malformed line

                    if (long.TryParse(parts[0].Trim(), out long shapeId))
                    {
                        string value = parts[1].Trim();
                        shapeValueMap[shapeId] = value;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read CSV: {ex.Message}");
                return;
            }

            // Iterate through all pages and shapes, applying themes based on CSV values
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    long id = shape.ID;
                    if (!shapeValueMap.TryGetValue(id, out string csvValue))
                        continue; // No mapping for this shape

                    // Apply a preset theme (Bubble) and choose variant based on csvValue
                    shape.PresetTheme = PresetThemeValue.Bubble;

                    // Determine variant
                    switch (csvValue)
                    {
                        case "1":
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;
                            break;
                        case "2":
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant2;
                            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;
                            break;
                        case "3":
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant3;
                            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle3;
                            break;
                        default:
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant4;
                            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle4;
                            break;
                    }

                    // Optionally set a style matrix (example uses Style2 and Color3)
                    shape.SetPresetThemeStyleMatrics(PresetStyleMatricsValue.Style2, PresetColorMatricsValue.Color3);
                }
            }

            // Save the modified diagram
            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save diagram: {ex.Message}");
            }
        }
    }