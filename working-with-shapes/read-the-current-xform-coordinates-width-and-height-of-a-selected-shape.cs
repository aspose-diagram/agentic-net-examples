using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Prompt for diagram file path
        Console.Write("Enter the path to the Visio diagram file: ");
        string diagramPath = Console.ReadLine();

        // Guard: ensure the file exists
        if (string.IsNullOrWhiteSpace(diagramPath) || !File.Exists(diagramPath))
        {
            Console.Error.WriteLine("Error: Diagram file not found.");
            return;
        }

        // Prompt for the shape ID to inspect
        Console.Write("Enter the Shape ID (numeric) to read XForm data: ");
        string shapeIdInput = Console.ReadLine();

        if (!long.TryParse(shapeIdInput, out long targetShapeId))
        {
            Console.Error.WriteLine("Error: Invalid Shape ID.");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Search for the shape with the specified ID across all pages
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    if (shape.ID == targetShapeId)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null)
                    break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {targetShapeId} not found.");
                return;
            }

            // Read XForm properties
            double pinX = targetShape.XForm.PinX.Value;
            double pinY = targetShape.XForm.PinY.Value;
            double width = targetShape.XForm.Width.Value;
            double height = targetShape.XForm.Height.Value;

            // Output the retrieved values
            Console.WriteLine("Shape XForm Data:");
            Console.WriteLine($"  PinX   : {pinX}");
            Console.WriteLine($"  PinY   : {pinY}");
            Console.WriteLine($"  Width  : {width}");
            Console.WriteLine($"  Height : {height}");
        }
        catch (Exception ex)
        {
            // Log any Aspose or runtime errors
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}