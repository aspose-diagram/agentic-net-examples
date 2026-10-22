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
        // Guard: ensure the Visio file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found – {visioPath}");
            return;
        }

        // Parse the shape ID (expected to be a long integer).
        if (!long.TryParse(args[1], out long shapeId))
        {
            Console.Error.WriteLine("Error: ShapeId must be a valid integer.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Error: The diagram contains no pages.");
                    return;
                }

                // Access the first page (index 0). Adjust if a different page is needed.
                Page page = diagram.Pages[0];

                // Retrieve the shape by its ID using the correct API.
                Shape shape = page.Shapes.GetShape(shapeId);

                // Verify that the shape is not logically deleted.
                if (shape.Del == BOOL.True)
                {
                    Console.Error.WriteLine($"Warning: Shape with ID {shapeId} is marked as deleted.");
                }

                // Output basic shape information.
                Console.WriteLine($"Shape ID   : {shape.ID}");
                Console.WriteLine($"NameU      : {shape.NameU}");
                Console.WriteLine($"Master Name: {shape.Master?.Name ?? "(none)"}");

                // Retrieve and display the shape comment if present.
                string comment = shape.Misc?.Comment?.Value ?? string.Empty;
                if (!string.IsNullOrEmpty(comment))
                {
                    Console.WriteLine($"Comment    : {comment}");
                }

                // If the shape is a group, list its child shapes.
                if (shape.Type == TypeValue.Group && shape.Shapes != null)
                {
                    Console.WriteLine($"Group contains {shape.Shapes.Count} child shape(s):");
                    foreach (Shape child in shape.Shapes)
                    {
                        Console.WriteLine($"  Child ID: {child.ID}, NameU: {child.NameU}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}