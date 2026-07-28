using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page and the first shape (adjust indices as needed)
            Shape shape = diagram.Pages[0].Shapes[0];

            // Retrieve the gradient fill of the shape
            GradientFill gradientFill = shape.Fill.GradientFill;

            // Access the gradient stop at index 0
            GradientStop firstStop = gradientFill.GradientStops[0];

            // Read the position value of the gradient stop
            double position = firstStop.Position.Value;

            // Example verification against an expected value
            double expected = 0.0; // replace with the expected position
            if (Math.Abs(position - expected) < 0.0001)
            {
                Console.WriteLine("Gradient stop position matches the expected value.");
            }
            else
            {
                Console.WriteLine($"Gradient stop position {position} does not match expected {expected}.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
