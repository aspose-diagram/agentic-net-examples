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
            Console.Error.WriteLine("Usage: Program <diagramFilePath> <shapeId>");
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

        // Parse the shape ID (expected to be a long integer).
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'. Must be a numeric value.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Attempt to retrieve the shape from the first page.
            // Adjust if your diagram uses multiple pages.
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes.GetShape(shapeId);

            if (shape == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeId} not found on page 0.");
                return;
            }

            // Retrieve IDs of all shapes connected to the specified shape.
            // Using ConnectedShapesAllNodes flag to get all directly connected nodes.
            long[] connectedShapeIds = shape.ConnectedShapes(ConnectedShapesFlags.ConnectedShapesAllNodes, null);

            // Output the results.
            Console.WriteLine($"Shape ID {shapeId} is connected to {connectedShapeIds.Length} shape(s):");
            foreach (long id in connectedShapeIds)
            {
                Console.WriteLine($"- Connected Shape ID: {id}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}