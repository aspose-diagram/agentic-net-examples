using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, visibility data CSV path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath> <visibilityCsvPath>");
            return;
        }

        string inputDiagramPath = args[0];
        string outputDiagramPath = args[1];
        string csvPath = args[2];

        // Guard: verify input diagram file exists
        if (!File.Exists(inputDiagramPath))
        {
            Console.Error.WriteLine($"Input diagram file not found: {inputDiagramPath}");
            return;
        }

        // Guard: verify CSV data file exists
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"Visibility CSV file not found: {csvPath}");
            return;
        }

        // Load visibility data from CSV (format: ShapeName,IsVisible)
        var visibilityMap = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
        try
        {
            foreach (var line in File.ReadAllLines(csvPath))
            {
                // Skip empty lines
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(',');
                if (parts.Length != 2)
                {
                    Console.Error.WriteLine($"Invalid CSV line (expected 2 columns): {line}");
                    continue;
                }

                string shapeName = parts[0].Trim();
                string boolText = parts[1].Trim();

                if (bool.TryParse(boolText, out bool isVisible))
                {
                    visibilityMap[shapeName] = isVisible;
                }
                else
                {
                    Console.Error.WriteLine($"Invalid boolean value in CSV line: {line}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading CSV file: {ex.Message}");
            return;
        }

        // Load the diagram and apply visibility changes
        try
        {
            // Load diagram from file
            Diagram diagram = new Diagram(inputDiagramPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Determine if we have a visibility rule for this shape (using NameU)
                    if (visibilityMap.TryGetValue(shape.NameU, out bool isVisible))
                    {
                        if (isVisible)
                        {
                            // Show shape: set width and height to a default non-zero size (e.g., 1 inch)
                            shape.XForm.Width.Value = 1.0;
                            shape.XForm.Height.Value = 1.0;
                        }
                        else
                        {
                            // Hide shape: set width and height to zero
                            shape.XForm.Width.Value = 0.0;
                            shape.XForm.Height.Value = 0.0;
                        }
                    }
                }
            }

            // Save the modified diagram to the output path in VSDX format
            diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        Console.WriteLine("Diagram processing completed successfully.");
    }
}