using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, shape NameU, output file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioFile> <shapeNameU> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string targetShapeNameU = args[1];
        string outputPath = args[2];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Locate the shape with the specified NameU across all pages
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU == targetShapeNameU)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null) break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape with NameU \"{targetShapeNameU}\" not found.");
                return;
            }

            // Remove protection by setting all lock properties to FALSE
            // Note: Each lock property is a BoolValue; assign via .Value
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

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Protection removed from shape \"{targetShapeNameU}\" and diagram saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}