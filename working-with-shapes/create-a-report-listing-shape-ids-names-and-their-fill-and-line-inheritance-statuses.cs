using System;
using System.IO;
using Aspose.Diagram;

class ShapeInheritanceReport
{
    static void Main(string[] args)
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Prepare a CSV file to store the report
            string reportPath = "ShapeInheritanceReport.csv";
            using (StreamWriter writer = new StreamWriter(reportPath))
            {
                // Write CSV header
                writer.WriteLine("ShapeID,ShapeName,InheritFill,InheritLine");

                // Iterate through all pages and their shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve shape ID and name
                        long shapeId = shape.ID;
                        string shapeName = shape.Name ?? string.Empty;

                        // Determine inheritance status
                        bool inheritsFill = shape.InheritFill != null;
                        bool inheritsLine = shape.InheritLine != null;

                        // Write a line to the CSV
                        writer.WriteLine($"{shapeId},{EscapeCsv(shapeName)},{inheritsFill},{inheritsLine}");
                    }
                }
            }

            Console.WriteLine($"Report generated at: {Path.GetFullPath(reportPath)}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper to escape commas and quotes in CSV fields
    private static string EscapeCsv(string field)
    {
        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
        {
            field = field.Replace("\"", "\"\"");
            return $"\"{field}\"";
        }
        return field;
    }
}
