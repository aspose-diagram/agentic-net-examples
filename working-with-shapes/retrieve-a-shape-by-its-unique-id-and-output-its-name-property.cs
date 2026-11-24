using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expect diagram file path and shape ID.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeId>");
            return;
        }

        string diagramPath = args[0];
        string shapeIdArg = args[1];

        // Guard: check if the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Parse the shape ID (expected to be a numeric value).
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'. Must be a numeric value.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages and shapes to find the shape with the specified ID.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Compare the shape's unique ID.
                    if (shape.ID == shapeId)
                    {
                        // Output the shape's Name property.
                        Console.WriteLine($"Shape ID: {shapeId}");
                        Console.WriteLine($"Shape Name: {shape.Name}");
                        return;
                    }
                }
            }

            // If we reach here, the shape was not found.
            Console.Error.WriteLine($"Shape with ID {shapeId} was not found in the diagram.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}