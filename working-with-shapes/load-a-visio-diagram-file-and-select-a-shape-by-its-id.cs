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
            Console.Error.WriteLine("Usage: Program <VisioFilePath> <ShapeId>");
            return;
        }

        string visioPath = args[0];
        string shapeIdArg = args[1];

        // Guard: ensure the Visio file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found – '{visioPath}'.");
            return;
        }

        // Guard: parse the shape ID (must be a long integer).
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID – '{shapeIdArg}'.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Attempt to locate the shape by ID across all pages.
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                try
                {
                    // GetShape returns the shape with the specified ID on this page.
                    Shape shape = page.Shapes.GetShape(shapeId);
                    if (shape != null)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                catch
                {
                    // If the shape is not on this page, GetShape may throw; ignore and continue.
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} was not found in the diagram.");
                return;
            }

            // Output some basic information about the found shape.
            Console.WriteLine($"Shape ID   : {targetShape.ID}");
            Console.WriteLine($"NameU      : {targetShape.NameU}");
            Console.WriteLine($"Name       : {targetShape.Name}");
            Console.WriteLine($"Master Name: {targetShape.Master?.NameU ?? "N/A"}");
            Console.WriteLine($"Deleted    : {(targetShape.Del == BOOL.True ? "Yes" : "No")}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}