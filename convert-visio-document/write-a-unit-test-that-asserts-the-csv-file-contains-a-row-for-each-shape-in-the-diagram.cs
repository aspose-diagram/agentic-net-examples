using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio diagram
            string inputPath = "input.vsdx";

            // Path for the exported CSV file
            string csvPath = "output.csv";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Export the diagram to CSV
            diagram.Save(csvPath, SaveFileFormat.Csv);

            // Count total shapes in the diagram (including all pages)
            int shapeCount = 0;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    shapeCount++;
                }
            }

            // Read all lines from the CSV file
            string[] csvLines = File.ReadAllLines(csvPath);

            // Assume the first line is a header; count data rows only
            int dataRowCount = csvLines.Length > 0 ? csvLines.Length - 1 : 0;

            // Validate that each shape has a corresponding CSV row
            if (dataRowCount != shapeCount)
            {
                throw new Exception($"CSV row count mismatch: expected {shapeCount} rows, but found {dataRowCount} rows.");
            }
            else
            {
                Console.WriteLine($"Success: CSV contains a row for each of the {shapeCount} shapes.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
