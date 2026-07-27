using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: input diagram path, CSV file path, output diagram path
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: DiagramEventImporter <inputDiagram.vsdx> <events.csv> <outputDiagram.vsdx>");
                return;
            }

            string diagramPath = args[0];
            string csvPath = args[1];
            string outputPath = args[2];

            // Load the diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(diagramPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Verify CSV file exists
            if (!File.Exists(csvPath))
            {
                Console.WriteLine($"CSV file not found: {csvPath}");
                return;
            }

            // Process each line of the CSV
            // Expected CSV format: ShapeId,EventName,Formula
            // Example: 5,EventDblClick,"CALLTHIS(\"ThisDocument.ShowAlert\")"
            foreach (string line in File.ReadLines(csvPath))
            {
                // Skip empty lines
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Split by comma, respecting possible quoted commas
                string[] parts = SplitCsvLine(line);
                if (parts.Length < 3)
                {
                    Console.WriteLine($"Invalid CSV line (expected at least 3 columns): {line}");
                    continue;
                }

                // Parse ShapeId
                if (!long.TryParse(parts[0].Trim(), out long shapeId))
                {
                    Console.WriteLine($"Invalid ShapeId in line: {line}");
                    continue;
                }

                string eventName = parts[1].Trim();
                string formula = parts[2].Trim().Trim('\"'); // Remove surrounding quotes if present

                // Retrieve the shape from the first page (adjust if needed)
                Shape shape;
                try
                {
                    shape = diagram.Pages[0].Shapes.GetShape(shapeId);
                }
                catch (Exception)
                {
                    Console.WriteLine($"Shape with ID {shapeId} not found on page 0.");
                    continue;
                }

                // Apply the event formula based on the event name
                try
                {
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
                            Console.WriteLine($"Unsupported event name '{eventName}' for shape ID {shapeId}.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error setting event for shape ID {shapeId}: {ex.Message}");
                }
            }

            // Save the modified diagram
            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save diagram: {ex.Message}");
            }
        }

        // Simple CSV splitter handling commas inside double quotes
        private static string[] SplitCsvLine(string line)
        {
            var result = new System.Collections.Generic.List<string>();
            bool inQuotes = false;
            int start = 0;

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '\"')
                {
                    inQuotes = !inQuotes;
                }
                else if (line[i] == ',' && !inQuotes)
                {
                    result.Add(line.Substring(start, i - start));
                    start = i + 1;
                }
            }

            // Add the last field
            if (start <= line.Length)
                result.Add(line.Substring(start));

            return result.ToArray();
        }
    }