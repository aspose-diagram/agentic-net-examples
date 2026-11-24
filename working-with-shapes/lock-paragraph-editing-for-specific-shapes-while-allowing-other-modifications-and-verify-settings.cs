using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the diagram inside a using block as required
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define the IDs of shapes whose paragraph editing should be locked
                // (example: lock shape with ID 1; add more IDs as needed)
                long[] shapesToLock = new long[] { 1 };

                // Iterate through all pages and shapes to apply the lock
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the current shape is in the lock list
                        bool shouldLock = false;
                        foreach (long id in shapesToLock)
                        {
                            if (shape.ID == id)
                            {
                                shouldLock = true;
                                break;
                            }
                        }

                        if (shouldLock)
                        {
                            // Lock paragraph (text) editing for this shape
                            shape.Protection.LockTextEdit.Value = BOOL.True;

                            // Ensure other lock properties remain unlocked (optional explicit setting)
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
                            shape.Protection.LockThemeColors.Value = BOOL.False;
                            shape.Protection.LockThemeEffects.Value = BOOL.False;
                            shape.Protection.LockVtxEdit.Value = BOOL.False;
                            shape.Protection.LockWidth.Value = BOOL.False;
                        }
                    }
                }

                // Verification: output lock status for each shape
                Console.WriteLine("Paragraph editing lock status per shape:");
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        BOOL lockStatus = shape.Protection.LockTextEdit.Value;
                        Console.WriteLine($"Shape ID {shape.ID}, Name \"{shape.Name}\": LockTextEdit = {lockStatus}");
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}