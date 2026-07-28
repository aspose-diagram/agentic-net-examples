using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output_unprotected.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Clear global document protection settings
                diagram.DocumentSettings.ProtectBkgnds = BOOL.False;
                diagram.DocumentSettings.ProtectMasters = BOOL.False;
                diagram.DocumentSettings.ProtectShapes = BOOL.False;
                diagram.DocumentSettings.ProtectStyles = BOOL.False;

                // Iterate through all pages and shapes to clear shape-level protection
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
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

                // Save the diagram with all protections cleared
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("All protection settings have been cleared and the diagram saved.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
