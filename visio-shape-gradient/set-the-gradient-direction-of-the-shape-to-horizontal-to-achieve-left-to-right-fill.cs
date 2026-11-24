using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, output file, optional shape ID
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> [shapeId]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        int shapeId = 0; // default to first shape if not provided

        if (args.Length >= 3 && !int.TryParse(args[2], out shapeId))
        {
            Console.Error.WriteLine("Invalid shapeId argument.");
            return;
        }

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (index 0)
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }
            Page page = diagram.Pages[0];

            // Determine which shape to modify
            Shape shape = null;
            if (shapeId != 0)
            {
                // Retrieve shape by provided ID
                shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on the first page.");
                    return;
                }
            }
            else
            {
                // Use the first shape on the page if no ID supplied
                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The first page contains no shapes.");
                    return;
                }
                // Shapes collection is indexed by long IDs; get the first ID
                long firstId = page.Shapes[0].ID;
                shape = page.Shapes.GetShape(firstId);
            }

            // Enable gradient fill
            shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set gradient direction to horizontal (left‑to‑right)
            // In Visio, 0 represents horizontal direction
            shape.Fill.GradientFill.GradientDir.Value = 0;

            // Optional: define gradient stops (left color to right color)
            shape.Fill.GradientFill.GradientStops.Clear();
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#FF0000", MeasureConst.Undefined)); // Red at start
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#00FF00", MeasureConst.Undefined)); // Green at end

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}