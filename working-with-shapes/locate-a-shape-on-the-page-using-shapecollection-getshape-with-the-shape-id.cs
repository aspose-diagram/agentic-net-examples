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
            Console.Error.WriteLine($"Error: File not found - {visioPath}");
            return;
        }

        // Parse the shape ID (expected to be a long integer).
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'. Must be a numeric value.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The diagram contains no pages.");
                return;
            }

            // Retrieve the first page (index 0).
            Page page = diagram.Pages[0];

            // Locate the shape by its ID using ShapeCollection.GetShape.
            Shape shape = page.Shapes.GetShape(shapeId);

            // Verify that the shape was found.
            if (shape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} was not found on page 0.");
                return;
            }

            // Output basic information about the located shape.
            Console.WriteLine($"Shape found:");
            Console.WriteLine($"  ID      : {shape.ID}");
            Console.WriteLine($"  NameU   : {shape.NameU}");
            Console.WriteLine($"  Master  : {(shape.Master != null ? shape.Master.Name : "None")}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}