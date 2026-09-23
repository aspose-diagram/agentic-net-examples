using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output CSV file path
            string csvPath = "shape_events.csv";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            using (StreamWriter writer = new StreamWriter(csvPath))
            {
                // CSV header
                writer.WriteLine("PageName,ShapeID,ShapeNameU,EventXFMod,EventDblClick,EventDrop,EventMultiDrop,TheText,TheData");

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve event cell formulas (empty string if not set)
                        string eventXFMod = shape.Event.EventXFMod?.Ufe?.F ?? "";
                        string eventDblClick = shape.Event.EventDblClick?.Ufe?.F ?? "";
                        string eventDrop = shape.Event.EventDrop?.Ufe?.F ?? "";
                        string eventMultiDrop = shape.Event.EventMultiDrop?.Ufe?.F ?? "";
                        string theText = shape.Event.TheText?.Ufe?.F ?? "";
                        string theData = shape.Event.TheData?.Ufe?.F ?? "";

                        // Escape CSV fields
                        string pageNameEsc = EscapeCsv(page.Name);
                        string shapeIdEsc = shape.ID.ToString();
                        string shapeNameEsc = EscapeCsv(shape.NameU);
                        string xfModEsc = EscapeCsv(eventXFMod);
                        string dblClickEsc = EscapeCsv(eventDblClick);
                        string dropEsc = EscapeCsv(eventDrop);
                        string multiDropEsc = EscapeCsv(eventMultiDrop);
                        string theTextEsc = EscapeCsv(theText);
                        string theDataEsc = EscapeCsv(theData);

                        // Write CSV line
                        writer.WriteLine($"{pageNameEsc},{shapeIdEsc},{shapeNameEsc},{xfModEsc},{dblClickEsc},{dropEsc},{multiDropEsc},{theTextEsc},{theDataEsc}");
                    }
                }
            }

            Console.WriteLine($"Event cell values exported to {csvPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method to escape commas, quotes, and newlines in CSV fields
    static string EscapeCsv(string field)
    {
        if (field == null)
            return "";
        if (field.Contains("\""))
            field = field.Replace("\"", "\"\"");
        if (field.Contains(",") || field.Contains("\n") || field.Contains("\r"))
            return $"\"{field}\"";
        return field;
    }
}
