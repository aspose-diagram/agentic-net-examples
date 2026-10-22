using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has a gradient fill defined.
                        if (shape.Fill == null || shape.Fill.GradientFill == null)
                            continue;

                        // Access the collection of gradient stops.
                        GradientStopCollection stops = shape.Fill.GradientFill.GradientStops;

                        // If there are no stops, continue to next shape.
                        if (stops == null || stops.Count == 0)
                            continue;

                        Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}");

                        // Iterate explicitly using GradientStop type.
                        foreach (GradientStop stop in stops)
                        {
                            // Log position and color values of each stop.
                            // Position.Value is a double, Color.Value is a hex string.
                            Console.WriteLine($"  Stop Position: {stop.Position.Value}, Color: {stop.Color.Value}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}