using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
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
                // Ensure there is at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Access the first page.
                Page page = diagram.Pages[0];

                // Ensure the page has at least one shape.
                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The first page contains no shapes.");
                    return;
                }

                // Retrieve the first shape (indexer uses int).
                Shape shape = page.Shapes[0];

                // Skip logically deleted shapes.
                if (shape.Del == BOOL.True)
                {
                    Console.Error.WriteLine("The selected shape is marked as deleted.");
                    return;
                }

                // Access the gradient fill collection.
                GradientFill gradientFill = shape.Fill.GradientFill;
                GradientStopCollection stopsCollection = gradientFill.GradientStops;

                // Ensure there are at least two gradient stops.
                if (stopsCollection.Count <= 1)
                {
                    Console.Error.WriteLine("The shape does not have a gradient stop at index 1.");
                    return;
                }

                // Copy existing stops to a temporary list.
                List<GradientStop> tempStops = new List<GradientStop>();
                foreach (GradientStop stop in stopsCollection)
                {
                    tempStops.Add(stop);
                }

                // Modify the position of the stop at index 1 to 0.5 (midpoint).
                GradientStop targetStop = tempStops[1];
                // Create a new DoubleValue for the position (0.5, numeric unit).
                targetStop.Position = new DoubleValue(0.5, MeasureConst.NUM);
                // Update the list entry.
                tempStops[1] = targetStop;

                // Clear existing stops and re‑add them with the updated values.
                stopsCollection.Clear();
                foreach (GradientStop stop in tempStops)
                {
                    // Add expects position and color values.
                    stopsCollection.Add(stop.Position, stop.Color);
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Gradient stop position updated and diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}