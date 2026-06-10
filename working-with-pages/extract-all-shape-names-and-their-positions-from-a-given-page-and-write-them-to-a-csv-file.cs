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

            // Select the page you want to process (here we use the first page)
            Page page = diagram.Pages[0];

            // Create a CSV file to store shape names and positions
            using (StreamWriter writer = new StreamWriter("shapes.csv"))
            {
                // Write CSV header
                writer.WriteLine("ShapeName,PinX,PinY");

                // Iterate through all shapes on the selected page
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the shape's name (fallback to universal name if needed)
                    string shapeName = shape.Name ?? shape.NameU ?? string.Empty;

                    // Retrieve the shape's position (PinX and PinY are in inches)
                    double pinX = shape.XForm.PinX.Value;
                    double pinY = shape.XForm.PinY.Value;

                    // Write a CSV line, escaping the name if it contains special characters
                    writer.WriteLine($"{EscapeCsv(shapeName)},{pinX},{pinY}");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method to escape commas, quotes, and newlines for CSV compliance
    static string EscapeCsv(string value)
    {
        if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
        {
            value = value.Replace("\"", "\"\"");
            return $"\"{value}\"";
        }
        return value;
    }
}
