using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Clear all shape protection locks
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

            // Validate that no shape still has any lock enabled
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Protection.LockAspect.Value == BOOL.True ||
                        shape.Protection.LockBegin.Value == BOOL.True ||
                        shape.Protection.LockCalcWH.Value == BOOL.True ||
                        shape.Protection.LockCrop.Value == BOOL.True ||
                        shape.Protection.LockCustProp.Value == BOOL.True ||
                        shape.Protection.LockDelete.Value == BOOL.True ||
                        shape.Protection.LockEnd.Value == BOOL.True ||
                        shape.Protection.LockFormat.Value == BOOL.True ||
                        shape.Protection.LockFromGroupFormat.Value == BOOL.True ||
                        shape.Protection.LockGroup.Value == BOOL.True ||
                        shape.Protection.LockHeight.Value == BOOL.True ||
                        shape.Protection.LockMoveX.Value == BOOL.True ||
                        shape.Protection.LockMoveY.Value == BOOL.True ||
                        shape.Protection.LockRotate.Value == BOOL.True ||
                        shape.Protection.LockSelect.Value == BOOL.True ||
                        shape.Protection.LockTextEdit.Value == BOOL.True ||
                        shape.Protection.LockThemeColors.Value == BOOL.True ||
                        shape.Protection.LockThemeEffects.Value == BOOL.True ||
                        shape.Protection.LockVtxEdit.Value == BOOL.True ||
                        shape.Protection.LockWidth.Value == BOOL.True)
                    {
                        throw new Exception($"Shape ID {shape.ID} still has protection locks enabled.");
                    }
                }
            }

            Console.WriteLine("All shape protection locks have been cleared and validated.");

            // Optionally save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
