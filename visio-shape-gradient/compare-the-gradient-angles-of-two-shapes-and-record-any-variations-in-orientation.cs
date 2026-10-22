using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments count
        if (args.Length < 5)
        {
            Console.Error.WriteLine("Usage: <inputVisioFile> <pageIndex1> <shapeId1> <pageIndex2> <shapeId2>");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse page indices and shape IDs
        if (!int.TryParse(args[1], out int pageIndex1) ||
            !long.TryParse(args[2], out long shapeId1) ||
            !int.TryParse(args[3], out int pageIndex2) ||
            !long.TryParse(args[4], out long shapeId2))
        {
            Console.Error.WriteLine("Invalid page index or shape ID arguments.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve the first shape
                if (pageIndex1 < 0 || pageIndex1 >= diagram.Pages.Count)
                {
                    Console.Error.WriteLine($"Page index {pageIndex1} is out of range.");
                    return;
                }
                Page page1 = diagram.Pages[pageIndex1];
                Shape shape1 = page1.Shapes.GetShape(shapeId1);

                // Retrieve the second shape
                if (pageIndex2 < 0 || pageIndex2 >= diagram.Pages.Count)
                {
                    Console.Error.WriteLine($"Page index {pageIndex2} is out of range.");
                    return;
                }
                Page page2 = diagram.Pages[pageIndex2];
                Shape shape2 = page2.Shapes.GetShape(shapeId2);

                // Access gradient angle values
                double angle1 = shape1.Fill.GradientFill.GradientAngle.Value;
                double angle2 = shape2.Fill.GradientFill.GradientAngle.Value;

                // Compare angles with a tolerance to account for floating‑point precision
                const double tolerance = 0.0001;
                if (Math.Abs(angle1 - angle2) > tolerance)
                {
                    Console.WriteLine($"Gradient angles differ:");
                    Console.WriteLine($"  Shape 1 (ID {shapeId1}) angle: {angle1} degrees");
                    Console.WriteLine($"  Shape 2 (ID {shapeId2}) angle: {angle2} degrees");
                }
                else
                {
                    Console.WriteLine($"Gradient angles are the same: {angle1} degrees");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any Aspose or runtime errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}