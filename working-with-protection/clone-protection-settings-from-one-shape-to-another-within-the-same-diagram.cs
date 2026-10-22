using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expecting four arguments: inputVisioPath sourceShapeId targetShapeId outputVisioPath
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <inputVisioPath> <sourceShapeId> <targetShapeId> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string sourceIdStr = args[1];
        string targetIdStr = args[2];
        string outputPath = args[3];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: parse shape IDs
        if (!long.TryParse(sourceIdStr, out long sourceShapeId))
        {
            Console.Error.WriteLine($"Invalid source shape ID: {sourceIdStr}");
            return;
        }

        if (!long.TryParse(targetIdStr, out long targetShapeId))
        {
            Console.Error.WriteLine($"Invalid target shape ID: {targetIdStr}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Retrieve source shape
            Shape sourceShape = GetShapeById(diagram, sourceShapeId);
            if (sourceShape == null)
            {
                Console.Error.WriteLine($"Source shape with ID {sourceShapeId} not found.");
                return;
            }

            // Retrieve target shape
            Shape targetShape = GetShapeById(diagram, targetShapeId);
            if (targetShape == null)
            {
                Console.Error.WriteLine($"Target shape with ID {targetShapeId} not found.");
                return;
            }

            // Clone protection settings from source to target
            // Each protection property is a BoolValue; copy its .Value (BOOL enum)
            targetShape.Protection.LockAspect.Value = sourceShape.Protection.LockAspect.Value;
            targetShape.Protection.LockBegin.Value = sourceShape.Protection.LockBegin.Value;
            targetShape.Protection.LockCalcWH.Value = sourceShape.Protection.LockCalcWH.Value;
            targetShape.Protection.LockCrop.Value = sourceShape.Protection.LockCrop.Value;
            targetShape.Protection.LockCustProp.Value = sourceShape.Protection.LockCustProp.Value;
            targetShape.Protection.LockDelete.Value = sourceShape.Protection.LockDelete.Value;
            targetShape.Protection.LockEnd.Value = sourceShape.Protection.LockEnd.Value;
            targetShape.Protection.LockFormat.Value = sourceShape.Protection.LockFormat.Value;
            targetShape.Protection.LockFromGroupFormat.Value = sourceShape.Protection.LockFromGroupFormat.Value;
            targetShape.Protection.LockGroup.Value = sourceShape.Protection.LockGroup.Value;
            targetShape.Protection.LockHeight.Value = sourceShape.Protection.LockHeight.Value;
            targetShape.Protection.LockMoveX.Value = sourceShape.Protection.LockMoveX.Value;
            targetShape.Protection.LockMoveY.Value = sourceShape.Protection.LockMoveY.Value;
            targetShape.Protection.LockRotate.Value = sourceShape.Protection.LockRotate.Value;
            targetShape.Protection.LockSelect.Value = sourceShape.Protection.LockSelect.Value;
            targetShape.Protection.LockTextEdit.Value = sourceShape.Protection.LockTextEdit.Value;
            targetShape.Protection.LockThemeColors.Value = sourceShape.Protection.LockThemeColors.Value;
            targetShape.Protection.LockThemeEffects.Value = sourceShape.Protection.LockThemeEffects.Value;
            targetShape.Protection.LockVtxEdit.Value = sourceShape.Protection.LockVtxEdit.Value;
            targetShape.Protection.LockWidth.Value = sourceShape.Protection.LockWidth.Value;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Protection settings cloned successfully. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Searches all pages in the diagram for a shape with the specified ID.
    /// Returns null if not found.
    /// </summary>
    private static Shape GetShapeById(Diagram diagram, long shapeId)
    {
        foreach (Page page in diagram.Pages)
        {
            try
            {
                // Attempt to retrieve the shape; if not present, an exception may be thrown.
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape != null)
                    return shape;
            }
            catch
            {
                // Ignore and continue searching other pages.
            }
        }
        return null;
    }
}