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
            Console.Error.WriteLine("Usage: Program <inputFilePath> <outputFilePath>");
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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through pages to find the first non‑deleted shape.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                    {
                        Shape shape = page.Shapes[shapeIndex];

                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Enable gradient fill and set fill pattern to gradient.
                        shape.Fill.FillPattern.Value = 25; // Gradient fill pattern.
                        shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                        // Clear any existing gradient stops.
                        shape.Fill.GradientFill.GradientStops.Clear();

                        // Add a new gradient stop at position 0.75 with blue color (RGB 0,0,255).
                        shape.Fill.GradientFill.GradientStops.Add(
                            new DoubleValue(0.75, MeasureConst.NUM),
                            new ColorValue("#0000FF", MeasureConst.Undefined));

                        // Gradient stop added – exit loops after first applicable shape.
                        goto SaveDiagram;
                    }
                }

            SaveDiagram:
                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}