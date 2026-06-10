using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram (replace with actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the Protection object exists
                        if (shape.Protection == null)
                            continue;

                        // Clear all lock properties
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

                        // Validate that no lock property remains set to TRUE
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
                            throw new Exception($"Shape ID {shape.ID} on page '{page.Name}' still has locked attributes after clearing protection.");
                        }
                    }
                }

                Console.WriteLine("All shape protection attributes have been cleared and validated successfully.");

                // Optionally save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }