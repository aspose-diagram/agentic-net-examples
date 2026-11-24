using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: diagramPath, dataPath, outputPath
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <dataPath> <outputPath>");
            return;
        }

        string diagramPath = args[0];
        string dataPath = args[1];
        string outputPath = args[2];

        // Guard against missing files
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }
        if (!File.Exists(dataPath))
        {
            Console.Error.WriteLine($"Data file not found: {dataPath}");
            return;
        }

        // Load external data (CSV: ShapeName,Value)
        var shapeValues = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
        try
        {
            foreach (var line in File.ReadAllLines(dataPath))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(',');
                if (parts.Length < 2)
                    continue; // malformed line

                string name = parts[0].Trim();
                if (double.TryParse(parts[1].Trim(), out double val))
                {
                    shapeValues[name] = val;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading data file: {ex.Message}");
            return;
        }

        // Define thresholds
        double lowThreshold = 50.0;
        double highThreshold = 80.0;

        // Load diagram and apply conditional formatting
        try
        {
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Match shape by its universal name
                        if (!string.IsNullOrEmpty(shape.NameU) && shapeValues.TryGetValue(shape.NameU, out double value))
                        {
                            // Determine fill color based on thresholds
                            if (value > highThreshold)
                            {
                                // Red fill for high values
                                shape.Fill.FillForegnd.Value = "#FF0000";
                            }
                            else if (value > lowThreshold)
                            {
                                // Yellow fill for medium values
                                shape.Fill.FillForegnd.Value = "#FFFF00";
                            }
                            else
                            {
                                // Green fill for low values
                                shape.Fill.FillForegnd.Value = "#00FF00";
                            }

                            // Optionally make text bold for high values
                            if (value > highThreshold && shape.Chars.Count > 0)
                            {
                                // Apply bold style to all characters in the shape
                                for (int i = 0; i < shape.Chars.Count; i++)
                                {
                                    shape.Chars[i].Style.Value = StyleValue.Bold;
                                }
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}