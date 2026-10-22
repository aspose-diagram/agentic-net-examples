using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <csvPath> <outputPath>");
            return;
        }

        string diagramPath = args[0];
        string csvPath = args[1];
        string outputPath = args[2];

        // Guard against missing input files.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV data file not found: {csvPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Prepare a simple category‑to‑color map.
            var categoryColors = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "A", "#FF0000" }, // Red
                { "B", "#00FF00" }, // Green
                { "C", "#0000FF" }  // Blue
            };
            // Default color if category is not in the map.
            const string defaultColor = "#FFFFFF";

            // Read all lines from the CSV file.
            // Expected format (including header): ShapeId,Category
            string[] csvLines = File.ReadAllLines(csvPath);
            for (int i = 1; i < csvLines.Length; i++) // Skip header line (index 0)
            {
                string line = csvLines[i].Trim();
                if (string.IsNullOrEmpty(line))
                    continue; // Skip empty lines

                string[] parts = line.Split(',');
                if (parts.Length < 2)
                {
                    Console.Error.WriteLine($"Invalid CSV line at {i + 1}: {line}");
                    continue;
                }

                // Parse shape ID.
                if (!long.TryParse(parts[0].Trim(), out long shapeId))
                {
                    Console.Error.WriteLine($"Invalid ShapeId at line {i + 1}: {parts[0]}");
                    continue;
                }

                string category = parts[1].Trim();

                // Determine the fill color.
                string fillColor;
                if (!categoryColors.TryGetValue(category, out fillColor))
                {
                    fillColor = defaultColor;
                }

                // Retrieve the shape from the active page.
                Shape shape = null;
                try
                {
                    shape = diagram.ActivePage.Shapes.GetShape(shapeId);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to get shape with ID {shapeId}: {ex.Message}");
                    continue;
                }

                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found.");
                    continue;
                }

                // Apply solid fill pattern.
                shape.Fill.FillPattern.Value = 1; // Solid fill

                // Set foreground fill color.
                shape.Fill.FillForegnd.Value = fillColor;

                // Ensure fill is fully opaque.
                shape.Fill.FillForegndTrans.Value = 0.0; // 0 = fully opaque
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}