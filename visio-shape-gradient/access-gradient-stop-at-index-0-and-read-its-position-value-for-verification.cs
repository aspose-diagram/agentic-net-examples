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
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Access the first page
            Page page = diagram.Pages[0];

            // Find the first shape that contains gradient stops
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Fill != null &&
                    shape.Fill.GradientFill != null &&
                    shape.Fill.GradientFill.GradientStops != null &&
                    shape.Fill.GradientFill.GradientStops.Count > 0)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("No shape with gradient stops was found on the first page.");
                return;
            }

            // Access the gradient stop at index 0
            GradientStop firstStop = targetShape.Fill.GradientFill.GradientStops[0];

            // Read its position value
            double position = firstStop.Position.Value;

            // Output the position for verification
            Console.WriteLine($"Gradient stop at index 0 has position value: {position}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}