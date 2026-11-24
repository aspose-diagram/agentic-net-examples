using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <shapeId1> <shapeId2>");
            return;
        }

        string inputPath = args[0];
        string shapeId1Str = args[1];
        string shapeId2Str = args[2];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Parse shape IDs.
        if (!long.TryParse(shapeId1Str, out long shapeId1) || !long.TryParse(shapeId2Str, out long shapeId2))
        {
            Console.Error.WriteLine("Error: Shape IDs must be valid integers.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Error: Diagram contains no pages.");
                    return;
                }

                // Retrieve the first page (index 0).
                Page page = diagram.Pages[0];

                // Retrieve the first shape.
                Shape shape1 = page.Shapes.GetShape(shapeId1);
                if (shape1 == null)
                {
                    Console.Error.WriteLine($"Error: Shape with ID {shapeId1} not found on page 0.");
                    return;
                }

                // Retrieve the second shape.
                Shape shape2 = page.Shapes.GetShape(shapeId2);
                if (shape2 == null)
                {
                    Console.Error.WriteLine($"Error: Shape with ID {shapeId2} not found on page 0.");
                    return;
                }

                // Access gradient direction values.
                double dir1 = shape1.Fill?.GradientFill?.GradientDir?.Value ?? double.NaN;
                double dir2 = shape2.Fill?.GradientFill?.GradientDir?.Value ?? double.NaN;

                // Output the gradient directions.
                Console.WriteLine($"Shape {shapeId1} Gradient Direction: {dir1}");
                Console.WriteLine($"Shape {shapeId2} Gradient Direction: {dir2}");

                // Compare the directions and note any differences.
                if (double.IsNaN(dir1) && double.IsNaN(dir2))
                {
                    Console.WriteLine("Both shapes have no gradient direction defined.");
                }
                else if (double.IsNaN(dir1) || double.IsNaN(dir2))
                {
                    Console.WriteLine("One of the shapes does not have a gradient direction defined.");
                }
                else if (Math.Abs(dir1 - dir2) < 0.0001)
                {
                    Console.WriteLine("The gradient directions are identical.");
                }
                else
                {
                    Console.WriteLine("The gradient directions differ.");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any Aspose or runtime errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}