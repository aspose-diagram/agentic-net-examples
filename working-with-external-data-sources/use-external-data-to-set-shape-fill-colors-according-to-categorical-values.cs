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

            // Input diagram, output diagram, and external CSV file paths
            string diagramPath = "input.vsdx";
            string outputPath = "output.vsdx";
            string dataPath = "colors.csv";

            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Read external CSV data (format: ShapeName,HexColor)
            var colorMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (!File.Exists(dataPath))
            {
                Console.WriteLine($"Data file not found: {dataPath}");
                return;
            }

            foreach (var line in File.ReadAllLines(dataPath))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(',');
                if (parts.Length < 2)
                    continue;

                string shapeName = parts[0].Trim();
                string hexColor = parts[1].Trim();

                if (!string.IsNullOrEmpty(shapeName) && !string.IsNullOrEmpty(hexColor))
                    colorMap[shapeName] = hexColor;
            }

            // Apply fill colors based on the external data
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Use the universal name if available, otherwise the local name
                    string key = shape.NameU ?? shape.Name;
                    if (key == null)
                        continue;

                    if (colorMap.TryGetValue(key, out string hex))
                    {
                        // Set solid fill pattern and foreground color
                        shape.Fill.FillPattern.Value = 1; // Solid fill
                        shape.Fill.FillForegnd.Value = hex;
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
