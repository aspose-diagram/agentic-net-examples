using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output CSV file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramEventExport <inputVisioFile> <outputCsvFile>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare CSV writer
            using (StreamWriter writer = new StreamWriter(outputPath, false, Encoding.UTF8))
            {
                // Write CSV header
                writer.WriteLine("PageName,ShapeID,ShapeNameU,EventXFMod,EventDblClick,EventDrop,EventMultiDrop,TheText,TheData");

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve event cell formulas (may be empty)
                        string eventXFMod = shape.Event.EventXFMod?.Ufe?.F ?? string.Empty;
                        string eventDblClick = shape.Event.EventDblClick?.Ufe?.F ?? string.Empty;
                        string eventDrop = shape.Event.EventDrop?.Ufe?.F ?? string.Empty;
                        string eventMultiDrop = shape.Event.EventMultiDrop?.Ufe?.F ?? string.Empty;
                        string theText = shape.Event.TheText?.Ufe?.F ?? string.Empty;
                        string theData = shape.Event.TheData?.Ufe?.F ?? string.Empty;

                        // Build CSV line with proper escaping
                        List<string> fields = new List<string>
                        {
                            page.Name,
                            shape.ID.ToString(),
                            shape.NameU ?? string.Empty,
                            eventXFMod,
                            eventDblClick,
                            eventDrop,
                            eventMultiDrop,
                            theText,
                            theData
                        };

                        writer.WriteLine(BuildCsvLine(fields));
                    }
                }
            }

            Console.WriteLine($"Event cell values exported successfully to '{outputPath}'.");
        }

        // Helper method to escape CSV fields according to RFC 4180
        private static string BuildCsvLine(IEnumerable<string> fields)
        {
            StringBuilder sb = new StringBuilder();
            bool first = true;
            foreach (string field in fields)
            {
                if (!first)
                    sb.Append(',');

                string escaped = field ?? string.Empty;
                bool mustQuote = escaped.Contains(',') || escaped.Contains('\"') || escaped.Contains('\n') || escaped.Contains('\r');

                if (mustQuote)
                {
                    escaped = escaped.Replace("\"", "\"\"");
                    sb.Append('\"').Append(escaped).Append('\"');
                }
                else
                {
                    sb.Append(escaped);
                }

                first = false;
            }
            return sb.ToString();
        }
    }