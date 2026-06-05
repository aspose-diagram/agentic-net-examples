using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: input Visio file, CSV file, output Visio file
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: DiagramEventImporter <inputVisio> <eventsCsv> <outputVisio>");
                return;
            }

            string inputVisioPath = args[0];
            string csvPath = args[1];
            string outputVisioPath = args[2];

            // Load the diagram
            Diagram diagram = new Diagram(inputVisioPath);

            // Read all lines from the CSV (skip empty lines)
            string[] lines = File.ReadAllLines(csvPath);
            if (lines.Length == 0)
            {
                Console.WriteLine("CSV file is empty.");
                return;
            }

            // Assume first line is a header; start processing from the second line
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (string.IsNullOrEmpty(line))
                    continue;

                // Expected CSV format: ShapeId,EventName,Formula
                string[] parts = line.Split(',');
                if (parts.Length < 3)
                {
                    Console.WriteLine($"Invalid CSV line at {i + 1}: {line}");
                    continue;
                }

                // Parse shape ID
                if (!long.TryParse(parts[0].Trim(), out long shapeId))
                {
                    Console.WriteLine($"Invalid ShapeId at line {i + 1}: {parts[0]}");
                    continue;
                }

                string eventName = parts[1].Trim();
                string formula = parts[2].Trim();

                // Retrieve the shape from the first page (adjust if needed)
                Page page = diagram.Pages[0];
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.WriteLine($"Shape with ID {shapeId} not found on page 0.");
                    continue;
                }

                // Apply the event formula based on the event name
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
                        Console.WriteLine($"Unsupported event name '{eventName}' at line {i + 1}.");
                        break;
                }
            }

            // Save the modified diagram
            diagram.Save(outputVisioPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputVisioPath}");
        }
    }