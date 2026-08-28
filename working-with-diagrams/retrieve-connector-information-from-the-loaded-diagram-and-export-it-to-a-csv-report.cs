using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be processed
            string inputPath = "input.vsdx"; // TODO: replace with actual file path
            // Path where the CSV report will be saved
            string outputCsv = "connectors_report.csv";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a CSV file and write the header
            using (StreamWriter writer = new StreamWriter(outputCsv))
            {
                writer.WriteLine("PageName,FromShapeID,FromShapeName,ToShapeID,ToShapeName,FromCell,ToCell");

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    string pageName = page.Name ?? string.Empty;

                    // Iterate through each connector (Connect) on the page
                    foreach (Connect connect in page.Connects)
                    {
                        // Retrieve source and target shapes using their IDs
                        Shape fromShape = page.Shapes.GetShape(connect.FromSheet);
                        Shape toShape = page.Shapes.GetShape(connect.ToSheet);

                        string fromName = fromShape?.Name ?? string.Empty;
                        string toName = toShape?.Name ?? string.Empty;

                        // Build a CSV line, escaping fields that may contain commas or quotes
                        string line = string.Join(",",
                            Escape(pageName),
                            connect.FromSheet.ToString(),
                            Escape(fromName),
                            connect.ToSheet.ToString(),
                            Escape(toName),
                            Escape(connect.FromCell),
                            Escape(connect.ToCell));

                        writer.WriteLine(line);
                    }
                }
            }

            Console.WriteLine($"Connector report has been saved to '{outputCsv}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method to escape CSV fields containing commas, quotes, or line breaks
    static string Escape(string field)
    {
        if (field == null) return string.Empty;
        bool mustQuote = field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r");
        if (mustQuote)
        {
            field = field.Replace("\"", "\"\"");
            return $"\"{field}\"";
        }
        return field;
    }
}
