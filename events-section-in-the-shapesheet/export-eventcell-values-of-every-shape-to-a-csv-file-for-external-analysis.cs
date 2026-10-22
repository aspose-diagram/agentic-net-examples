using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputCsvFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare the CSV writer.
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                // Write CSV header.
                writer.WriteLine("PageIndex,ShapeId,ShapeNameU,EventDrop,EventDblClick,EventXFMod,EventMultiDrop,TheText,TheData");

                // Iterate through all pages.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve shape identifiers.
                        long shapeId = shape.ID;
                        string shapeNameU = shape.NameU ?? string.Empty;

                        // Retrieve event formulas, guarding against null references.
                        string eventDrop = string.Empty;
                        string eventDblClick = string.Empty;
                        string eventXFMod = string.Empty;
                        string eventMultiDrop = string.Empty;
                        string theText = string.Empty;
                        string theData = string.Empty;

                        if (shape.Event != null)
                        {
                            eventDrop = shape.Event.EventDrop?.Ufe?.F ?? string.Empty;
                            eventDblClick = shape.Event.EventDblClick?.Ufe?.F ?? string.Empty;
                            eventXFMod = shape.Event.EventXFMod?.Ufe?.F ?? string.Empty;
                            eventMultiDrop = shape.Event.EventMultiDrop?.Ufe?.F ?? string.Empty;
                            theText = shape.Event.TheText?.Ufe?.F ?? string.Empty;
                            theData = shape.Event.TheData?.Ufe?.F ?? string.Empty;
                        }

                        // Escape fields for CSV compliance.
                        shapeNameU = EscapeCsv(shapeNameU);
                        eventDrop = EscapeCsv(eventDrop);
                        eventDblClick = EscapeCsv(eventDblClick);
                        eventXFMod = EscapeCsv(eventXFMod);
                        eventMultiDrop = EscapeCsv(eventMultiDrop);
                        theText = EscapeCsv(theText);
                        theData = EscapeCsv(theData);

                        // Write a CSV line for the current shape.
                        writer.WriteLine($"{pageIndex},{shapeId},\"{shapeNameU}\",\"{eventDrop}\",\"{eventDblClick}\",\"{eventXFMod}\",\"{eventMultiDrop}\",\"{theText}\",\"{theData}\"");
                    }
                }
            }

            Console.WriteLine($"Event cell data exported successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Helper method to escape CSV fields containing double quotes, commas, or newlines.
    private static string EscapeCsv(string field)
    {
        if (field.Contains("\""))
        {
            field = field.Replace("\"", "\"\"");
        }

        if (field.Contains(",") || field.Contains("\n") || field.Contains("\r"))
        {
            field = $"\"{field}\"";
        }

        return field;
    }
}