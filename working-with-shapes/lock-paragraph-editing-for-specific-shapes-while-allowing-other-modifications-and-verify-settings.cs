using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Shapes whose paragraph formatting should be locked (by shape name)
            string[] targetShapeNames = { "Process", "Decision" };

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify target shapes (case‑insensitive match on Name)
                    if (Array.Exists(targetShapeNames, n => n.Equals(shape.Name, StringComparison.OrdinalIgnoreCase)))
                    {
                        // Lock paragraph formatting (LockFormat) while keeping other protections off
                        shape.Protection.LockFormat.Value = BOOL.True;
                        shape.Protection.LockMoveX.Value = BOOL.False;
                        shape.Protection.LockMoveY.Value = BOOL.False;
                        shape.Protection.LockWidth.Value = BOOL.False;
                        shape.Protection.LockHeight.Value = BOOL.False;
                        shape.Protection.LockRotate.Value = BOOL.False;
                        shape.Protection.LockTextEdit.Value = BOOL.False; // allow text editing

                        // Verify that the locks are set as intended
                        if (shape.Protection.LockFormat.Value != BOOL.True)
                            throw new Exception($"LockFormat not applied to shape ID {shape.ID}.");

                        if (shape.Protection.LockMoveX.Value != BOOL.False ||
                            shape.Protection.LockMoveY.Value != BOOL.False ||
                            shape.Protection.LockWidth.Value != BOOL.False ||
                            shape.Protection.LockHeight.Value != BOOL.False ||
                            shape.Protection.LockRotate.Value != BOOL.False ||
                            shape.Protection.LockTextEdit.Value != BOOL.False)
                        {
                            throw new Exception($"Unexpected lock settings on shape ID {shape.ID}.");
                        }

                        Console.WriteLine($"Paragraph editing locked for shape '{shape.Name}' (ID {shape.ID}).");
                    }
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
