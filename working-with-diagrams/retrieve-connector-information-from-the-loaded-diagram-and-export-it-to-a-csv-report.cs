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

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare the CSV writer.
                using (StreamWriter writer = new StreamWriter(outputPath, false))
                {
                    // Write CSV header.
                    writer.WriteLine("PageName,FromShapeID,FromShapeName,ToShapeID,ToShapeName");

                    // Iterate through each page in the diagram.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through each connector (Connect) on the current page.
                        foreach (Connect connect in page.Connects)
                        {
                            // Retrieve source and target shape IDs.
                            long fromId = connect.FromSheet;
                            long toId = connect.ToSheet;

                            // Attempt to get shape names; fallback to empty string if not found.
                            string fromName = string.Empty;
                            string toName = string.Empty;

                            try
                            {
                                Shape fromShape = page.Shapes.GetShape(fromId);
                                if (fromShape != null)
                                    fromName = fromShape.Name ?? string.Empty;
                            }
                            catch { /* Ignore errors retrieving source shape */ }

                            try
                            {
                                Shape toShape = page.Shapes.GetShape(toId);
                                if (toShape != null)
                                    toName = toShape.Name ?? string.Empty;
                            }
                            catch { /* Ignore errors retrieving target shape */ }

                            // Write a CSV line for the connector.
                            writer.WriteLine($"{EscapeCsv(page.Name)},{fromId},{EscapeCsv(fromName)},{toId},{EscapeCsv(toName)}");
                        }
                    }
                }

                Console.WriteLine($"Connector report successfully written to: {outputPath}");
            }
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

        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
        {
            string escaped = field.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
        return field;
    }
}