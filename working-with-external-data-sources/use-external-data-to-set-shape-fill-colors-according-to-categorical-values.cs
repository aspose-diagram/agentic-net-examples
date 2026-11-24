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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <dataCsvPath> <outputDiagramPath>");
            return;
        }

        string inputDiagramPath = args[0];
        string dataCsvPath = args[1];
        string outputDiagramPath = args[2];

        // Guard against missing files.
        if (!File.Exists(inputDiagramPath))
        {
            Console.Error.WriteLine($"Input diagram file not found: {inputDiagramPath}");
            return;
        }

        if (!File.Exists(dataCsvPath))
        {
            Console.Error.WriteLine($"Data CSV file not found: {dataCsvPath}");
            return;
        }

        // Load the Visio diagram inside a try/catch block.
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputDiagramPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Parse the CSV file (expected format: ShapeId,Category).
        // Build a map of shape IDs to category strings.
        var shapeCategoryMap = new Dictionary<long, string>();
        try
        {
            string[] lines = File.ReadAllLines(dataCsvPath);
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                // Skip empty lines.
                if (string.IsNullOrEmpty(line))
                    continue;

                // Optional: skip header line if it contains non‑numeric first token.
                string[] parts = line.Split(',');
                if (parts.Length < 2)
                    continue; // malformed line, ignore.

                // Attempt to parse shape ID.
                if (!long.TryParse(parts[0].Trim(), out long shapeId))
                    continue; // invalid ID, ignore.

                string category = parts[1].Trim();
                shapeCategoryMap[shapeId] = category;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read or parse CSV data: {ex.Message}");
            return;
        }

        // Define a simple category‑to‑color mapping.
        var categoryColorMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "A", "#FF0000" }, // Red
            { "B", "#00FF00" }, // Green
            { "C", "#0000FF" }, // Blue
            // Add more mappings as needed.
        };

        // Apply fill colors to shapes based on the category.
        try
        {
            // Iterate over each entry in the CSV‑derived map.
            foreach (KeyValuePair<long, string> entry in shapeCategoryMap)
            {
                long shapeId = entry.Key;
                string category = entry.Value;

                // Retrieve the shape from the active page.
                // If the shape might be on another page, additional logic would be required.
                Shape shape = diagram.ActivePage.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on the active page.");
                    continue;
                }

                // Determine the fill color for the current category.
                string fillColor;
                if (!categoryColorMap.TryGetValue(category, out fillColor))
                {
                    // Default color if the category is unmapped.
                    fillColor = "#FFFFFF"; // White
                }

                // Set a solid fill pattern.
                shape.Fill.FillPattern.Value = 1; // Solid fill.

                // Apply the foreground fill color.
                shape.Fill.FillForegnd.Value = fillColor;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while applying fill colors: {ex.Message}");
            return;
        }

        // Save the modified diagram.
        try
        {
            diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputDiagramPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}