using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file path and shape ID
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeId>");
            return;
        }

        string diagramPath = args[0];
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        if (!long.TryParse(args[1], out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {args[1]}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Retrieve the active page (or you can select a specific page if needed)
            Page page = diagram.ActivePage;

            // Get the shape by its ID
            Shape shape = page.Shapes.GetShape((int)shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found.");
                return;
            }

            // -----------------------------------------------------------------
            // Apply gradient fill modifications
            // -----------------------------------------------------------------
            // Set fill pattern to gradient (value 25)
            shape.Fill.FillPattern.Value = 25;

            // Enable gradient fill
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set gradient direction (0 = left‑to‑right)
            shape.Fill.GradientFill.GradientDir.Value = 0;

            // Clear any existing gradient stops
            shape.Fill.GradientFill.GradientStops.Clear();

            // Add four gradient stops
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#FF0000", MeasureConst.Undefined)); // Red at start

            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0.33, MeasureConst.NUM),
                new ColorValue("#00FF00", MeasureConst.Undefined)); // Green

            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0.66, MeasureConst.NUM),
                new ColorValue("#0000FF", MeasureConst.Undefined)); // Blue

            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#FFFF00", MeasureConst.Undefined)); // Yellow at end

            // -----------------------------------------------------------------
            // Verify that the shape now contains exactly four gradient stops
            // -----------------------------------------------------------------
            int stopCount = shape.Fill.GradientFill.GradientStops.Count;
            if (stopCount == 4)
            {
                Console.WriteLine("Verification succeeded: Shape contains exactly four gradient stops.");
            }
            else
            {
                throw new Exception($"Verification failed: Expected 4 gradient stops, but found {stopCount}.");
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}