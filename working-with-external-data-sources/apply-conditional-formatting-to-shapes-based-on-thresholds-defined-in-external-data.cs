using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths – adjust as needed
                string diagramPath = "input.vsdx";
                string thresholdsPath = "thresholds.csv"; // Format: ShapeName,Value
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Load thresholds from external CSV file into a dictionary
                var thresholds = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
                if (File.Exists(thresholdsPath))
                {
                    foreach (var line in File.ReadAllLines(thresholdsPath))
                    {
                        // Skip empty lines
                        if (string.IsNullOrWhiteSpace(line))
                            continue;

                        var parts = line.Split(',');
                        if (parts.Length != 2)
                            continue; // Invalid line, ignore

                        string shapeName = parts[0].Trim();
                        if (double.TryParse(parts[1].Trim(), out double value))
                        {
                            thresholds[shapeName] = value;
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Threshold file not found: {thresholdsPath}");
                    return;
                }

                // Define a simple threshold value for demonstration
                double highThreshold = 100.0;

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if the shape name exists in the thresholds dictionary
                        if (shape.NameU != null && thresholds.TryGetValue(shape.NameU, out double shapeValue))
                        {
                            // Apply conditional fill color based on the value
                            if (shapeValue > highThreshold)
                            {
                                // High value – fill red
                                shape.Fill.FillForegnd.Value = "#FF0000";
                            }
                            else
                            {
                                // Low or equal – fill green
                                shape.Fill.FillForegnd.Value = "#00FF00";
                            }

                            // Optionally, change line color to black for visibility
                            shape.Line.LineColor.Value = "#000000";
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved to {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }