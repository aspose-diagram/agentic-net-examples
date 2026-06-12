using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file (adjust as needed)
            string inputPath = "input.vsdx";

            // Path for the generated CSV file
            string outputCsv = "UserCells.csv";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a StreamWriter for CSV output
            using (StreamWriter writer = new StreamWriter(outputCsv))
            {
                // Write CSV header
                writer.WriteLine("ShapeID,UserCellName,UserCellValue,Prompt");

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        long shapeId = shape.ID;

                        // Iterate through user-defined cells of the shape
                        foreach (User userCell in shape.Users)
                        {
                            string name = userCell.Name ?? string.Empty;
                            string value = userCell.Value?.Val ?? string.Empty;
                            string prompt = userCell.Prompt?.Value ?? string.Empty;

                            // Escape commas by enclosing fields in double quotes
                            writer.WriteLine($"{shapeId},\"{name}\",\"{value}\",\"{prompt}\"");
                        }
                    }
                }
            }

            Console.WriteLine($"User-defined cell data exported to: {outputCsv}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
