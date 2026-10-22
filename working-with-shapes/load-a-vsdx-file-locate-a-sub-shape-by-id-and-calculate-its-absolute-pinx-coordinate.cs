using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVsdxPath> <shapeId>");
            return;
        }

        string inputPath = args[0];
        string shapeIdArg = args[1];

        // Guard: check if the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Parse shape ID
        if (!long.TryParse(shapeIdArg, out long targetShapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'. Must be a numeric value.");
            return;
        }

        try
        {
            // Load the VSDX diagram
            Diagram diagram = new Diagram(inputPath);

            // Locate the shape by ID across all pages
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                // Attempt to retrieve the shape; GetShape returns null if not found
                Shape shape = page.Shapes.GetShape(targetShapeId);
                if (shape != null && shape.ID == targetShapeId)
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

            // Calculate absolute PinX coordinate (recursive for nested groups)
            double absolutePinX = GetAbsolutePinX(targetShape);
            Console.WriteLine($"Absolute PinX for shape ID {targetShapeId}: {absolutePinX}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Recursively calculates the absolute PinX coordinate of a shape,
    /// taking into account parent group transformations.
    /// </summary>
    /// <param name="shape">The shape whose absolute PinX is required.</param>
    /// <returns>Absolute PinX value.</returns>
    private static double GetAbsolutePinX(Shape shape)
    {
        // If the shape has no parent, its PinX is already absolute
        if (shape.ParentShape == null)
        {
            return shape.XForm.PinX.Value;
        }

        // Recursively get the parent's absolute PinX
        double parentAbsolutePinX = GetAbsolutePinX(shape.ParentShape);

        // Adjust for the parent's local origin (LocPinX)
        double absolutePinX = parentAbsolutePinX + shape.XForm.PinX.Value - shape.ParentShape.XForm.LocPinX.Value;
        return absolutePinX;
    }
}