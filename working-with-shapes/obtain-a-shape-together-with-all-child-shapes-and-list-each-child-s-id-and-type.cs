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
            Console.Error.WriteLine("Usage: <program> <diagramPath> <shapeId>");
            return;
        }

        // Assign and guard the diagram file path.
        string diagramPath = args[0];
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Parse the shape ID argument to a long.
        if (!long.TryParse(args[1], out long targetShapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {args[1]}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(diagramPath);

            // Search all pages for the shape with the requested ID.
            Shape parentShape = null;
            foreach (Page page in diagram.Pages)
            {
                // GetShape returns null if the ID does not exist on this page.
                Shape shape = page.Shapes.GetShape(targetShapeId);
                if (shape != null)
                {
                    parentShape = shape;
                    break;
                }
            }

            // If the shape was not found, report and exit.
            if (parentShape == null)
            {
                Console.Error.WriteLine($"Shape with ID {targetShapeId} not found in any page.");
                return;
            }

            // Output the parent shape information.
            Console.WriteLine($"Parent Shape ID: {parentShape.ID}, Type: {parentShape.Type}");

            // If the shape is a group, it may contain child shapes.
            // The child collection is accessed via the Shapes property.
            if (parentShape.Shapes != null && parentShape.Shapes.Count > 0)
            {
                Console.WriteLine("Child Shapes:");
                foreach (Shape child in parentShape.Shapes)
                {
                    // List each child's ID and its Type enum value.
                    Console.WriteLine($"  Child ID: {child.ID}, Type: {child.Type}");
                }
            }
            else
            {
                // No child shapes present (shape is not a group or is empty).
                Console.WriteLine("The specified shape has no child shapes.");
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}