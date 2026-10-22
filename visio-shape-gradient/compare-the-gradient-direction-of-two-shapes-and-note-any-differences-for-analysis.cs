using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file path, first shape ID, second shape ID
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <shapeId1> <shapeId2>");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        if (!long.TryParse(args[1], out long shapeId1))
        {
            Console.Error.WriteLine($"Invalid shape ID: {args[1]}");
            return;
        }

        if (!long.TryParse(args[2], out long shapeId2))
        {
            Console.Error.WriteLine($"Invalid shape ID: {args[2]}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Assume shapes are on the first page; adjust if needed
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                Page page = diagram.Pages[0];

                // Retrieve the two shapes by their IDs
                Shape shape1 = page.Shapes.GetShape(shapeId1);
                Shape shape2 = page.Shapes.GetShape(shapeId2);

                if (shape1 == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId1} not found.");
                    return;
                }

                if (shape2 == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId2} not found.");
                    return;
                }

                // Helper function to extract gradient direction value
                double GetGradientDirection(Shape shape)
                {
                    // Check if gradient fill is enabled; if not, treat direction as undefined (NaN)
                    if (shape.Fill?.GradientFill?.GradientEnabled?.Value == BOOL.True)
                    {
                        // GradientDir may be null; guard against it
                        return shape.Fill.GradientFill.GradientDir?.Value ?? double.NaN;
                    }
                    else
                    {
                        return double.NaN; // Not enabled
                    }
                }

                double dir1 = GetGradientDirection(shape1);
                double dir2 = GetGradientDirection(shape2);

                // Output the retrieved directions
                Console.WriteLine($"Shape {shapeId1} gradient direction: {(double.IsNaN(dir1) ? "Not enabled" : dir1.ToString())}");
                Console.WriteLine($"Shape {shapeId2} gradient direction: {(double.IsNaN(dir2) ? "Not enabled" : dir2.ToString())}");

                // Compare the directions, considering NaN (not enabled) as a possible difference
                bool areDifferent;
                if (double.IsNaN(dir1) && double.IsNaN(dir2))
                {
                    areDifferent = false; // Both not enabled
                }
                else if (double.IsNaN(dir1) || double.IsNaN(dir2))
                {
                    areDifferent = true; // One enabled, the other not
                }
                else
                {
                    // Use a tolerance for floating‑point comparison
                    areDifferent = Math.Abs(dir1 - dir2) > 0.0001;
                }

                // Report the analysis result
                if (areDifferent)
                {
                    Console.WriteLine("Difference detected: gradient directions are not the same.");
                }
                else
                {
                    Console.WriteLine("No difference: gradient directions are identical.");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}