using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, CSV path, output diagram path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagram> <eventsCsv> <outputDiagram>");
            return;
        }

        string diagramPath = args[0];
        string csvPath = args[1];
        string outputPath = args[2];

        // Guard against missing files
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        try
        {
            // Load the Visio diagram (assuming VSDX format; adjust LoadFileFormat if needed)
            Diagram diagram = new Diagram(diagramPath, LoadFileFormat.Vsdx);

            // Read all lines from the CSV file
            string[] lines = File.ReadAllLines(csvPath);

            // Process each line (skip empty lines and optional header)
            foreach (string rawLine in lines)
            {
                if (string.IsNullOrWhiteSpace(rawLine))
                    continue;

                // Simple CSV split – assumes no commas inside fields
                string[] parts = rawLine.Split(',');

                // Expect at least 3 columns: ShapeId, EventName, Formula
                if (parts.Length < 3)
                {
                    Console.Error.WriteLine($"Invalid CSV line (expected 3 columns): {rawLine}");
                    continue;
                }

                // Parse shape ID
                if (!long.TryParse(parts[0].Trim(), out long shapeId))
                {
                    Console.Error.WriteLine($"Invalid ShapeId in line: {rawLine}");
                    continue;
                }

                string eventName = parts[1].Trim();
                string formula = parts[2].Trim();

                // Locate the shape across all pages
                Shape targetShape = null;
                foreach (Page page in diagram.Pages)
                {
                    try
                    {
                        // GetShape throws if not found; catch and continue
                        Shape s = page.Shapes.GetShape(shapeId);
                        if (s != null)
                        {
                            targetShape = s;
                            break;
                        }
                    }
                    catch
                    {
                        // Shape not on this page; continue searching
                    }
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found in diagram.");
                    continue;
                }

                // Apply the formula to the appropriate event cell
                // Map common event names to the corresponding Event properties
                // Supported events: EventDrop, EventDblClick, EventXFMod, EventMultiDrop, TheText, TheData
                switch (eventName.ToLowerInvariant())
                {
                    case "eventdrop":
                        targetShape.Event.EventDrop.Ufe.F = formula;
                        break;
                    case "eventdblclick":
                        targetShape.Event.EventDblClick.Ufe.F = formula;
                        break;
                    case "eventxfmod":
                        targetShape.Event.EventXFMod.Ufe.F = formula;
                        break;
                    case "eventmultidrop":
                        targetShape.Event.EventMultiDrop.Ufe.F = formula;
                        break;
                    case "thetext":
                        targetShape.Event.TheText.Ufe.F = formula;
                        break;
                    case "thedata":
                        targetShape.Event.TheData.Ufe.F = formula;
                        break;
                    default:
                        Console.Error.WriteLine($"Unsupported event name '{eventName}' for shape ID {shapeId}.");
                        break;
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}