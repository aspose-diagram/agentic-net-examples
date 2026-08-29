using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Path for the generated CSV report
            string reportPath = "ShapeReport.csv";

            using (StreamWriter writer = new StreamWriter(reportPath))
            {
                // Write CSV header
                writer.WriteLine("ShapeID,Name,InheritFill,InheritLine");

                // Iterate through all pages and their shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve shape ID and name
                        long shapeId = shape.ID;
                        string shapeName = shape.Name ?? string.Empty;

                        // Determine inheritance status for Fill and Line
                        string inheritFill = shape.InheritFill != null ? "Inherited" : "NotInherited";
                        string inheritLine = shape.InheritLine != null ? "Inherited" : "NotInherited";

                        // Write a line to the CSV file
                        writer.WriteLine($"{shapeId},{EscapeCsv(shapeName)},{inheritFill},{inheritLine}");
                    }
                }
            }

            Console.WriteLine($"Report generated: {Path.GetFullPath(reportPath)}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method to escape CSV fields containing commas, quotes, or newlines
    static string EscapeCsv(string field)
    {
        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
        {
            field = field.Replace("\"", "\"\"");
            return $"\"{field}\"";
        }
        return field;
    }
}
