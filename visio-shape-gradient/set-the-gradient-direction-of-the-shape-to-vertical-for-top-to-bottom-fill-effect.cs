using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the source Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page.
            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape.
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Iterate over all shapes on the page and apply a vertical gradient fill.
            for (int i = 0; i < page.Shapes.Count; i++)
            {
                // Retrieve the shape by its ID.
                // The Shapes collection stores shape IDs; use GetShape to obtain the Shape object.
                long shapeId = page.Shapes[i].ID;
                Shape shape = page.Shapes.GetShape((int)shapeId);

                if (shape == null)
                    continue;

                // Set the fill pattern to gradient (value 25 as per Aspose.Diagram documentation).
                shape.Fill.FillPattern.Value = 25;

                // Enable gradient fill.
                shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                // Set gradient direction to vertical (top‑to‑bottom). 
                // In Visio, a value of 1 represents vertical direction.
                shape.Fill.GradientFill.GradientDir.Value = 1;

                // Optional: clear existing gradient stops and add two sample stops.
                shape.Fill.GradientFill.GradientStops.Clear();
                shape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(0, MeasureConst.NUM),
                    new ColorValue("#FF0000", MeasureConst.Undefined)); // Start color (red)
                shape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(1, MeasureConst.NUM),
                    new ColorValue("#0000FF", MeasureConst.Undefined)); // End color (blue)
            }

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}