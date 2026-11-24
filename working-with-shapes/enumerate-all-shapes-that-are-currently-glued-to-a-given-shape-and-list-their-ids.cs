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
        string shapeIdArg = args[1];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Parse the shape ID argument.
        if (!long.TryParse(shapeIdArg, out long targetShapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Locate the shape with the specified ID across all pages.
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                // GetShape returns null if the ID is not present on this page.
                Shape shape = page.Shapes.GetShape(targetShapeId);
                if (shape != null)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {targetShapeId} not found in the diagram.");
                return;
            }

            // Retrieve IDs of all 1‑D connector shapes glued to the target shape.
            long[] gluedIds = targetShape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);

            // Output the results.
            Console.WriteLine($"Shape ID {targetShapeId} has {gluedIds?.Length ?? 0} glued shape(s):");
            if (gluedIds != null && gluedIds.Length > 0)
            {
                foreach (long id in gluedIds)
                {
                    Console.WriteLine($"- Glued Shape ID: {id}");
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}