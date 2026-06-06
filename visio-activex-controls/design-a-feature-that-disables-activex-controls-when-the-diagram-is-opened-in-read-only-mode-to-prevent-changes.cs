using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Determine if any window is marked as read‑only
            bool isReadOnly = false;
            foreach (Window win in diagram.Windows)
            {
                if (win.ReadOnly == BOOL.True)
                {
                    isReadOnly = true;
                    break;
                }
            }

            // If the diagram is opened in read‑only mode, lock all ActiveX controls
            if (isReadOnly)
            {
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.ActiveXControl != null)
                        {
                            // Prevent selection, movement, deletion and other edits
                            shape.Protection.LockSelect.Value = BOOL.True;
                            shape.Protection.LockBegin.Value = BOOL.True;
                            shape.Protection.LockEnd.Value = BOOL.True;
                            shape.Protection.LockMoveX.Value = BOOL.True;
                            shape.Protection.LockMoveY.Value = BOOL.True;
                            shape.Protection.LockDelete.Value = BOOL.True;
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
