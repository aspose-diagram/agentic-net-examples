using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path as first argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the input Visio file path as an argument.");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define output path (same folder, with suffix "_updated.vsdx")
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_updated.vsdx");

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Batch add a rectangle shape to each page
            foreach (Page page in diagram.Pages)
            {
                // Add a rectangle at position (1,1). Use the built‑in "Rectangle" master.
                // The fourth parameter 'isCalculate' is set to false to avoid automatic layout.
                long shapeId = page.AddShape(1.0, 1.0, "Rectangle", false);

                // Retrieve the newly added shape (optional, in case further modifications are needed)
                Shape newShape = page.Shapes.GetShape(shapeId);
                // Example modification: set the shape's fill color (optional)
                // newShape.Fill.FillForegnd.Value = "#FFCC00";
            }

            // Validate that each page now contains at least one shape
            foreach (Page page in diagram.Pages)
            {
                if (page.Shapes.Count == 0)
                {
                    // If a page has no shapes, report and abort
                    string message = $"Validation failed: Page '{page.Name}' contains no shapes.";
                    Console.Error.WriteLine(message);
                    throw new Exception(message);
                }
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram processed and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}