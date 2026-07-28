using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Output Visio file path
                string outputPath = "output.vsdx";

                // External CSV file that maps categories to hex color strings (e.g., "Category,Color")
                // Example content:
                // High,#FF0000
                // Medium,#FFFF00
                // Low,#00FF00
                string csvPath = "categoryColors.csv";

                // Load external category‑to‑color mapping
                var categoryColors = LoadCategoryColors(csvPath);

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Assume the shape's Data1 cell holds the categorical value
                        string category = shape.Data1?.Trim();

                        if (string.IsNullOrEmpty(category))
                            continue;

                        if (categoryColors.TryGetValue(category, out string hexColor))
                        {
                            // Set solid fill pattern
                            shape.Fill.FillPattern.Value = 1; // 1 = solid

                            // Apply the fill foreground color using a hex string
                            shape.Fill.FillForegnd.Value = hexColor;
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

        // Reads a CSV file where each line is "Category,HexColor"
        private static Dictionary<string, string> LoadCategoryColors(string csvPath)
        {
            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (!File.Exists(csvPath))
                throw new FileNotFoundException($"Category‑color CSV file not found: {csvPath}");

            foreach (string line in File.ReadAllLines(csvPath))
            {
                // Skip empty lines
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split(',');

                if (parts.Length != 2)
                    continue; // Invalid line format; ignore

                string category = parts[0].Trim();
                string color = parts[1].Trim();

                // Basic validation of hex color (must start with '#')
                if (!color.StartsWith("#"))
                    continue; // Invalid color format; ignore

                dict[category] = color;
            }

            return dict;
        }
    }