using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    // Predefined constant background color to compare against (hex string, case-insensitive)
    private const string TargetBackgroundColor = "#FF0000";

    static void Main(string[] args)
    {
        // Validate command‑line arguments: expect diagram file path and shape ID
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeId>");
            return;
        }

        string diagramPath = args[0];
        string shapeIdArg = args[1];

        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Parse shape ID
        if (!int.TryParse(shapeIdArg, out int shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'. Must be an integer.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Get the first page (index 0). Adjust if a specific page is required.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: Diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeId} not found on page '{page.Name}'.");
                return;
            }

            // Read the fill background color (hex string)
            string backgroundColor = shape.Fill.FillBkgnd.Value ?? string.Empty;

            // Compare with the predefined constant (case‑insensitive)
            if (string.Equals(backgroundColor, TargetBackgroundColor, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Shape ID {shapeId} background color matches the target: {TargetBackgroundColor}");
            }
            else
            {
                Console.WriteLine($"Shape ID {shapeId} background color '{backgroundColor}' does NOT match the target '{TargetBackgroundColor}'.");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or runtime exceptions and report them
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}