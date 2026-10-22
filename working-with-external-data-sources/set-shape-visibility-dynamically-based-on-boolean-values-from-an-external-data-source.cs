using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: 1) path to the Visio diagram, 2) path to the CSV data file.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <dataCsvPath> [outputPath]");
            return;
        }

        string diagramPath = args[0];
        string dataCsvPath = args[1];
        string outputPath = args.Length >= 3 ? args[2] : "output.vsdx";

        // Guard against missing files.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }
        if (!File.Exists(dataCsvPath))
        {
            Console.Error.WriteLine($"Data CSV file not found: {dataCsvPath}");
            return;
        }

        // Load visibility data from CSV (format: ShapeName,IsVisible)
        var visibilityMap = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
        try
        {
            foreach (var line in File.ReadAllLines(dataCsvPath))
            {
                // Skip empty lines.
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(',');
                if (parts.Length < 2)
                    continue; // Invalid line, ignore.

                string shapeName = parts[0].Trim();
                string boolText = parts[1].Trim();

                bool isVisible = false;
                // Accept true/false (case-insensitive) or 1/0.
                if (bool.TryParse(boolText, out bool parsedBool))
                {
                    isVisible = parsedBool;
                }
                else if (int.TryParse(boolText, out int intVal))
                {
                    isVisible = intVal != 0;
                }

                if (!string.IsNullOrEmpty(shapeName))
                {
                    visibilityMap[shapeName] = isVisible;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading CSV data: {ex.Message}");
            return;
        }

        // Load the Visio diagram and apply visibility changes.
        try
        {
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Use the shape's NameU as the identifier.
                    string shapeName = shape.NameU;
                    if (string.IsNullOrEmpty(shapeName))
                        continue;

                    if (visibilityMap.TryGetValue(shapeName, out bool shouldBeVisible))
                    {
                        if (shouldBeVisible)
                        {
                            // Ensure the shape has a reasonable size if it was previously hidden.
                            // If width or height is zero, set them to a default of 1 inch.
                            if (shape.XForm.Width.Value == 0)
                                shape.XForm.Width.Value = 1.0;
                            if (shape.XForm.Height.Value == 0)
                                shape.XForm.Height.Value = 1.0;
                        }
                        else
                        {
                            // Hide the shape by setting its width and height to zero.
                            shape.XForm.Width.Value = 0.0;
                            shape.XForm.Height.Value = 0.0;
                        }
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}