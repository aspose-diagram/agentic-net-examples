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

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve a shape by its ID (example uses ID = 1)
            Shape shape = page.Shapes.GetShape(1);

            // Get the Fill property of the shape
            Fill fill = shape.Fill;

            // Obtain the GradientFill object associated with the shape's fill
            GradientFill gradient = fill.GradientFill;

            // Example: check whether the gradient is enabled
            if (gradient.GradientEnabled.Value == BOOL.True)
            {
                Console.WriteLine("Gradient fill is enabled for this shape.");
            }
            else
            {
                Console.WriteLine("Gradient fill is not enabled for this shape.");
            }

            // Iterate through gradient stops and display their positions and colors
            foreach (GradientStop stop in gradient.GradientStops)
            {
                double position = stop.Position.Value; // position (0 to 1)
                string colorHex = stop.Color.Value;   // color as hex string, e.g., "#FF0000"
                Console.WriteLine($"Stop at {position} with color {colorHex}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
