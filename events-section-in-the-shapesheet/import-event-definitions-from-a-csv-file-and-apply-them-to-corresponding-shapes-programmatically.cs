using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: diagram file, CSV file with events, output diagram file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <csvPath> <outputPath>");
            return;
        }

        string diagramPath = args[0];
        string csvPath = args[1];
        string outputPath = args[2];

        // Guard: ensure diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Guard: ensure CSV file exists
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        Diagram diagram;
        try
        {
            // Load the Visio diagram
            diagram = new Diagram(diagramPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Read all lines from the CSV file
        string[] csvLines;
        try
        {
            csvLines = File.ReadAllLines(csvPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read CSV file: {ex.Message}");
            return;
        }

        // Process each CSV line (skip empty lines and optional header)
        foreach (string rawLine in csvLines)
        {
            if (string.IsNullOrWhiteSpace(rawLine))
                continue;

            // Simple CSV split – assumes no commas inside quoted fields
            string[] parts = rawLine.Split(',');

            // Expect at least three columns: ShapeId, EventName, Formula
            if (parts.Length < 3)
            {
                Console.Error.WriteLine($"Invalid CSV line (expected at least 3 columns): {rawLine}");
                continue;
            }

            // Trim whitespace from each part
            string shapeIdStr = parts[0].Trim();
            string eventName = parts[1].Trim();
            string formula = parts[2].Trim();

            // Parse shape ID (supports int or long)
            if (!long.TryParse(shapeIdStr, out long shapeId))
            {
                Console.Error.WriteLine($"Invalid ShapeId '{shapeIdStr}' in line: {rawLine}");
                continue;
            }

            // Retrieve the shape from the first page (adjust if needed)
            Shape shape = null;
            try
            {
                // Ensure the page index exists
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Diagram contains no pages.");
                    break;
                }

                // Use the first page for lookup
                shape = diagram.Pages[0].Shapes.GetShape(shapeId);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error retrieving shape ID {shapeId}: {ex.Message}");
                continue;
            }

            if (shape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found.");
                continue;
            }

            // Apply the formula to the appropriate event cell
            try
            {
                switch (eventName.ToLowerInvariant())
                {
                    case "eventdrop":
                        shape.Event.EventDrop.Ufe.F = formula;
                        break;
                    case "eventdblclick":
                        shape.Event.EventDblClick.Ufe.F = formula;
                        break;
                    case "eventxfmod":
                        shape.Event.EventXFMod.Ufe.F = formula;
                        break;
                    case "eventmultidrop":
                        shape.Event.EventMultiDrop.Ufe.F = formula;
                        break;
                    case "thetext":
                        shape.Event.TheText.Ufe.F = formula;
                        break;
                    case "thedata":
                        shape.Event.TheData.Ufe.F = formula;
                        break;
                    default:
                        Console.Error.WriteLine($"Unsupported event name '{eventName}' for shape ID {shapeId}.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to set event '{eventName}' on shape ID {shapeId}: {ex.Message}");
            }
        }

        // Save the modified diagram
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}