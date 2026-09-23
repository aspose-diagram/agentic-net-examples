using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio diagram to modify
                string diagramPath = "input.vsdx";

                // Path to the CSV file containing event definitions
                // Expected CSV columns: ShapeName,EventName,Formula
                string csvPath = "events.csv";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Read all event definitions from the CSV file
                List<EventDefinition> events = LoadEventDefinitions(csvPath);

                // Apply each event definition to the matching shape
                foreach (EventDefinition ev in events)
                {
                    bool shapeFound = false;

                    // Search through all pages and shapes for the target shape name
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            if (string.Equals(shape.NameU, ev.ShapeName, StringComparison.OrdinalIgnoreCase))
                            {
                                ApplyEventToShape(shape, ev);
                                shapeFound = true;
                                break;
                            }
                        }

                        if (shapeFound) break;
                    }

                    if (!shapeFound)
                    {
                        Console.WriteLine($"Warning: Shape \"{ev.ShapeName}\" not found in diagram.");
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Csv);
                Console.WriteLine($"Diagram saved to {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Represents a single event definition from the CSV file
        private class EventDefinition
        {
            public string ShapeName { get; set; } = string.Empty;
            public string EventName { get; set; } = string.Empty;
            public string Formula { get; set; } = string.Empty;
        }

        // Loads event definitions from a CSV file
        private static List<EventDefinition> LoadEventDefinitions(string csvPath)
        {
            var list = new List<EventDefinition>();

            if (!File.Exists(csvPath))
            {
                Console.WriteLine($"Error: CSV file \"{csvPath}\" not found.");
                return list;
            }

            using (var reader = new StreamReader(csvPath))
            {
                bool isFirstLine = true;
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Skip header line
                    if (isFirstLine)
                    {
                        isFirstLine = false;
                        continue;
                    }

                    // Simple CSV split (no quoted commas handling)
                    string[] parts = line.Split(',');

                    if (parts.Length < 3)
                        continue; // malformed line

                    var ev = new EventDefinition
                    {
                        ShapeName = parts[0].Trim(),
                        EventName = parts[1].Trim(),
                        Formula   = parts[2].Trim()
                    };
                    list.Add(ev);
                }
            }

            return list;
        }

        // Applies the specified event to the given shape
        private static void ApplyEventToShape(Shape shape, EventDefinition ev)
        {
            // Ensure the shape's Event section is available
            if (shape.Event == null)
            {
                Console.WriteLine($"Error: Shape \"{shape.NameU}\" does not have an Event section.");
                return;
            }

            // Map the event name from CSV to the corresponding property
            switch (ev.EventName.ToLowerInvariant())
            {
                case "eventdblclick":
                    shape.Event.EventDblClick.Ufe.F = ev.Formula;
                    break;
                case "eventdrop":
                    shape.Event.EventDrop.Ufe.F = ev.Formula;
                    break;
                case "eventmultidrop":
                    shape.Event.EventMultiDrop.Ufe.F = ev.Formula;
                    break;
                case "eventxfmod":
                    shape.Event.EventXFMod.Ufe.F = ev.Formula;
                    break;
                case "thetext":
                    shape.Event.TheText.Ufe.F = ev.Formula;
                    break;
                case "thedata":
                    shape.Event.TheData.Ufe.F = ev.Formula;
                    break;
                default:
                    Console.WriteLine($"Warning: Unsupported event name \"{ev.EventName}\" for shape \"{shape.NameU}\".");
                    break;
            }
        }
    }