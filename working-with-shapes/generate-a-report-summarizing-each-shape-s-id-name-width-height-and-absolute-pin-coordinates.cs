using System;
using System.Text;
using System.IO;
using Aspose.Diagram;

class ShapeReport
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            StringBuilder sb = new StringBuilder();
            // CSV header
            sb.AppendLine("ShapeID,Name,Width,Height,PinX,PinY");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve required properties
                    long id = shape.ID;
                    string name = shape.Name ?? string.Empty;
                    double width = shape.XForm.Width.Value;
                    double height = shape.XForm.Height.Value;
                    double pinX = shape.XForm.PinX.Value;
                    double pinY = shape.XForm.PinY.Value;

                    // Append a CSV line for the shape
                    sb.AppendLine($"{id},{EscapeCsv(name)},{width},{height},{pinX},{pinY}");
                }
            }

            // Save the report to a CSV file
            File.WriteAllText("ShapeReport.csv", sb.ToString());

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
