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
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: ensure the directory for the output file exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        Diagram diagram = null;
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
            // Open a StreamWriter for the CSV output
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                // Write CSV header
                writer.WriteLine("ShapeID,ShapeType,AreaSqMm");

                // Iterate through each page and each shape
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve shape ID
                        long shapeId = shape.ID;

                        // Retrieve shape type as string
                        string shapeType = shape.Type.ToString();

                        // Retrieve width and height (in inches) and compute area in square millimeters
                        double widthInches = shape.XForm.Width.Value;
                        double heightInches = shape.XForm.Height.Value;
                        double areaSqInches = widthInches * heightInches;
                        double areaSqMm = areaSqInches * 25.4 * 25.4;

                        // Write CSV line
                        writer.WriteLine($"{shapeId},{shapeType},{areaSqMm:F2}");
                    }
                }
            }

            Console.WriteLine($"CSV file generated successfully at: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while generating CSV: {ex.Message}");
        }
        finally
        {
            // Dispose diagram if needed
            diagram?.Dispose();
        }
    }
}