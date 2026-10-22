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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputCsvPath>");
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

            // Prepare the CSV file for writing.
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                // Write CSV header.
                writer.WriteLine("PageIndex,ShapeId,NameU,MasterName,Text,Data1,Data2,Data3");

                // Iterate through each page in the diagram.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through each shape on the current page.
                    for (int i = 0; i < page.Shapes.Count; i++)
                    {
                        Shape shape = page.Shapes[i];

                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve shape properties safely.
                        long shapeId = shape.ID; // Shape.ID is a long.
                        string nameU = shape.NameU ?? string.Empty;
                        string masterName = shape.Master != null ? shape.Master.Name : string.Empty;
                        string text = shape.Text != null && shape.Text.Value != null ? shape.Text.Value.Text : string.Empty;
                        string data1 = shape.Data1 ?? string.Empty;
                        string data2 = shape.Data2 ?? string.Empty;
                        string data3 = shape.Data3 ?? string.Empty;

                        // Escape fields for CSV compliance.
                        string csvLine = string.Join(",",
                            EscapeCsv(pageIndex.ToString()),
                            EscapeCsv(shapeId.ToString()),
                            EscapeCsv(nameU),
                            EscapeCsv(masterName),
                            EscapeCsv(text),
                            EscapeCsv(data1),
                            EscapeCsv(data2),
                            EscapeCsv(data3));

                        writer.WriteLine(csvLine);
                    }
                }
            }

            Console.WriteLine($"Shape data successfully exported to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Escapes a CSV field by wrapping it in double quotes and escaping internal quotes.
    /// </summary>
    private static string EscapeCsv(string field)
    {
        if (field.Contains("\"") || field.Contains(",") || field.Contains("\n") || field.Contains("\r"))
        {
            field = field.Replace("\"", "\"\"");
            return $"\"{field}\"";
        }
        return field;
    }
}