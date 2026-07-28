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

                // Input Visio file path
                string visioPath = "input.vsdx";

                // CSV file path (format: ShapeName,HexColor)
                string csvPath = "colors.csv";

                // Output Visio file path
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Read CSV data into a dictionary (key: shape name, value: hex color)
                var colorMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (var line in File.ReadAllLines(csvPath))
                {
                    // Skip empty lines
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Split by comma
                    var parts = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 2)
                    {
                        string shapeName = parts[0].Trim();
                        string hexColor = parts[1].Trim();

                        // Ensure color starts with '#'
                        if (!hexColor.StartsWith("#"))
                            hexColor = "#" + hexColor;

                        colorMap[shapeName] = hexColor;
                    }
                }

                // Iterate through all pages and shapes, applying colors where a match is found
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Use the universal name (NameU) for matching; fallback to Name if needed
                        string shapeKey = shape.NameU ?? shape.Name;

                        if (shapeKey != null && colorMap.TryGetValue(shapeKey, out string newColor))
                        {
                            // Set solid fill pattern
                            shape.Fill.FillPattern.Value = 1; // 1 = solid

                            // Apply the new fill foreground color
                            shape.Fill.FillForegnd.Value = newColor;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }