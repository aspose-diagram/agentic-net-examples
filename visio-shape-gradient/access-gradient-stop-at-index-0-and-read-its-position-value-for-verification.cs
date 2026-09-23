using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                var diagram = new Diagram("input.vsdx");

                // Access the first page (index 0)
                var page = diagram.Pages[0];

                // Retrieve a shape from the page.
                // Here we assume the shape with ID 1 exists; adjust as needed.
                var shape = page.Shapes.GetShape(1);

                // Ensure the shape has a gradient fill defined
                if (shape.Fill.GradientFill == null)
                {
                    Console.WriteLine("The selected shape does not contain a gradient fill.");
                    return;
                }

                var gradientStops = shape.Fill.GradientFill.GradientStops;

                // Verify that at least one gradient stop is present
                if (gradientStops.Count == 0)
                {
                    Console.WriteLine("No gradient stops are defined for this shape.");
                    return;
                }

                // Access the gradient stop at index 0
                GradientStop firstStop = gradientStops[0];

                // Read its position value (stored as DoubleValue)
                double position = firstStop.Position.Value;

                Console.WriteLine($"First gradient stop position: {position}");

                // Example verification: expect the position to be 0.0 (start of gradient)
                double expectedPosition = 0.0;
                if (Math.Abs(position - expectedPosition) > 0.0001)
                {
                    throw new Exception($"Verification failed: expected position {expectedPosition}, but found {position}.");
                }
                else
                {
                    Console.WriteLine("Gradient stop position verification succeeded.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }