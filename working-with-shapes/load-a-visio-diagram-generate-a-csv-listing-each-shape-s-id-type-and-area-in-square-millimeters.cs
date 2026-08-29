using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input Visio file path (first argument or default).
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: ensure the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine output CSV file path (second argument or default).
        string outputPath = args.Length > 1 ? args[1] : "shapes.csv";

        // Load the Visio diagram inside a try/catch to capture Aspose errors.
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Open a StreamWriter for the CSV output; ensure proper disposal.
        using (var writer = new StreamWriter(outputPath))
        {
            // Write CSV header.
            writer.WriteLine("ShapeID,ShapeType,AreaSqMm");

            // Iterate over each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate over each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve shape ID (long) and type (enum).
                    long shapeId = shape.ID;
                    string shapeType = shape.Type.ToString();

                    // Retrieve width and height in inches from the shape's XForm.
                    double widthInches = shape.XForm.Width.Value;
                    double heightInches = shape.XForm.Height.Value;

                    // Compute area in square millimeters (1 inch = 25.4 mm).
                    double areaSqMm = widthInches * heightInches * 25.4 * 25.4;

                    // Write a CSV line with ID, type, and area (rounded to 2 decimals).
                    writer.WriteLine($"{shapeId},{shapeType},{areaSqMm:F2}");
                }
            }
        }

        // Inform the user that processing completed successfully.
        Console.WriteLine($"CSV file generated at: {outputPath}");
    }
}