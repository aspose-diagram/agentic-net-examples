using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has a gradient fill enabled.
                        if (shape.Fill?.GradientFill?.GradientEnabled?.Value != BOOL.True)
                            continue;

                        // Access the gradient stops collection.
                        var gradientStops = shape.Fill.GradientFill.GradientStops;

                        // Iterate over each gradient stop and shift its position.
                        foreach (GradientStop stop in gradientStops)
                        {
                            // Current position (0‑1 range).
                            double currentPos = stop.Position?.Value ?? 0.0;

                            // Shift by +0.1 and clamp to 1.0.
                            double newPos = currentPos + 0.1;
                            if (newPos > 1.0)
                                newPos = 1.0;

                            // Assign the new position back to the stop.
                            if (stop.Position != null)
                                stop.Position.Value = newPos;
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}