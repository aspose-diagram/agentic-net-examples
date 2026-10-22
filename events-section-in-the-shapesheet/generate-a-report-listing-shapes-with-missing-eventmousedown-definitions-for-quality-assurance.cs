using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputReportPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output report path (optional second argument)
        string outputPath = args.Length > 1 ? args[1] : "MissingEventMouseDownReport.txt";
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            // Create directory for the report if it does not exist
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                // Write CSV header
                writer.WriteLine("PageIndex,ShapeID,ShapeName,MasterName");

                // Iterate through all pages
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Guard against null Event object
                        if (shape.Event == null)
                            continue;

                        // Use EventDrop as the closest substitute for EventMouseDown
                        string mouseDownFormula = shape.Event.EventDrop?.Ufe?.F;

                        // If the formula is null or empty, consider the EventMouseDown definition missing
                        if (string.IsNullOrWhiteSpace(mouseDownFormula))
                        {
                            string shapeName = shape.Name ?? "N/A";
                            string masterName = shape.Master?.Name ?? "N/A";

                            // Write shape information to the report
                            writer.WriteLine($"{pageIndex},{shape.ID},{shapeName},{masterName}");
                        }
                    }
                }
            }

            Console.WriteLine($"Report generated successfully at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}