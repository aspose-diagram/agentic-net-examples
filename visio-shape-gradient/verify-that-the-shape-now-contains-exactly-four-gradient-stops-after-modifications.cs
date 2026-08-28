using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("sample.vsdx");

            // Access the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Retrieve the target shape by its ID (replace 1 with the actual shape ID)
            Shape shape = page.Shapes.GetShape(1);
            if (shape == null)
                throw new Exception("Target shape not found.");

            // Get the collection of gradient stops for the shape
            var gradientStops = shape.Fill.GradientFill.GradientStops;

            // Count the gradient stops
            int stopCount = gradientStops.Count;

            // Verify that there are exactly four gradient stops
            if (stopCount != 4)
                throw new Exception($"Expected 4 gradient stops, but found {stopCount}.");
            else
                Console.WriteLine("Shape contains exactly four gradient stops.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
