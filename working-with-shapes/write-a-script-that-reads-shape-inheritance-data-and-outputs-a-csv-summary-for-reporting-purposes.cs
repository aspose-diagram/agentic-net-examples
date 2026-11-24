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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputCsvPath>");
            return;
        }

        string diagramPath = args[0];
        string csvPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Input diagram file not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Prepare the CSV writer.
            using (StreamWriter writer = new StreamWriter(csvPath, false))
            {
                // Write CSV header.
                writer.WriteLine("PageName,ShapeID,ShapeName,InheritFillForegnd,InheritLineColor");

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve shape identifier and name.
                        long shapeId = shape.ID;
                        string shapeName = shape.NameU ?? string.Empty;

                        // Retrieve inherited fill foreground color (if available).
                        string inheritFillForegnd = string.Empty;
                        if (shape.InheritFill != null && shape.InheritFill.FillForegnd != null && shape.InheritFill.FillForegnd.Value != null)
                        {
                            inheritFillForegnd = shape.InheritFill.FillForegnd.Value;
                        }

                        // Retrieve inherited line color (if available).
                        string inheritLineColor = string.Empty;
                        if (shape.InheritLine != null && shape.InheritLine.LineColor != null && shape.InheritLine.LineColor.Value != null)
                        {
                            inheritLineColor = shape.InheritLine.LineColor.Value;
                        }

                        // Write a CSV line for the current shape.
                        writer.WriteLine($"{EscapeCsv(page.NameU)},{shapeId},{EscapeCsv(shapeName)},{EscapeCsv(inheritFillForegnd)},{EscapeCsv(inheritLineColor)}");
                    }
                }
            }

            Console.WriteLine($"CSV summary successfully written to: {csvPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Helper method to escape CSV fields that may contain commas or quotes.
    private static string EscapeCsv(string field)
    {
        if (field == null)
            return string.Empty;

        if (field.Contains("\""))
            field = field.Replace("\"", "\"\"");

        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
            return $"\"{field}\"";

        return field;
    }
}