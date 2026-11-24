using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least one argument: the path to the Visio diagram file.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Dictionary to hold expected X and Y coordinates for shapes.
        // Key: shape ID (long), Value: Tuple of expected PinX and PinY.
        // Populate this dictionary with the expected positions after auto‑spacing.
        var expectedPositions = new Dictionary<long, (double ExpectedPinX, double ExpectedPinY)>
        {
            // Example entries (replace with real expected data):
            // { 1, (2.0, 3.0) },
            // { 2, (4.0, 5.0) }
        };

        try
        {
            // Load the diagram inside a using block (required pattern).
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Error: Diagram contains no pages.");
                    return;
                }

                // Work with the first page (adjust if needed).
                Page page = diagram.Pages[0];

                // Configure auto‑space options.
                AutoSpaceOptions options = new AutoSpaceOptions
                {
                    // Set horizontal and vertical distances (in inches).
                    DistanceInHorizontal = 1.0,
                    DistanceInVertical = 1.0
                };

                // Perform auto‑spacing on all shapes of the page.
                page.AutoSpaceShapes(page.Shapes, options);

                // Validate each shape's position against expected values.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    long shapeId = shape.ID;

                    // If there is no expected data for this shape, skip validation.
                    if (!expectedPositions.TryGetValue(shapeId, out var expected))
                        continue;

                    double actualPinX = shape.XForm.PinX.Value;
                    double actualPinY = shape.XForm.PinY.Value;

                    // Compare X coordinate.
                    if (Math.Abs(actualPinX - expected.ExpectedPinX) > 0.001)
                    {
                        string msg = $"Shape ID {shapeId}: PinX mismatch. Expected {expected.ExpectedPinX}, Actual {actualPinX}";
                        Console.Error.WriteLine(msg);
                        throw new Exception(msg);
                    }

                    // Compare Y coordinate.
                    if (Math.Abs(actualPinY - expected.ExpectedPinY) > 0.001)
                    {
                        string msg = $"Shape ID {shapeId}: PinY mismatch. Expected {expected.ExpectedPinY}, Actual {actualPinY}";
                        Console.Error.WriteLine(msg);
                        throw new Exception(msg);
                    }
                }

                Console.WriteLine("All shape positions match the expected values after auto‑spacing.");
            }
        }
        catch (Exception ex)
        {
            // Log any Aspose.Diagram or runtime errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}