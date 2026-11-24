using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required by Aspose)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page
                Page page = diagram.Pages[0];

                // Ensure the page has at least one shape
                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The first page contains no shapes.");
                    return;
                }

                // Retrieve the first shape by its ID (cast long to int as required by GetShape)
                int firstShapeId = (int)page.Shapes[0].ID;
                Shape shape = page.Shapes.GetShape(firstShapeId);

                // Set the fill pattern to a gradient pattern (value 25)
                shape.Fill.FillPattern.Value = 25;

                // Enable gradient fill
                shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                // Set gradient direction to vertical (top‑to‑bottom). 0 = vertical.
                shape.Fill.GradientFill.GradientDir.Value = 0;

                // Remove any existing gradient stops
                shape.Fill.GradientFill.GradientStops.Clear();

                // Add a start gradient stop (position 0) – red color
                shape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(0, MeasureConst.NUM),
                    new ColorValue("#FF0000", MeasureConst.Undefined));

                // Add an end gradient stop (position 1) – green color
                shape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(1, MeasureConst.NUM),
                    new ColorValue("#00FF00", MeasureConst.Undefined));

                // Save the modified diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}