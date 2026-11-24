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
        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Parse the shape ID (expected to be a long integer).
        if (!long.TryParse(args[1], out long shapeId))
        {
            Console.Error.WriteLine("Invalid shape ID. It must be a numeric value.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Use the active page (first page) for shape lookup.
            Page page = diagram.ActivePage;

            // Retrieve the sub‑shape by its ID.
            Shape subShape = page.Shapes.GetShape(shapeId);
            if (subShape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found on the active page.");
                return;
            }

            // Obtain the parent shape of the sub‑shape.
            Shape parentShape = subShape.ParentShape;

            // Display parent information or indicate absence of a parent.
            if (parentShape == null)
            {
                Console.WriteLine($"Shape ID {shapeId} has no parent shape.");
            }
            else
            {
                Console.WriteLine($"Parent Shape ID: {parentShape.ID}");
                // Prefer the universal name (NameU) if available; otherwise fall back to Name.
                string parentName = !string.IsNullOrEmpty(parentShape.NameU) ? parentShape.NameU : parentShape.Name;
                Console.WriteLine($"Parent Shape Name: {parentName}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}