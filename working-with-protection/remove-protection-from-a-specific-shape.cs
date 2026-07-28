using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: inputVisioPath shapeId outputVisioPath
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <inputVisioPath> <shapeId> <outputVisioPath>");
            return;
        }

        // Assign input and output paths
        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Parse shape ID (may throw if not a number)
        long shapeIdLong = long.Parse(args[1]);
        string outputPath = args[2];

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Locate the shape with the specified ID across all pages
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                // GetShape expects an int ID; cast safely
                int shapeId = (int)shapeIdLong;
                targetShape = page.Shapes.GetShape(shapeId);
                if (targetShape != null)
                    break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeIdLong} not found.");
                return;
            }

            // Remove all protection locks from the shape
            targetShape.Protection.LockAspect.Value = BOOL.False;
            targetShape.Protection.LockBegin.Value = BOOL.False;
            // LockCalcWH is not a valid property; omitted per API rules
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

            // Save the modified diagram to the output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}