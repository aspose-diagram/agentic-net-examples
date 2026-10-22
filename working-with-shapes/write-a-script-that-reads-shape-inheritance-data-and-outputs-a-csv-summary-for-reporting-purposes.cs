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

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare the CSV output file.
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                // Write CSV header.
                writer.WriteLine("PageName,ShapeID,ShapeNameU,InheritFillForegnd,InheritLineColor");

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Use the page's universal name for reporting.
                    string pageName = page.NameU ?? string.Empty;

                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve shape identifiers.
                        long shapeId = shape.ID;
                        string shapeName = shape.NameU ?? string.Empty;

                        // Retrieve inheritance information.
                        string inheritFill = shape.InheritFill?.FillForegnd?.Value ?? string.Empty;
                        string inheritLine = shape.InheritLine?.LineColor?.Value ?? string.Empty;

                        // Write a CSV line for the current shape.
                        writer.WriteLine($"{EscapeCsv(pageName)},{shapeId},{EscapeCsv(shapeName)},{EscapeCsv(inheritFill)},{EscapeCsv(inheritLine)}");
                    }
                }
            }

            Console.WriteLine($"CSV summary successfully written to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
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