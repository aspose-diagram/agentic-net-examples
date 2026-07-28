using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Path to the source Visio diagram
            string diagramPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Load visibility settings from an external CSV file (shapeId,visible)
            // Example line: 12345,true
            var visibilityMap = new Dictionary<long, bool>();
            string csvPath = "visibility.csv";

            if (File.Exists(csvPath))
            {
                foreach (string line in File.ReadAllLines(csvPath))
                {
                    // Skip empty lines
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] parts = line.Split(',');
                    if (parts.Length >= 2 &&
                        long.TryParse(parts[0].Trim(), out long shapeId) &&
                        bool.TryParse(parts[1].Trim(), out bool isVisible))
                    {
                        visibilityMap[shapeId] = isVisible;
                    }
                }
            }
            else
            {
                Console.WriteLine($"Visibility file not found: {csvPath}");
            }

            // Apply visibility settings to shapes
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    if (visibilityMap.TryGetValue(shape.ID, out bool isVisible))
                    {
                        // Hide shape when not visible by marking it as deleted locally
                        shape.Del = isVisible ? BOOL.False : BOOL.True;
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
