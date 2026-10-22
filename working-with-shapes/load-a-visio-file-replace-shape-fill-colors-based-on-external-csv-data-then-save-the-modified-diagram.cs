using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: Visio file path and CSV file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> <CsvFilePath>");
            return;
        }

        string visioPath = args[0];
        string csvPath = args[1];

        // Guard: ensure Visio file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Visio file not found: {visioPath}");
            return;
        }

        // Guard: ensure CSV file exists
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Load color mappings from CSV (expected format: ShapeId,FillColor)
        var colorMap = new Dictionary<long, string>();
        try
        {
            using (var reader = new StreamReader(csvPath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var parts = line.Split(',');
                    if (parts.Length < 2)
                        continue;

                    if (long.TryParse(parts[0].Trim(), out long shapeId))
                    {
                        string color = parts[1].Trim();
                        // Basic validation: ensure color starts with '#'
                        if (!color.StartsWith("#"))
                            color = "#" + color;
                        colorMap[shapeId] = color;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading CSV file: {ex.Message}");
            return;
        }

        // Load the Visio diagram and apply fill colors
        try
        {
            Diagram diagram = new Diagram(visioPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (colorMap.TryGetValue(shape.ID, out string newColor))
                    {
                        // Ensure the shape has a solid fill pattern
                        shape.Fill.FillPattern.Value = 1; // Solid fill

                        // Apply the new foreground fill color (hex string, e.g., "#FF0000")
                        shape.Fill.FillForegnd.Value = newColor;
                    }
                }
            }

            // Define output path (same directory, with suffix "_modified")
            string outputDir = Path.GetDirectoryName(visioPath) ?? "";
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(visioPath);
            string outputPath = Path.Combine(outputDir, $"{fileNameWithoutExt}_modified.vsdx");

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing Visio diagram: {ex.Message}");
        }
    }
}