using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, CSV mapping file, output Visio file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <csvPath> <outputVisioPath>");
            return;
        }

        string visioPath = args[0];
        string csvPath = args[1];
        string outputPath = args[2];

        // Guard: ensure the Visio file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Visio file not found: {visioPath}");
            return;
        }

        // Guard: ensure the CSV file exists
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Load CSV data into a dictionary (shape ID -> fill color hex string)
        var colorMap = new Dictionary<long, string>();
        try
        {
            // Read all lines, ignore empty lines
            string[] lines = File.ReadAllLines(csvPath);
            foreach (string rawLine in lines)
            {
                if (string.IsNullOrWhiteSpace(rawLine))
                    continue;

                // Simple CSV split on comma
                string[] parts = rawLine.Split(',');
                if (parts.Length < 2)
                    continue; // skip malformed lines

                // Trim whitespace
                string idPart = parts[0].Trim();
                string colorPart = parts[1].Trim();

                // Parse shape ID
                if (long.TryParse(idPart, out long shapeId) && !string.IsNullOrEmpty(colorPart))
                {
                    // Store mapping
                    colorMap[shapeId] = colorPart;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading CSV file: {ex.Message}");
            return;
        }

        // Load the Visio diagram
        Diagram diagram;
        try
        {
            diagram = new Diagram(visioPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading Visio file: {ex.Message}");
            return;
        }

        // Iterate through all pages and shapes, applying fill colors where a mapping exists
        try
        {
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    long shapeId = shape.ID;
                    if (colorMap.TryGetValue(shapeId, out string hexColor))
                    {
                        // Ensure solid fill pattern
                        shape.Fill.FillPattern.Value = 1; // 1 = solid fill

                        // Apply the foreground fill color (hex string, e.g., "#FF0000")
                        shape.Fill.FillForegnd.Value = hexColor;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing shapes: {ex.Message}");
            return;
        }

        // Save the modified diagram
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}