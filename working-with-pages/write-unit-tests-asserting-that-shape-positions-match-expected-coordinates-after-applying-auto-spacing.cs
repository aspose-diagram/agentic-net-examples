using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <visioFilePath>");
            return;
        }

        // Path to the Visio file to be processed.
        string visioPath = args[0];
        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(visioPath);

            // Retrieve the first page (index 0) where shapes reside.
            Page page = diagram.Pages[0];

            // Configure auto-spacing options: 2 inches horizontal and vertical gaps.
            AutoSpaceOptions options = new AutoSpaceOptions
            {
                DistanceInHorizontal = 2.0,
                DistanceInVertical = 2.0
            };

            // Apply auto-spacing to all shapes on the page.
            page.AutoSpaceShapes(page.Shapes, options);

            // Expected coordinates after auto-spacing (shape ID -> (PinX, PinY)).
            var expectedPositions = new System.Collections.Generic.Dictionary<long, (double PinX, double PinY)>
            {
                // Adjust these expected values to match your diagram's layout.
                { 1, (1.0, 1.0) },
                { 2, (3.0, 1.0) },
                { 3, (5.0, 1.0) }
            };

            // Tolerance for floating‑point comparison (in inches).
            const double tolerance = 0.001;

            // Iterate over each expected shape and verify its position.
            foreach (var kvp in expectedPositions)
            {
                long shapeId = kvp.Key;
                double expPinX = kvp.Value.PinX;
                double expPinY = kvp.Value.PinY;

                // Retrieve the shape by its ID; cast to int if required.
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    throw new Exception($"Shape with ID {shapeId} not found.");
                }

                // Actual coordinates from the shape's XForm.
                double actualPinX = shape.XForm.PinX.Value;
                double actualPinY = shape.XForm.PinY.Value;

                // Compare X coordinate.
                if (Math.Abs(actualPinX - expPinX) > tolerance)
                {
                    throw new Exception($"Shape ID {shapeId} PinX mismatch. Expected: {expPinX}, Actual: {actualPinX}");
                }

                // Compare Y coordinate.
                if (Math.Abs(actualPinY - expPinY) > tolerance)
                {
                    throw new Exception($"Shape ID {shapeId} PinY mismatch. Expected: {expPinY}, Actual: {actualPinY}");
                }

                // Log successful verification for this shape.
                Console.WriteLine($"Shape ID {shapeId} position verified: ({actualPinX}, {actualPinY})");
            }

            // All checks passed.
            Console.WriteLine("All shape positions match expected coordinates after auto-spacing.");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}