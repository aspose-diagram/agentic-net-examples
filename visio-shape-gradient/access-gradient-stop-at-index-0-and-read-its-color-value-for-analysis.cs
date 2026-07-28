using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file (replace with actual file path)
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page and one shape
            if (diagram.Pages.Count == 0)
            {
                Console.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page
            Page page = diagram.Pages[0];

            if (page.Shapes.Count == 0)
            {
                Console.WriteLine("The page contains no shapes.");
                return;
            }

            // Get the first shape (replace index as needed)
            Shape shape = page.Shapes[0];

            // Access the gradient fill of the shape
            GradientFill gradientFill = shape.Fill.GradientFill;

            // Ensure gradient stops exist
            if (gradientFill.GradientStops.Count == 0)
            {
                Console.WriteLine("The shape has no gradient stops.");
                return;
            }

            // Access the gradient stop at index 0
            GradientStop firstStop = gradientFill.GradientStops[0];

            // Read its color value (hex string)
            string colorHex = firstStop.Color.Value;

            // Output the color value for analysis
            Console.WriteLine($"Gradient stop 0 color: {colorHex}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
