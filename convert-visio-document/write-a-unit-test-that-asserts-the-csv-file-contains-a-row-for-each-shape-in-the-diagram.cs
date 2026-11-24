using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument). If not provided, show usage and exit.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];
        string csvPath = "shapes_export.csv";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Count all non-deleted shapes across all pages.
            int shapeCount = 0;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    shapeCount++;
                }
            }

            // Export the diagram to CSV format.
            diagram.Save(csvPath, SaveFileFormat.Csv);
            Console.WriteLine($"Diagram exported to CSV: {csvPath}");

            // Verify the CSV file was created.
            if (!File.Exists(csvPath))
            {
                Console.Error.WriteLine($"Failed to create CSV file: {csvPath}");
                return;
            }

            // Read all lines from the CSV file.
            string[] csvLines = File.ReadAllLines(csvPath);
            if (csvLines.Length == 0)
            {
                throw new Exception("CSV file is empty.");
            }

            // Assume the first line is a header; count remaining lines as data rows.
            int dataRowCount = csvLines.Length - 1;
            if (dataRowCount < 0) dataRowCount = 0; // safety

            // Assert that each shape has a corresponding CSV row.
            if (dataRowCount != shapeCount)
            {
                throw new Exception($"Shape count mismatch: Diagram has {shapeCount} shapes, but CSV contains {dataRowCount} data rows.");
            }

            Console.WriteLine("Test passed: CSV contains a row for each shape in the diagram.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}