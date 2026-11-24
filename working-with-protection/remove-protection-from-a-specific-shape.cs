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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <shapeId> [outputVisioPath]");
            return;
        }

        string inputPath = args[0];
        string shapeIdArg = args[1];
        string outputPath = args.Length >= 3 ? args[2] : "output_unprotected.vsdx";

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Parse the shape ID.
        if (!long.TryParse(shapeIdArg, out long targetShapeId))
        {
            Console.Error.WriteLine($"Error: Shape ID \"{shapeIdArg}\" is not a valid integer.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Locate the shape with the specified ID.
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.ID == targetShapeId)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null) break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Error: No shape with ID {targetShapeId} was found in the diagram.");
                return;
            }

            // Ensure the Protection object exists.
            if (targetShape.Protection == null)
            {
                Console.Error.WriteLine("Error: The target shape does not have a Protection object.");
                return;
            }

            // Remove protection by setting all lock properties to FALSE.
            targetShape.Protection.LockAspect.Value = BOOL.False;
            targetShape.Protection.LockBegin.Value = BOOL.False;
            targetShape.Protection.LockCalcWH.Value = BOOL.False;
            targetShape.Protection.LockCrop.Value = BOOL.False;
            targetShape.Protection.LockCustProp.Value = BOOL.False;
            targetShape.Protection.LockDelete.Value = BOOL.False;
            targetShape.Protection.LockEnd.Value = BOOL.False;
            targetShape.Protection.LockFormat.Value = BOOL.False;
            targetShape.Protection.LockFromGroupFormat.Value = BOOL.False;
            targetShape.Protection.LockGroup.Value = BOOL.False;
            targetShape.Protection.LockHeight.Value = BOOL.False;
            targetShape.Protection.LockMoveX.Value = BOOL.False;
            targetShape.Protection.LockMoveY.Value = BOOL.False;
            targetShape.Protection.LockRotate.Value = BOOL.False;
            targetShape.Protection.LockSelect.Value = BOOL.False;
            targetShape.Protection.LockTextEdit.Value = BOOL.False;
            targetShape.Protection.LockThemeColors.Value = BOOL.False;
            targetShape.Protection.LockThemeEffects.Value = BOOL.False;
            targetShape.Protection.LockVtxEdit.Value = BOOL.False;
            targetShape.Protection.LockWidth.Value = BOOL.False;

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Shape protection removed. Diagram saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}