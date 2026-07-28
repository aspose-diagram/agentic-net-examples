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

            // Access the first page
            Page page = diagram.Pages[0];

            // Find the first non‑deleted shape on the page
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.False) // ensure the shape is not marked as deleted
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.WriteLine("No shape found to protect.");
                return;
            }

            // Apply protection: lock movement, size, rotation, and vertex editing
            // but keep text editing unlocked
            targetShape.Protection.LockMoveX.Value   = BOOL.True;
            targetShape.Protection.LockMoveY.Value   = BOOL.True;
            targetShape.Protection.LockWidth.Value   = BOOL.True;
            targetShape.Protection.LockHeight.Value  = BOOL.True;
            targetShape.Protection.LockRotate.Value  = BOOL.True;
            targetShape.Protection.LockVtxEdit.Value = BOOL.True;
            targetShape.Protection.LockTextEdit.Value = BOOL.False; // allow paragraph text changes

            // Verify that the protection flags are set as intended
            bool protectionOk = targetShape.Protection.LockMoveX.Value   == BOOL.True &&
                                targetShape.Protection.LockMoveY.Value   == BOOL.True &&
                                targetShape.Protection.LockWidth.Value   == BOOL.True &&
                                targetShape.Protection.LockHeight.Value  == BOOL.True &&
                                targetShape.Protection.LockRotate.Value  == BOOL.True &&
                                targetShape.Protection.LockVtxEdit.Value == BOOL.True &&
                                targetShape.Protection.LockTextEdit.Value == BOOL.False;

            Console.WriteLine(protectionOk
                ? "Shape protection applied successfully."
                : "Shape protection verification failed.");

            // Save the modified diagram
            string outputPath = "protected_output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
