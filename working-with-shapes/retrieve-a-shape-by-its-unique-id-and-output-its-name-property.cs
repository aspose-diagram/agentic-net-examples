using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: expect diagram file path and shape ID.
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

        // Parse the shape ID (expected to be a numeric value).
        if (!long.TryParse(shapeIdArg, out long shapeIdLong))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'. Must be a numeric value.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The diagram contains no pages.");
                return;
            }

            // Retrieve the first page (index 0). Adjust if a different page is required.
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID. GetShape expects an int, so cast safely.
            Shape shape = page.Shapes.GetShape((int)shapeIdLong);

            // Verify that the shape was found.
            if (shape == null)
            {
                Console.Error.WriteLine($"Error: No shape with ID {shapeIdLong} was found on page 0.");
                return;
            }

            // Output the shape's Name property.
            Console.WriteLine($"Shape ID: {shapeIdLong}");
            Console.WriteLine($"Shape Name: {shape.Name}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}