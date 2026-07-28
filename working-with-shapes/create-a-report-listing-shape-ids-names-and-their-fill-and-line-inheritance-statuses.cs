using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Prepare CSV header
            StringBuilder report = new StringBuilder();
            report.AppendLine("ShapeID,Name,InheritFill,InheritLine");

            // Iterate through all shapes on the first page (adjust if needed)
            foreach (Shape shape in diagram.Pages[0].Shapes)
            {
                long id = shape.ID;
                string name = shape.Name ?? string.Empty;

                // Determine inheritance status
                string inheritFill = shape.InheritFill != null ? "True" : "False";
                string inheritLine = shape.InheritLine != null ? "True" : "False";

                // Append CSV line (escaping commas and quotes in the name)
                report.AppendLine($"{id},{EscapeCsv(name)},{inheritFill},{inheritLine}");
            }

            // Write the report to a CSV file
            File.WriteAllText("ShapeReport.csv", report.ToString());

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper to escape CSV fields containing commas or quotes
    static string EscapeCsv(string field)
    {
        if (field.Contains(",") || field.Contains("\""))
        {
            field = field.Replace("\"", "\"\"");
            return $"\"{field}\"";
        }
        return field;
    }
}
