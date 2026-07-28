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

            // Select the page you want to process (0‑based index)
            int pageIndex = 0;
            Page page = diagram.Pages[pageIndex];

            // Prepare CSV content
            StringBuilder csv = new StringBuilder();
            csv.AppendLine("ShapeName,PinX,PinY");

            // Iterate through all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Shape name (fallback to empty string if null)
                string name = shape.Name ?? string.Empty;

                // Position values (fallback to 0 if not available)
                double pinX = shape.XForm?.PinX?.Value ?? 0;
                double pinY = shape.XForm?.PinY?.Value ?? 0;

                // Escape commas or quotes in the name
                string escapedName = EscapeCsv(name);

                csv.AppendLine($"{escapedName},{pinX},{pinY}");
            }

            // Write CSV to file (replace with desired output path)
            File.WriteAllText("shapes.csv", csv.ToString());

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper to escape CSV fields containing special characters
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
