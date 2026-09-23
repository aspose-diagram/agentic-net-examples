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

            // Ensure there is at least one page and one shape
            if (diagram.Pages.Count == 0)
            {
                Console.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            if (page.Shapes.Count == 0)
            {
                Console.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the page
            Shape shape = page.Shapes.GetShape(1); // shape IDs start at 1

            // Access the Fill property of the shape
            Fill fill = shape.Fill;

            // Obtain the associated GradientFill object
            GradientFill gradientFill = fill.GradientFill;

            // Example: output some gradient fill details
            Console.WriteLine($"Gradient Enabled: {gradientFill.GradientEnabled.Value}");
            Console.WriteLine($"Gradient Direction: {gradientFill.GradientDir.Value}");
            Console.WriteLine($"Number of Gradient Stops: {gradientFill.GradientStops.Count}");

            // Iterate through gradient stops (if any) and display their positions and colors
            foreach (GradientStop stop in gradientFill.GradientStops)
            {
                double position = stop.Position.Value;
                string color = stop.Color.Value;
                Console.WriteLine($"Stop Position: {position}, Color: {color}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
