using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;
using System;
using System.Collections.Generic;

class AutoSpaceValidator
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Work with the first page
            Page page = diagram.Pages[0];

            // Get all shapes on the page
            ShapeCollection shapes = page.Shapes;

            // Define auto‑spacing options (distance in inches)
            AutoSpaceOptions options = new AutoSpaceOptions
            {
                DistanceInHorizontal = 0.5f, // horizontal spacing
                DistanceInVertical = 0.5f    // vertical spacing
            };

            // Apply auto‑spacing to the shapes
            page.AutoSpaceShapes(shapes, options);

            // Refresh each shape so its position data is up‑to‑date
            foreach (Shape shape in shapes)
            {
                shape.RefreshData();
            }

            // Expected positions (in inches) for shapes identified by their IDs
            var expectedPositions = new Dictionary<long, (double X, double Y)>
            {
                { 1, (1.0, 1.0) },
                { 2, (1.5, 1.0) },
                { 3, (2.0, 1.0) }
                // Add additional expected values as required
            };

            // Tolerance for comparison (in inches)
            const double tolerance = 0.01;

            // Validate actual positions against expected values
            foreach (Shape shape in shapes)
            {
                if (expectedPositions.TryGetValue(shape.ID, out var expected))
                {
                    double actualX = shape.XForm.PinX.Value;
                    double actualY = shape.XForm.PinY.Value;

                    bool xOk = Math.Abs(actualX - expected.X) <= tolerance;
                    bool yOk = Math.Abs(actualY - expected.Y) <= tolerance;

                    Console.WriteLine(
                        $"Shape ID {shape.ID}: X {(xOk ? "OK" : "FAIL")} (actual {actualX:F3}, expected {expected.X:F3}), " +
                        $"Y {(yOk ? "OK" : "FAIL")} (actual {actualY:F3}, expected {expected.Y:F3})");
                }
            }

            // Save the diagram after auto‑spacing (optional)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
