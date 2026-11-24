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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify the input file exists.
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

            // Get the first page (index 0). Adjust as needed.
            Page page = diagram.Pages[0];

            // Retrieve a shape by its ID. Here we use shape ID 1 as an example.
            Shape shape = page.Shapes.GetShape(1);
            if (shape == null)
            {
                Console.Error.WriteLine("Shape with ID 1 not found on the first page.");
                return;
            }

            // Enable gradient fill on the shape.
            shape.Fill.FillPattern.Value = 25;                     // Gradient fill pattern.
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Access the gradient stops collection.
            GradientStopCollection stops = shape.Fill.GradientFill.GradientStops;
            if (stops.Count == 0)
            {
                Console.Error.WriteLine("The shape does not contain any gradient stops.");
                return;
            }

            // Copy existing stops to a temporary list for modification.
            var stopList = new System.Collections.Generic.List<GradientStop>();
            foreach (GradientStop stop in stops)
            {
                stopList.Add(stop);
            }

            // Modify the color of the stop at index 1 (second stop) to pure green.
            if (stopList.Count > 1)
            {
                GradientStop targetStop = stopList[1];
                // Assign a new ColorValue representing pure green.
                targetStop.Color = new ColorValue("#00FF00", MeasureConst.Undefined);
            }
            else
            {
                Console.Error.WriteLine("The shape has fewer than two gradient stops; cannot modify index 1.");
                return;
            }

            // Clear existing stops and re‑add them with the updated color.
            shape.Fill.GradientFill.GradientStops.Clear();
            foreach (GradientStop stop in stopList)
            {
                shape.Fill.GradientFill.GradientStops.Add(stop.Position, stop.Color);
            }

            // Save the modified diagram to the specified output path.
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