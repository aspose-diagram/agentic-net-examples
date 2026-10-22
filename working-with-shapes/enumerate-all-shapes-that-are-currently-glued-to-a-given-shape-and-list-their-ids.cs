using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Prompt user for the Visio file path
        Console.Write("Enter the path to the Visio file: ");
        string filePath = Console.ReadLine();

        // Guard: ensure the file exists
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"Error: File not found - {filePath}");
            return;
        }

        // Prompt user for the shape ID to inspect
        Console.Write("Enter the shape ID to enumerate glued shapes: ");
        string idInput = Console.ReadLine();
        if (!long.TryParse(idInput, out long targetShapeId))
        {
            Console.Error.WriteLine("Error: Invalid shape ID.");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Locate the shape with the specified ID across all pages
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                // Attempt to retrieve the shape; GetShape returns null if not found
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

            // Retrieve IDs of all 1D connector shapes glued to the target shape
            // GluedShapesFlags.GluedShapesAll1D gets all 1D (connector) shapes
            long[] gluedIds = targetShape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);

            // Output the results
            Console.WriteLine($"Shape ID {targetShapeId} has {gluedIds.Length} glued connector(s):");
            foreach (long gluedId in gluedIds)
            {
                Console.WriteLine($"- Glued Shape ID: {gluedId}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}