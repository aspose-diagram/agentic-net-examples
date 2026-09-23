using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (VSD) file.
            // Adjust the input path as needed.
            string inputPath = "input.vsd";
            Diagram diagram = new Diagram(inputPath);

            // Define the output CSV file path.
            string outputCsvPath = "output.csv";

            // Use a FileStream inside a using block to write CSV data.
            using (FileStream fs = new FileStream(outputCsvPath, FileMode.Create, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                // Write CSV header.
                writer.WriteLine("Page,ShapeId,ShapeText");

                // Iterate through each page and each shape to extract data.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve plain text from the shape.
                        string shapeText = shape.Text.Value.Text;

                        // Escape commas in text to keep CSV format valid.
                        if (shapeText.Contains(","))
                        {
                            shapeText = $"\"{shapeText.Replace("\"", "\"\"")}\"";
                        }

                        // Write a CSV line: Page number (1‑based), Shape ID, Shape text.
                        writer.WriteLine($"{pageIndex + 1},{shape.ID},{shapeText}");
                    }
                }
            }

            Console.WriteLine($"Diagram data has been exported to CSV at: {outputCsvPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
