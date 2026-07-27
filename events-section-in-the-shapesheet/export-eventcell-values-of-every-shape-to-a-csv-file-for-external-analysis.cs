using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string diagramPath = "input.vsdx";

                // Output CSV file path
                string csvPath = "shape_events.csv";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Create a CSV writer
                using (StreamWriter writer = new StreamWriter(csvPath))
                {
                    // Write CSV header
                    writer.WriteLine("PageName,ShapeID,ShapeName,EventXFMod,EventDblClick,EventDrop,EventMultiDrop,TheText,TheData");

                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Retrieve event cell formulas; use empty string if null
                            string eventXFMod = shape.Event.EventXFMod?.Ufe?.F ?? string.Empty;
                            string eventDblClick = shape.Event.EventDblClick?.Ufe?.F ?? string.Empty;
                            string eventDrop = shape.Event.EventDrop?.Ufe?.F ?? string.Empty;
                            string eventMultiDrop = shape.Event.EventMultiDrop?.Ufe?.F ?? string.Empty;
                            string theText = shape.Event.TheText?.Ufe?.F ?? string.Empty;
                            string theData = shape.Event.TheData?.Ufe?.F ?? string.Empty;

                            // Escape commas in values
                            eventXFMod = EscapeCsv(eventXFMod);
                            eventDblClick = EscapeCsv(eventDblClick);
                            eventDrop = EscapeCsv(eventDrop);
                            eventMultiDrop = EscapeCsv(eventMultiDrop);
                            theText = EscapeCsv(theText);
                            theData = EscapeCsv(theData);

                            // Write a CSV line for the shape
                            writer.WriteLine($"{EscapeCsv(page.Name)},{shape.ID},{EscapeCsv(shape.Name)},{eventXFMod},{eventDblClick},{eventDrop},{eventMultiDrop},{theText},{theData}");
                        }
                    }
                }

                Console.WriteLine($"Event cell values exported to '{csvPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Helper to escape CSV fields containing commas or quotes
        private static string EscapeCsv(string field)
        {
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
            {
                field = field.Replace("\"", "\"\"");
                return $"\"{field}\"";
            }
            return field;
        }
    }