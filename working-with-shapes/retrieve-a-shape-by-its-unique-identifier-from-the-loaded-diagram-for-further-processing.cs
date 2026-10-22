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

        // Parse the shape identifier (expected to be a long integer).
        if (!long.TryParse(args[1], out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {args[1]}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Assume the shape resides on the first page; adjust as needed.
            Page page = diagram.Pages[0];

            // Retrieve the shape by its unique identifier.
            Shape shape = page.Shapes.GetShape(shapeId);

            if (shape == null)
            {
                Console.WriteLine($"Shape with ID {shapeId} was not found on page 0.");
                return;
            }

            // Output basic shape information for verification.
            Console.WriteLine($"Shape ID      : {shape.ID}");
            Console.WriteLine($"Shape NameU   : {shape.NameU}");
            Console.WriteLine($"Is Deleted    : {(shape.Del == BOOL.True ? "Yes" : "No")}");

            // If the shape is based on a master, display the master name.
            if (shape.Master != null)
            {
                Console.WriteLine($"Master NameU  : {shape.Master.NameU}");
            }
            else
            {
                Console.WriteLine("Shape has no associated master.");
            }

            // TODO: Add further processing of the retrieved shape here.
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}