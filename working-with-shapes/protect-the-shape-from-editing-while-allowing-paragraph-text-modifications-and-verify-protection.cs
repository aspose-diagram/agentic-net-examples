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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page.
            Page page = diagram.Pages[0];

            // Find the first shape on the page.
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                targetShape = shape;
                break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("No shapes found on the first page.");
                return;
            }

            // ------------------------------------------------------------
            // Protect the shape from editing while allowing paragraph text modifications.
            // ------------------------------------------------------------
            // Lock various aspects of the shape.
            targetShape.Protection.LockAspect.Value = BOOL.True;
            targetShape.Protection.LockBegin.Value = BOOL.True;
            targetShape.Protection.LockCalcWH.Value = BOOL.True;
            targetShape.Protection.LockCrop.Value = BOOL.True;
            targetShape.Protection.LockCustProp.Value = BOOL.True;
            targetShape.Protection.LockDelete.Value = BOOL.True;
            targetShape.Protection.LockEnd.Value = BOOL.True;
            targetShape.Protection.LockFormat.Value = BOOL.True;
            targetShape.Protection.LockFromGroupFormat.Value = BOOL.True;
            targetShape.Protection.LockGroup.Value = BOOL.True;
            targetShape.Protection.LockHeight.Value = BOOL.True;
            targetShape.Protection.LockMoveX.Value = BOOL.True;
            targetShape.Protection.LockMoveY.Value = BOOL.True;
            targetShape.Protection.LockRotate.Value = BOOL.True;
            targetShape.Protection.LockSelect.Value = BOOL.True;
            targetShape.Protection.LockThemeColors.Value = BOOL.True;
            targetShape.Protection.LockThemeEffects.Value = BOOL.True;
            targetShape.Protection.LockVtxEdit.Value = BOOL.True;
            targetShape.Protection.LockWidth.Value = BOOL.True;

            // Allow paragraph text modifications by NOT locking text edit.
            targetShape.Protection.LockTextEdit.Value = BOOL.False;

            // ------------------------------------------------------------
            // Verify protection settings by outputting each property's value.
            // ------------------------------------------------------------
            Console.WriteLine("Shape protection verification:");
            Console.WriteLine($"LockAspect: {targetShape.Protection.LockAspect.Value}");
            Console.WriteLine($"LockBegin: {targetShape.Protection.LockBegin.Value}");
            Console.WriteLine($"LockCalcWH: {targetShape.Protection.LockCalcWH.Value}");
            Console.WriteLine($"LockCrop: {targetShape.Protection.LockCrop.Value}");
            Console.WriteLine($"LockCustProp: {targetShape.Protection.LockCustProp.Value}");
            Console.WriteLine($"LockDelete: {targetShape.Protection.LockDelete.Value}");
            Console.WriteLine($"LockEnd: {targetShape.Protection.LockEnd.Value}");
            Console.WriteLine($"LockFormat: {targetShape.Protection.LockFormat.Value}");
            Console.WriteLine($"LockFromGroupFormat: {targetShape.Protection.LockFromGroupFormat.Value}");
            Console.WriteLine($"LockGroup: {targetShape.Protection.LockGroup.Value}");
            Console.WriteLine($"LockHeight: {targetShape.Protection.LockHeight.Value}");
            Console.WriteLine($"LockMoveX: {targetShape.Protection.LockMoveX.Value}");
            Console.WriteLine($"LockMoveY: {targetShape.Protection.LockMoveY.Value}");
            Console.WriteLine($"LockRotate: {targetShape.Protection.LockRotate.Value}");
            Console.WriteLine($"LockSelect: {targetShape.Protection.LockSelect.Value}");
            Console.WriteLine($"LockThemeColors: {targetShape.Protection.LockThemeColors.Value}");
            Console.WriteLine($"LockThemeEffects: {targetShape.Protection.LockThemeEffects.Value}");
            Console.WriteLine($"LockVtxEdit: {targetShape.Protection.LockVtxEdit.Value}");
            Console.WriteLine($"LockWidth: {targetShape.Protection.LockWidth.Value}");
            Console.WriteLine($"LockTextEdit (should be False): {targetShape.Protection.LockTextEdit.Value}");

            // ------------------------------------------------------------
            // Save the modified diagram.
            // ------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}