using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: Diagram contains no pages.");
                return;
            }

            // Access the first page
            Page page = diagram.Pages[0];

            // Ensure the page contains at least one shape
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("Error: Page contains no shapes.");
                return;
            }

            // Access the first shape on the page
            Shape shape = page.Shapes[0];

            // Access the gradient fill of the shape
            GradientFill gradientFill = shape.Fill?.GradientFill;
            if (gradientFill == null)
            {
                Console.Error.WriteLine("Error: Shape does not have a gradient fill.");
                return;
            }

            // Access the collection of gradient stops
            GradientStopCollection stops = gradientFill.GradientStops;
            if (stops == null || stops.Count == 0)
            {
                Console.Error.WriteLine("Error: No gradient stops found in the shape.");
                return;
            }

            // Retrieve the gradient stop at index 0
            GradientStop firstStop = stops[0];

            // Read the position value of the first gradient stop
            double position = firstStop.Position.Value;

            // Output the position for verification
            Console.WriteLine($"Gradient stop at index 0 has position value: {position}");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error console
            Console.Error.WriteLine($"Exception occurred: {ex.Message}");
        }
    }
}