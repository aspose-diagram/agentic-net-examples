using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments: expect diagram file path and shape ID
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeId>");
            return;
        }

        string diagramPath = args[0];
        string shapeIdArg = args[1];

        // Guard: check if the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Parse shape ID
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'. Must be a numeric value.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Retrieve the active page (or you could select a specific page if needed)
            Page page = diagram.ActivePage;
            if (page == null)
            {
                Console.Error.WriteLine("Error: No active page found in the diagram.");
                return;
            }

            // Get the shape by ID
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeId} not found on the active page.");
                return;
            }

            // Ensure the shape has a gradient fill defined
            if (shape.Fill == null || shape.Fill.GradientFill == null)
            {
                Console.Error.WriteLine("Error: The selected shape does not have a gradient fill.");
                return;
            }

            // Read the current gradient direction value
            double gradientDirection = shape.Fill.GradientFill.GradientDir.Value;

            // Output the gradient direction for verification
            Console.WriteLine($"Shape ID {shapeId} Gradient Direction: {gradientDirection}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Exception occurred: {ex.Message}");
        }
    }
}