using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
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
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ----- Clear global document protection settings -----
                diagram.DocumentSettings.ProtectBkgnds = BOOL.False;
                diagram.DocumentSettings.ProtectMasters = BOOL.False;
                diagram.DocumentSettings.ProtectShapes = BOOL.False;
                diagram.DocumentSettings.ProtectStyles = BOOL.False;

                // ----- Iterate through all pages and shapes to clear shape protection -----
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Clear each protection flag on the shape.
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

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}