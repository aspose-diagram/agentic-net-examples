using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Input diagram path (first argument or default)
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Get the active page (the page where auto-spacing will be applied)
                Page page = diagram.ActivePage;

                // Prepare auto-space options (use correct property names)
                AutoSpaceOptions options = new AutoSpaceOptions();
                // Set horizontal and vertical distances (in inches)
                options.DistanceInHorizontal = 0.5; // 0.5 inches between shapes horizontally
                options.DistanceInVertical = 0.5;   // 0.5 inches between shapes vertically

                // Apply auto-spacing to all shapes on the page
                page.AutoSpaceShapes(page.Shapes, options);

                // Define expected coordinates for specific shape IDs
                // Populate this dictionary with the actual expected values for your diagram
                Dictionary<long, (double ExpectedPinX, double ExpectedPinY)> expectedPositions = new Dictionary<long, (double, double)>
                {
                    // { shapeId, (expectedPinX, expectedPinY) }
                    { 1, (2.0, 2.0) },
                    { 2, (3.0, 2.0) },
                    { 3, (2.0, 3.0) }
                };

                // Verify each expected shape's position
                foreach (var kvp in expectedPositions)
                {
                    long shapeId = kvp.Key;
                    double expectedPinX = kvp.Value.ExpectedPinX;
                    double expectedPinY = kvp.Value.ExpectedPinY;

                    // Retrieve the shape by its ID
                    Shape shape = page.Shapes.GetShape(shapeId);
                    if (shape == null)
                    {
                        throw new Exception($"Shape with ID {shapeId} not found.");
                    }

                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                    {
                        continue;
                    }

                    double actualPinX = shape.XForm.PinX.Value;
                    double actualPinY = shape.XForm.PinY.Value;

                    // Simple tolerance for floating point comparison
                    const double tolerance = 0.001;

                    if (Math.Abs(actualPinX - expectedPinX) > tolerance ||
                        Math.Abs(actualPinY - expectedPinY) > tolerance)
                    {
                        throw new Exception($"Shape ID {shapeId} position mismatch. Expected (PinX,PinY)=({expectedPinX},{expectedPinY}) but got ({actualPinX},{actualPinY}).");
                    }
                }

                // If all checks pass
                Console.WriteLine("All shape positions match the expected coordinates after auto-spacing.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}