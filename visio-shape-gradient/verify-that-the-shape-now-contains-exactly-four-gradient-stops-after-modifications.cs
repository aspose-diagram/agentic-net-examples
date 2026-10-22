using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeId>");
            return;
        }

        string diagramPath = args[0];
        string shapeIdArg = args[1];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Parse the shape identifier.
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Retrieve the shape from the active page.
            Shape shape = diagram.ActivePage.Shapes.GetShape((int)shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeId} not found.");
                return;
            }

            // -----------------------------------------------------------------
            // Configure gradient fill on the shape.
            // -----------------------------------------------------------------
            // Set fill pattern to gradient (value 25 as per API).
            shape.Fill.FillPattern.Value = 25;

            // Enable gradient fill.
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Optionally set gradient direction (0 = left‑to‑right).
            shape.Fill.GradientFill.GradientDir.Value = 0;

            // Clear any existing gradient stops.
            shape.Fill.GradientFill.GradientStops.Clear();

            // Add four gradient stops.
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0.0, MeasureConst.NUM),
                new ColorValue("#FF0000", MeasureConst.Undefined)); // Red at start

            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0.33, MeasureConst.NUM),
                new ColorValue("#00FF00", MeasureConst.Undefined)); // Green

            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0.66, MeasureConst.NUM),
                new ColorValue("#0000FF", MeasureConst.Undefined)); // Blue

            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1.0, MeasureConst.NUM),
                new ColorValue("#FFFF00", MeasureConst.Undefined)); // Yellow at end

            // -----------------------------------------------------------------
            // Verify that exactly four gradient stops are present.
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

            // (Optional) Save the modified diagram to a new file for inspection.
            // string outputPath = Path.Combine(Path.GetDirectoryName(diagramPath) ?? "", "Modified_" + Path.GetFileName(diagramPath));
            // diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}