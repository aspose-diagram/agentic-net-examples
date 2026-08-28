using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths – adjust as needed
            string diagramPath = "input.vsdx";
            string csvPath = "events.csv";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Verify CSV file exists
            if (!File.Exists(csvPath))
            {
                Console.WriteLine($"CSV file not found: {csvPath}");
                return;
            }

            // Read all lines from the CSV (expected format: ShapeId,EventName,Formula)
            string[] lines = File.ReadAllLines(csvPath);
            foreach (string rawLine in lines)
            {
                // Skip empty or whitespace-only lines
                if (string.IsNullOrWhiteSpace(rawLine))
                    continue;

                // Split by comma – simple CSV parsing (no quoted fields handling)
                string[] parts = rawLine.Split(',');
                if (parts.Length < 3)
                {
                    Console.WriteLine($"Invalid line (expected at least 3 columns): {rawLine}");
                    continue;
                }

                // Parse ShapeId
                if (!long.TryParse(parts[0].Trim(), out long shapeId))
                {
                    Console.WriteLine($"Invalid ShapeId value: {parts[0]}");
                    continue;
                }

                string eventName = parts[1].Trim();
                string formula = parts[2].Trim();

                // Locate the shape by ID across all pages
                Shape shape = null;
                foreach (Page page in diagram.Pages)
                {
                    shape = page.Shapes.GetShape(shapeId);
                    if (shape != null)
                        break;
                }

                if (shape == null)
                {
                    Console.WriteLine($"Shape with ID {shapeId} not found.");
                    continue;
                }

                // Apply the event formula to the appropriate event cell
                switch (eventName)
                {
                    case "EventXFMod":
                        shape.Event.EventXFMod.Ufe.F = formula;
                        break;
                    case "EventDblClick":
                        shape.Event.EventDblClick.Ufe.F = formula;
                        break;
                    case "EventDrop":
                        shape.Event.EventDrop.Ufe.F = formula;
                        break;
                    case "EventMultiDrop":
                        shape.Event.EventMultiDrop.Ufe.F = formula;
                        break;
                    case "TheText":
                        shape.Event.TheText.Ufe.F = formula;
                        break;
                    case "TheData":
                        shape.Event.TheData.Ufe.F = formula;
                        break;
                    default:
                        Console.WriteLine($"Unsupported event name: {eventName}");
                        break;
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Csv);
            Console.WriteLine($"Diagram saved to {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
