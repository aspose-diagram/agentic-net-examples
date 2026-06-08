using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path (change as needed)
            string inputPath = "input.vsdx";
            // Output CSV file path
            string outputCsv = "connectors_report.csv";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            try
            {
                using (StreamWriter writer = new StreamWriter(outputCsv))
                {
                    // Write CSV header
                    writer.WriteLine("PageName,ConnectorFromShapeId,ConnectorFromShapeName,ConnectorToShapeId,ConnectorToShapeName");

                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all connections on the page
                        foreach (Connect connection in page.Connects)
                        {
                            long fromId = connection.FromSheet;
                            long toId = connection.ToSheet;

                            // Retrieve source and target shapes
                            Shape fromShape = page.Shapes.GetShape(fromId);
                            Shape toShape = page.Shapes.GetShape(toId);

                            string fromName = fromShape != null ? fromShape.Name : string.Empty;
                            string toName = toShape != null ? toShape.Name : string.Empty;

                            // Write a CSV line for this connector
                            writer.WriteLine($"{page.Name},{fromId},{EscapeCsv(fromName)},{toId},{EscapeCsv(toName)}");
                        }
                    }
                }

                Console.WriteLine($"Connector information exported successfully to '{outputCsv}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while exporting connector data:");
                Console.WriteLine(ex.Message);
                throw;
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper to escape commas and quotes in CSV fields
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
