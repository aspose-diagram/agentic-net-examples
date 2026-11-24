using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify that the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block to ensure proper disposal.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // -----------------------------------------------------------------
                // Clear global document protection settings.
                // -----------------------------------------------------------------
                diagram.DocumentSettings.ProtectBkgnds = BOOL.False;
                diagram.DocumentSettings.ProtectMasters = BOOL.False;
                diagram.DocumentSettings.ProtectShapes = BOOL.False;
                diagram.DocumentSettings.ProtectStyles = BOOL.False;

                // -----------------------------------------------------------------
                // Iterate through all pages and shapes to clear shape-level protection.
                // -----------------------------------------------------------------
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Clear each protection flag using BOOL.False.
                        shape.Protection.LockAspect.Value = BOOL.False;
                        shape.Protection.LockBegin.Value = BOOL.False;
                        shape.Protection.LockCalcWH.Value = BOOL.False;
                        shape.Protection.LockCrop.Value = BOOL.False;
                        shape.Protection.LockCustProp.Value = BOOL.False;
                        shape.Protection.LockDelete.Value = BOOL.False;
                        shape.Protection.LockEnd.Value = BOOL.False;
                        shape.Protection.LockFormat.Value = BOOL.False;
                        shape.Protection.LockFromGroupFormat.Value = BOOL.False;
                        shape.Protection.LockGroup.Value = BOOL.False;
                        shape.Protection.LockHeight.Value = BOOL.False;
                        shape.Protection.LockMoveX.Value = BOOL.False;
                        shape.Protection.LockMoveY.Value = BOOL.False;
                        shape.Protection.LockRotate.Value = BOOL.False;
                        shape.Protection.LockSelect.Value = BOOL.False;
                        shape.Protection.LockTextEdit.Value = BOOL.False;
                        shape.Protection.LockThemeColors.Value = BOOL.False;
                        shape.Protection.LockThemeEffects.Value = BOOL.False;
                        shape.Protection.LockVtxEdit.Value = BOOL.False;
                        shape.Protection.LockWidth.Value = BOOL.False;
                    }
                }

                // -----------------------------------------------------------------
                // Save the modified diagram to the specified output path.
                // -----------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram protection cleared and saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}