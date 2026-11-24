using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: expect diagram path and shape ID.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeId>");
            return;
        }

        string diagramPath = args[0];
        string shapeIdText = args[1];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Parse the shape identifier.
        if (!long.TryParse(shapeIdText, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdText}'. Must be a numeric value.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Retrieve the first page (index 0). Adjust if needed for multi‑page diagrams.
            Page page = diagram.Pages[0];

            // Retrieve the shape by its unique identifier.
            Shape shape = page.Shapes.GetShape(shapeId);

            if (shape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} was not found on page 0.");
                return;
            }

            // Output basic shape information.
            Console.WriteLine($"Shape ID   : {shape.ID}");
            Console.WriteLine($"Shape Name : {shape.NameU}");
            Console.WriteLine($"Master Name: {(shape.Master != null ? shape.Master.Name : "None")}");
            Console.WriteLine($"Deleted    : {(shape.Del == BOOL.True ? "Yes" : "No")}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram exceptions and report them.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}