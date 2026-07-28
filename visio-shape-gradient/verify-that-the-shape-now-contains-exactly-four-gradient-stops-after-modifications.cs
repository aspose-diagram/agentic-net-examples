using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Retrieve a shape to inspect.
            // Here we assume the shape with ID 1 exists; adjust as needed.
            Shape shape = page.Shapes.GetShape(1);
            if (shape == null)
                throw new Exception("Target shape not found.");

            // Access the gradient stops collection of the shape's fill.
            var gradientStops = shape.Fill.GradientFill.GradientStops;

            // Count the number of gradient stops.
            int stopCount = 0;
            foreach (GradientStop stop in gradientStops)
            {
                stopCount++;
            }

            // Verify that there are exactly four gradient stops.
            if (stopCount == 4)
            {
                Console.WriteLine("Shape contains exactly four gradient stops.");
            }
            else
            {
                throw new Exception($"Gradient stop count mismatch: expected 4, found {stopCount}.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
