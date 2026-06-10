using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // ----- Clear global document protection settings -----
            diagram.DocumentSettings.ProtectBkgnds = BOOL.False;
            diagram.DocumentSettings.ProtectMasters = BOOL.False;
            diagram.DocumentSettings.ProtectShapes = BOOL.False;
            diagram.DocumentSettings.ProtectStyles = BOOL.False;

            // ----- Clear shape‑level protection for every shape on every page -----
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Unlock all supported protection cells
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

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
