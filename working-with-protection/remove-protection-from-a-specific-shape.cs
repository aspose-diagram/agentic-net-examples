using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Identify the target shape by its ID (replace with actual ID as needed)
            long targetShapeId = 5; // example ID

            // Retrieve the shape from the first page (adjust page index if needed)
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes.GetShape(targetShapeId);

            // Validate that the shape was found
            if (shape == null)
            {
                Console.WriteLine("Target shape not found.");
                return;
            }

            // Remove protection by setting lock properties to FALSE
            shape.Protection.LockMoveX.Value = BOOL.False;
            shape.Protection.LockMoveY.Value = BOOL.False;
            shape.Protection.LockWidth.Value = BOOL.False;
            shape.Protection.LockHeight.Value = BOOL.False;
            shape.Protection.LockRotate.Value = BOOL.False;
            shape.Protection.LockVtxEdit.Value = BOOL.False;
            shape.Protection.LockDelete.Value = BOOL.False;
            shape.Protection.LockBegin.Value = BOOL.False;
            shape.Protection.LockEnd.Value = BOOL.False;
            shape.Protection.LockCalcWH.Value = BOOL.False;
            shape.Protection.LockCrop.Value = BOOL.False;
            shape.Protection.LockCustProp.Value = BOOL.False;
            shape.Protection.LockFormat.Value = BOOL.False;
            shape.Protection.LockFromGroupFormat.Value = BOOL.False;
            shape.Protection.LockGroup.Value = BOOL.False;
            shape.Protection.LockSelect.Value = BOOL.False;
            shape.Protection.LockTextEdit.Value = BOOL.False;
            shape.Protection.LockThemeColors.Value = BOOL.False;
            shape.Protection.LockThemeEffects.Value = BOOL.False;
            // Removed invalid properties: LockThemeFonts and LockThemeIndex

            // Save the modified diagram to a new file
            string outputPath = "output_unprotected.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Protection removed from shape ID {targetShapeId} and diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}