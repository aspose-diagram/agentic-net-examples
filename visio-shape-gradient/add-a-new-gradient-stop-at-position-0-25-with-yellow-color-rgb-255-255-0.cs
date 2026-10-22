using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, shape ID to modify
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputPath> <outputPath> <shapeId>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string shapeIdArg = args[2];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse shape ID
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        try
        {
            // Load diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Assume we work with the first page; adjust if needed
                Page page = diagram.Pages[0];

                // Retrieve the shape by its ID
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on page 0.");
                    return;
                }

                // Enable gradient fill and set a solid fill pattern (25 = gradient fill)
                shape.Fill.FillPattern.Value = 25;
                shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                // Optional: set gradient direction (0 = left to right)
                shape.Fill.GradientFill.GradientDir.Value = 0;

                // Clear any existing gradient stops
                shape.Fill.GradientFill.GradientStops.Clear();

                // Add new gradient stop at position 0.25 with yellow color (#FFFF00)
                shape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(0.25, MeasureConst.NUM),
                    new ColorValue("#FFFF00", MeasureConst.Undefined));

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}