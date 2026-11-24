using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputCsvPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (Diagram implements IDisposable)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare CSV writer
                using (StreamWriter writer = new StreamWriter(outputPath, false))
                {
                    // Write CSV header
                    writer.WriteLine("PageIndex,ShapeID,ShapeName,EventDrop,EventDblClick,EventXFMod,EventMultiDrop,TheText,TheData");

                    // Helper to escape CSV fields
                    string Escape(string s)
                    {
                        if (s.Contains(",") || s.Contains("\"") || s.Contains("\n"))
                        {
                            s = s.Replace("\"", "\"\"");
                            return $"\"{s}\"";
                        }
                        return s;
                    }

                    // Iterate through all pages
                    for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                    {
                        Page page = diagram.Pages[pageIndex];

                        // Iterate through all shapes on the page
                        for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                        {
                            Shape shape = page.Shapes[shapeIndex];

                            // Skip deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            // Retrieve shape identifier and name
                            long shapeId = shape.ID;
                            string shapeName = shape.NameU ?? string.Empty;

                            // Retrieve event cell formulas, handling possible nulls
                            string eventDrop = shape.Event?.EventDrop?.Ufe?.F ?? string.Empty;
                            string eventDblClick = shape.Event?.EventDblClick?.Ufe?.F ?? string.Empty;
                            string eventXFMod = shape.Event?.EventXFMod?.Ufe?.F ?? string.Empty;
                            string eventMultiDrop = shape.Event?.EventMultiDrop?.Ufe?.F ?? string.Empty;
                            string theText = shape.Event?.TheText?.Ufe?.F ?? string.Empty;
                            string theData = shape.Event?.TheData?.Ufe?.F ?? string.Empty;

                            // Write CSV line
                            writer.WriteLine($"{pageIndex},{shapeId},{Escape(shapeName)},{Escape(eventDrop)},{Escape(eventDblClick)},{Escape(eventXFMod)},{Escape(eventMultiDrop)},{Escape(theText)},{Escape(theData)}");
                        }
                    }
                }
            }

            Console.WriteLine($"Event cell values exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}