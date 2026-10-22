using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> [ShapeId]");
            return;
        }

        string visioPath = args[0];
        // Guard: ensure the Visio file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found - {visioPath}");
            return;
        }

        // Optional shape ID argument (default to 1)
        int shapeId = 1;
        if (args.Length >= 2 && int.TryParse(args[1], out int parsedId))
        {
            shapeId = parsedId;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Ensure the diagram contains at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The diagram contains no pages.");
                return;
            }

            // Retrieve the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeId} not found on page.");
                return;
            }

            // Access the gradient fill of the shape
            GradientFill gradientFill = shape.Fill.GradientFill;

            // Verify that gradient stops are present
            if (gradientFill.GradientStops.Count == 0)
            {
                Console.WriteLine("The shape does not contain any gradient stops.");
                return;
            }

            // Access the gradient stop at index 0
            GradientStop firstStop = gradientFill.GradientStops[0];

            // Read the color value of the first gradient stop
            string colorValue = firstStop.Color.Value;

            // Output the color value for analysis
            Console.WriteLine($"Gradient Stop 0 Color: {colorValue}");
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}