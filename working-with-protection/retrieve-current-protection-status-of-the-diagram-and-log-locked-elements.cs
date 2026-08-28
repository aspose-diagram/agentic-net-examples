using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Log document‑level protection setting
            Console.WriteLine("Document ProtectShapes: " + diagram.DocumentSettings.ProtectShapes);

            // Iterate through all pages and shapes to find locked elements
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    Protection prot = shape.Protection;
                    List<string> locked = new List<string>();

                    if (prot.LockAspect.Value != 0)          locked.Add("LockAspect");
                    if (prot.LockBegin.Value != 0)           locked.Add("LockBegin");
                    if (prot.LockCalcWH.Value != 0)          locked.Add("LockCalcWH");
                    if (prot.LockCrop.Value != 0)            locked.Add("LockCrop");
                    if (prot.LockCustProp.Value != 0)        locked.Add("LockCustProp");
                    if (prot.LockDelete.Value != 0)          locked.Add("LockDelete");
                    if (prot.LockEnd.Value != 0)             locked.Add("LockEnd");
                    if (prot.LockFormat.Value != 0)          locked.Add("LockFormat");
                    if (prot.LockFromGroupFormat.Value != 0) locked.Add("LockFromGroupFormat");
                    if (prot.LockGroup.Value != 0)           locked.Add("LockGroup");
                    if (prot.LockHeight.Value != 0)          locked.Add("LockHeight");
                    if (prot.LockMoveX.Value != 0)           locked.Add("LockMoveX");
                    if (prot.LockMoveY.Value != 0)           locked.Add("LockMoveY");
                    if (prot.LockRotate.Value != 0)          locked.Add("LockRotate");
                    if (prot.LockSelect.Value != 0)          locked.Add("LockSelect");
                    if (prot.LockTextEdit.Value != 0)        locked.Add("LockTextEdit");
                    if (prot.LockThemeColors.Value != 0)     locked.Add("LockThemeColors");
                    if (prot.LockThemeEffects.Value != 0)    locked.Add("LockThemeEffects");
                    if (prot.LockVtxEdit.Value != 0)         locked.Add("LockVtxEdit");
                    if (prot.LockWidth.Value != 0)           locked.Add("LockWidth");

                    if (locked.Count > 0)
                    {
                        Console.WriteLine($"Shape ID={shape.ID}, Name={shape.Name}, Locked: {string.Join(", ", locked)}");
                    }
                }
            }

            // No changes made; saving is optional
            // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
