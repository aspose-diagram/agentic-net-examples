using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output_unprotected.vsdx";

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // ----- Clear global document protection -----
            diagram.DocumentSettings.ProtectBkgnds = BOOL.False;
            diagram.DocumentSettings.ProtectMasters = BOOL.False;
            diagram.DocumentSettings.ProtectShapes = BOOL.False;
            diagram.DocumentSettings.ProtectStyles = BOOL.False;

            // ----- Clear shape‑level protection for every shape on every page -----
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Unlock all supported protection flags
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

            // Save the unprotected diagram to the output file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}