using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeId>");
            return;
        }

        string diagramPath = args[0];
        string shapeIdArg = args[1];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Parse shape ID.
        if (!int.TryParse(shapeIdArg, out int shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'. It must be an integer.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The diagram contains no pages.");
                return;
            }

            // Use the active page (or first page) to locate the shape.
            Page page = diagram.ActivePage ?? diagram.Pages[0];

            // Retrieve the shape by its ID.
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Error: No shape found with ID {shapeId} on the active page.");
                return;
            }

            // Access the gradient fill information.
            // GradientFill may be null if the shape does not have a gradient; handle gracefully.
            if (shape.Fill?.GradientFill == null)
            {
                Console.WriteLine("The selected shape does not have a gradient fill.");
                return;
            }

            // Read the current gradient angle (in degrees).
            double gradientAngle = shape.Fill.GradientFill.GradientAngle.Value;

            // Output the angle to the console.
            Console.WriteLine($"Shape ID {shapeId} gradient angle: {gradientAngle} degrees");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}