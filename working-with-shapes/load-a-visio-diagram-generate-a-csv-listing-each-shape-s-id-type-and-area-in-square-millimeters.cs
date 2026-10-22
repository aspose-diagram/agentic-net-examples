using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputCsvPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare the CSV writer.
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                // Write CSV header.
                writer.WriteLine("ShapeID,ShapeType,AreaSqMm");

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve shape ID.
                        long shapeId = shape.ID;

                        // Retrieve shape type as string.
                        string shapeType = shape.Type.ToString();

                        // Retrieve width and height (in inches) and compute area in square millimetres.
                        double widthInches = shape.XForm.Width.Value;
                        double heightInches = shape.XForm.Height.Value;
                        double areaInches = widthInches * heightInches;
                        double mmPerInch = 25.4;
                        double areaSqMm = areaInches * mmPerInch * mmPerInch;

                        // Write CSV line (rounded to two decimal places for readability).
                        writer.WriteLine($"{shapeId},{shapeType},{areaSqMm:F2}");
                    }
                }
            }

            Console.WriteLine($"CSV file generated successfully at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during loading or processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}