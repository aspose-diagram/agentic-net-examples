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
            Console.Error.WriteLine("Usage: Program <inputVsdxPath> <subShapeId>");
            return;
        }

        string inputPath = args[0];
        string shapeIdArg = args[1];

        // Guard: check that the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Parse the shape ID (must be a long).
        if (!long.TryParse(shapeIdArg, out long targetShapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'. It must be a numeric value.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            Shape targetShape = null;
            Page containingPage = null;

            // Search for the shape across all pages.
            foreach (Page page in diagram.Pages)
            {
                // GetShape returns null if the ID is not present on this page.
                Shape shape = page.Shapes.GetShape(targetShapeId);
                if (shape != null)
                {
                    targetShape = shape;
                    containingPage = page;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {targetShapeId} not found in the diagram.");
                return;
            }

            // Calculate absolute PinX.
            double absolutePinX;

            // If the shape has a parent (i.e., it is a sub‑shape of a group), compute using the parent’s transform.
            if (targetShape.ParentShape != null)
            {
                Shape parentShape = targetShape.ParentShape;

                // Absolute PinX = parent.PinX + child.PinX - parent.LocPinX
                absolutePinX = parentShape.XForm.PinX.Value
                               + targetShape.XForm.PinX.Value
                               - parentShape.XForm.LocPinX.Value;
            }
            else
            {
                // No parent – the PinX is already absolute.
                absolutePinX = targetShape.XForm.PinX.Value;
            }

            Console.WriteLine($"Absolute PinX of shape ID {targetShapeId}: {absolutePinX}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}