using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for SaveFileFormat enum

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, CSV file, output Visio file
        if (args.Length < 3)
        {
            // Write usage message to error output and exit gracefully
            Console.Error.WriteLine("Usage: VisioColorUpdater <inputVisioPath> <csvPath> <outputVisioPath>");
            return;
        }

        string inputVisioPath = args[0];
        string csvPath = args[1];
        string outputVisioPath = args[2];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputVisioPath))
        {
            Console.Error.WriteLine($"File not found: {inputVisioPath}");
            return;
        }

        // Guard: ensure the CSV file exists
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"File not found: {csvPath}");
            return;
        }

        // Load CSV data into a dictionary (key: shape NameU, value: fill color hex string)
        var colorMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (string line in File.ReadAllLines(csvPath))
        {
            if (string.IsNullOrWhiteSpace(line))
                continue; // Skip empty lines

            // Simple CSV split by comma; assumes no commas inside fields
            string[] parts = line.Split(',');
            if (parts.Length < 2)
                continue; // Invalid line, ignore

            string shapeName = parts[0].Trim();
            string colorHex = parts[1].Trim();

            if (!string.IsNullOrEmpty(shapeName) && !string.IsNullOrEmpty(colorHex))
            {
                colorMap[shapeName] = colorHex;
            }
        }

        try
        {
            // Load the Visio diagram
            var diagram = new Diagram(inputVisioPath);

            // Iterate through all pages and shapes, applying fill colors where a match is found
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (colorMap.TryGetValue(shape.NameU, out string newColor))
                    {
                        // Set solid fill pattern (1 = solid)
                        shape.Fill.FillPattern.Value = 1;
                        // Apply the new fill foreground color (hex string, e.g., "#FF0000")
                        shape.Fill.FillForegnd.Value = newColor;
                    }
                }
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputVisioPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}