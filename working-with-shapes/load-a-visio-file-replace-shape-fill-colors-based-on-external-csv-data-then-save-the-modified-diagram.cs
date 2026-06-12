using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string visioPath = "input.vsdx";
                // CSV file path (format: ShapeName,FillColorHex)
                string csvPath = "colors.csv";
                // Output Visio file path
                string outputPath = "output.vsdx";

                // Load CSV data into a dictionary: shape name -> fill color
                var colorMap = LoadColorMap(csvPath);

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(visioPath))
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

                            // Determine the key to look up (using universal name)
                            string shapeKey = shape.NameU;

                            if (colorMap.TryGetValue(shapeKey, out string fillColor))
                            {
                                // Apply solid fill pattern
                                shape.Fill.FillPattern.Value = 1; // 1 = solid
                                // Set the foreground fill color using hex string
                                shape.Fill.FillForegnd.Value = fillColor;
                            }
                        }
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Diagram processing completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Reads a CSV file where each line contains a shape name and a hex color code,
        /// separated by a comma. Returns a dictionary for quick lookup.
        /// </summary>
        static Dictionary<string, string> LoadColorMap(string csvFilePath)
        {
            var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (!File.Exists(csvFilePath))
                throw new FileNotFoundException("CSV file not found.", csvFilePath);

            foreach (var line in File.ReadLines(csvFilePath))
            {
                // Skip empty lines
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(',');

                if (parts.Length < 2)
                    continue; // Invalid line, ignore

                string shapeName = parts[0].Trim();
                string colorHex = parts[1].Trim();

                // Ensure color starts with '#'
                if (!colorHex.StartsWith("#"))
                    colorHex = "#" + colorHex;

                if (!map.ContainsKey(shapeName))
                    map.Add(shapeName, colorHex);
            }

            return map;
        }
    }