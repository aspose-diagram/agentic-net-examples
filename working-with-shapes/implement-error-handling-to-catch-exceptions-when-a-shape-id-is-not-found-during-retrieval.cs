using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Parse the shape ID (must be a long integer).
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'. Must be a numeric value.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern).
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Verify that the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Error: The diagram contains no pages.");
                    return;
                }

                // Use the first page for shape retrieval.
                Page page = diagram.Pages[0];

                // Attempt to retrieve the shape by its ID.
                Shape shape;
                try
                {
                    shape = page.Shapes.GetShape(shapeId);
                }
                catch (Exception ex)
                {
                    // Shape ID not found – report the error and exit.
                    Console.Error.WriteLine($"Error: Shape with ID {shapeId} was not found. Details: {ex.Message}");
                    return;
                }

                // Shape retrieved successfully – output basic information.
                Console.WriteLine($"Shape with ID {shapeId} retrieved successfully.");
                string shapeName = shape.NameU ?? "(Unnamed)";
                Console.WriteLine($"Shape NameU: {shapeName}");

                // (Optional) Save the diagram unchanged to demonstrate a successful operation.
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(diagramPath) ?? string.Empty,
                    "output.vsdx");

                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // General exception handling for any Aspose.Diagram operation failures.
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}