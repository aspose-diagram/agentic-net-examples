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

                // Paths to the input Visio file, CSV file, and output Visio file.
                string visioInputPath = "input.vsdx";
                string csvPath = "colors.csv";
                string visioOutputPath = "output.vsdx";

                // Load the Visio diagram.
                Diagram diagram = new Diagram(visioInputPath);

                // Read CSV data into a dictionary: shape universal name -> fill color (hex string).
                var colorMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                using (var reader = new StreamReader(csvPath))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (string.IsNullOrWhiteSpace(line))
                            continue;

                        // Expecting format: ShapeName,FillColor (e.g., Process,#FF0000)
                        string[] parts = line.Split(',');
                        if (parts.Length < 2)
                            continue;

                        string shapeName = parts[0].Trim();
                        string color = parts[1].Trim();

                        // Ensure the color is a hex string starting with '#'.
                        if (!color.StartsWith("#"))
                            color = "#" + color;

                        colorMap[shapeName] = color;
                    }
                }

                // Iterate through all pages and shapes, applying fill colors where a match is found.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Use the universal shape name (NameU) to look up the desired color.
                        if (colorMap.TryGetValue(shape.NameU, out string fillColor))
                        {
                            // Set solid fill pattern.
                            shape.Fill.FillPattern.Value = 1; // 1 = solid fill.
                            // Apply the new foreground fill color.
                            shape.Fill.FillForegnd.Value = fillColor;
                        }
                    }
                }

                // Save the modified diagram.
                diagram.Save(visioOutputPath, SaveFileFormat.Csv);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }