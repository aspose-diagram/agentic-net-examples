using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string visioPath = args[0];

        // Guard: ensure the Visio file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found - {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Ensure the diagram contains at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The diagram does not contain any pages.");
                return;
            }

            // Get the first page
            Page page = diagram.Pages[0];

            // Ensure the page contains at least one shape
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("Error: The page does not contain any shapes.");
                return;
            }

            // Get the first shape on the page
            Shape shape = page.Shapes[0];

            // Access the gradient fill of the shape
            GradientFill gradientFill = shape.Fill.GradientFill;

            // Ensure there is at least one gradient stop
            if (gradientFill.GradientStops.Count == 0)
            {
                Console.Error.WriteLine("Error: The shape does not have any gradient stops.");
                return;
            }

            // Retrieve the gradient stop at index 0
            GradientStop firstStop = gradientFill.GradientStops[0];

            // Read the color value of the first gradient stop
            string colorValue = firstStop.Color.Value;

            // Output the color value for analysis
            Console.WriteLine($"Gradient Stop[0] Color: {colorValue}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Exception: {ex.Message}");
        }
    }
}