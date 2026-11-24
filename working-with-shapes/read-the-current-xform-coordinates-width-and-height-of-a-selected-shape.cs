using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: diagram file path and shape ID
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeId>");
            return;
        }

        string diagramPath = args[0];
        string shapeIdArg = args[1];

        // Guard: check diagram file existence
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Parse shape ID (must be a long)
        if (!long.TryParse(shapeIdArg, out long targetShapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'. Must be a numeric value.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            Shape foundShape = null;

            // Iterate through all pages to locate the shape with the specified ID
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape is not deleted
                    if (shape.Del == BOOL.False && shape.ID == targetShapeId)
                    {
                        foundShape = shape;
                        break;
                    }
                }
                if (foundShape != null)
                    break;
            }

            if (foundShape == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {targetShapeId} not found in the diagram.");
                return;
            }

            // Read XForm properties: PinX, PinY, Width, Height
            double pinX = foundShape.XForm.PinX.Value;
            double pinY = foundShape.XForm.PinY.Value;
            double width = foundShape.XForm.Width.Value;
            double height = foundShape.XForm.Height.Value;

            // Output the retrieved values
            Console.WriteLine($"Shape ID: {targetShapeId}");
            Console.WriteLine($"PinX: {pinX}");
            Console.WriteLine($"PinY: {pinY}");
            Console.WriteLine($"Width: {width}");
            Console.WriteLine($"Height: {height}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}