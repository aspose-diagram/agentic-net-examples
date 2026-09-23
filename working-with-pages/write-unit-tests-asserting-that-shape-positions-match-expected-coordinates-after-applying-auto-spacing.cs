using System;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram (default page is added automatically)
            Diagram diagram = new Diagram();

            // Get the first (and only) page
            Page page = diagram.Pages[0];

            // Add three rectangle shapes using DrawRectangle (pinX, pinY, width, height)
            // All shapes have the same size for simplicity
            long shapeId1 = page.DrawRectangle(1.0, 1.0, 1.0, 1.0);
            long shapeId2 = page.DrawRectangle(1.5, 1.0, 1.0, 1.0);
            long shapeId3 = page.DrawRectangle(2.0, 1.0, 1.0, 1.0);

            // Retrieve shape objects for later inspection
            Shape shape1 = page.Shapes.GetShape(shapeId1);
            Shape shape2 = page.Shapes.GetShape(shapeId2);
            Shape shape3 = page.Shapes.GetShape(shapeId3);

            // Configure auto‑spacing options (2 inches horizontal and vertical gaps)
            AutoSpaceOptions options = new AutoSpaceOptions();
            options.DistanceInHorizontal = 2.0;
            options.DistanceInVertical = 2.0;

            // Apply auto‑spacing to all shapes on the page
            page.AutoSpaceShapes(page.Shapes, options);

            // After spacing, verify that each pair of shapes respects the minimum horizontal distance
            Shape[] shapes = new Shape[] { shape1, shape2, shape3 };
            double minHorizontal = options.DistanceInHorizontal;

            for (int i = 0; i < shapes.Length; i++)
            {
                for (int j = i + 1; j < shapes.Length; j++)
                {
                    Shape a = shapes[i];
                    Shape b = shapes[j];

                    // Horizontal distance between shape centers
                    double horizDist = Math.Abs(a.XForm.PinX.Value - b.XForm.PinX.Value);
                    // Minimum allowed center‑to‑center distance = half widths + required gap
                    double horizMinAllowed = (a.XForm.Width.Value + b.XForm.Width.Value) / 2.0 + minHorizontal;

                    // Throw if the horizontal spacing requirement is not met
                    if (horizDist < horizMinAllowed)
                    {
                        throw new Exception($"Horizontal spacing violation between shape {a.ID} and shape {b.ID}. " +
                                            $"Actual: {horizDist}, Required minimum: {horizMinAllowed}");
                    }
                }
            }

            Console.WriteLine("All shape positions satisfy the expected auto‑spacing distances.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}