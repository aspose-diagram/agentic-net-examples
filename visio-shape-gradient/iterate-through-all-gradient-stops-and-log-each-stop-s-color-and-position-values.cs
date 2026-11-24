using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through each shape on the current page
                    for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                    {
                        Shape shape = page.Shapes[shapeIndex];

                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if the shape has a gradient fill enabled
                        if (shape.Fill != null &&
                            shape.Fill.GradientFill != null &&
                            shape.Fill.GradientFill.GradientEnabled != null &&
                            shape.Fill.GradientFill.GradientEnabled.Value == BOOL.True)
                        {
                            // Iterate through each gradient stop
                            foreach (GradientStop stop in shape.Fill.GradientFill.GradientStops)
                            {
                                // Log shape ID, stop position and color
                                Console.WriteLine($"Shape ID: {shape.ID}, Stop Position: {stop.Position.Value}, Color: {stop.Color.Value}");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}