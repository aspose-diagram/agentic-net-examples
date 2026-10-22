using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expect diagram file path and shape ID.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeId>");
            return;
        }

        string diagramPath = args[0];
        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Parse shape ID.
        if (!long.TryParse(args[1], out long shapeId) || shapeId <= 0)
        {
            Console.Error.WriteLine("Error: Invalid shape ID. It must be a positive integer.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Get the active page (or you could select a specific page if needed).
            Page page = diagram.ActivePage;

            // Retrieve the shape by its ID.
            Shape shape = page.Shapes.GetShape((int)shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeId} not found on the active page.");
                return;
            }

            // Verify that gradient fill is enabled.
            if (shape.Fill?.GradientFill?.GradientEnabled?.Value != BOOL.True)
            {
                Console.WriteLine("Gradient fill is not enabled for the selected shape.");
                return;
            }

            // Read the current gradient direction.
            // GradientDir.Value typically holds a numeric value representing the direction.
            double gradientDirection = shape.Fill.GradientFill.GradientDir?.Value ?? 0.0;

            // Output the gradient direction for verification.
            Console.WriteLine($"Shape ID: {shapeId}");
            Console.WriteLine($"Current Gradient Direction: {gradientDirection}");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}