using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output CSV file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputCsvPath>");
            return;
        }

        string inputPath = args[0];
        string outputCsvPath = args[1];

        // Guard: verify input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure output directory exists
        string outputDir = Path.GetDirectoryName(outputCsvPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        Diagram diagram;
        try
        {
            // Load the Visio diagram
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        try
        {
            // Export the diagram to CSV format
            diagram.Save(outputCsvPath, SaveFileFormat.Csv);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save CSV: {ex.Message}");
            return;
        }

        // Count non-deleted shapes across all pages
        int shapeCount = 0;
        try
        {
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Exclude shapes marked as deleted
                    if (shape.Del == BOOL.False)
                    {
                        shapeCount++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while counting shapes: {ex.Message}");
            return;
        }

        // Verify the CSV file exists after saving
        if (!File.Exists(outputCsvPath))
        {
            Console.Error.WriteLine($"CSV file was not created: {outputCsvPath}");
            return;
        }

        // Read CSV lines and determine number of data rows (excluding possible header)
        int csvDataRows;
        try
        {
            string[] lines = File.ReadAllLines(outputCsvPath);
            csvDataRows = lines.Length;

            // If the first line contains metadata (e.g., "Application"), treat it as a header
            if (csvDataRows > 0 && lines[0].Contains("Application"))
            {
                csvDataRows--; // exclude header line
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read CSV file: {ex.Message}");
            return;
        }

        // Assert that each shape has a corresponding CSV row
        if (csvDataRows != shapeCount)
        {
            throw new Exception($"CSV row count ({csvDataRows}) does not match shape count ({shapeCount}).");
        }

        Console.WriteLine("Test passed: CSV contains a row for each shape in the diagram.");
    }
}