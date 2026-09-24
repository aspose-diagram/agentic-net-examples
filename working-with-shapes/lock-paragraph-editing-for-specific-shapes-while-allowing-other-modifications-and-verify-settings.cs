using System;
using System.IO;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        // Paths to the source and destination Visio files
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists before proceeding
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }
        string outputPath = "output_locked.vsdx";

        // Load the diagram inside a try/catch to handle any loading errors
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Lock paragraph (text) editing for shapes named "TargetShape"
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                if (shape.NameU == "TargetShape")
                {
                    // Lock text (paragraph) editing
                    shape.Protection.LockTextEdit.Value = BOOL.True;

                    // Ensure other protection flags remain unlocked
                    shape.Protection.LockMoveX.Value = BOOL.False;
                    shape.Protection.LockMoveY.Value = BOOL.False;
                    shape.Protection.LockWidth.Value = BOOL.False;
                    shape.Protection.LockHeight.Value = BOOL.False;
                    shape.Protection.LockRotate.Value = BOOL.False;
                    shape.Protection.LockBegin.Value = BOOL.False;
                    shape.Protection.LockEnd.Value = BOOL.False;
                    shape.Protection.LockSelect.Value = BOOL.False;
                    shape.Protection.LockDelete.Value = BOOL.False;
                    shape.Protection.LockFormat.Value = BOOL.False;
                    shape.Protection.LockThemeColors.Value = BOOL.False;
                    shape.Protection.LockThemeEffects.Value = BOOL.False;
                    shape.Protection.LockVtxEdit.Value = BOOL.False;
                }
            }
        }

        // Save the modified diagram inside a try/catch to capture any save errors
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
            return;
        }

        // Verify that the protection settings were applied correctly
        Diagram verifyDiagram;
        try
        {
            verifyDiagram = new Diagram(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading verification diagram: {ex.Message}");
            return;
        }

        foreach (Page page in verifyDiagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                if (shape.NameU == "TargetShape")
                {
                    // Verify paragraph (text) lock
                    if (shape.Protection.LockTextEdit.Value != BOOL.True)
                        throw new Exception("LockTextEdit was not set correctly.");

                    // Verify that other locks are not enabled
                    if (shape.Protection.LockMoveX.Value != BOOL.False ||
                        shape.Protection.LockMoveY.Value != BOOL.False ||
                        shape.Protection.LockWidth.Value != BOOL.False ||
                        shape.Protection.LockHeight.Value != BOOL.False ||
                        shape.Protection.LockRotate.Value != BOOL.False ||
                        shape.Protection.LockBegin.Value != BOOL.False ||
                        shape.Protection.LockEnd.Value != BOOL.False ||
                        shape.Protection.LockSelect.Value != BOOL.False ||
                        shape.Protection.LockDelete.Value != BOOL.False ||
                        shape.Protection.LockFormat.Value != BOOL.False ||
                        shape.Protection.LockThemeColors.Value != BOOL.False ||
                        shape.Protection.LockThemeEffects.Value != BOOL.False ||
                        shape.Protection.LockVtxEdit.Value != BOOL.False)
                    {
                        throw new Exception("Unexpected protection flags were modified.");
                    }

                    Console.WriteLine($"Shape '{shape.NameU}' paragraph editing locked successfully.");
                }
            }
        }
    }
}