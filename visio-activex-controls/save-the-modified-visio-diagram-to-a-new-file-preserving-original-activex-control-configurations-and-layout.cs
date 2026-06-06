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

            // Load the original Visio diagram (preserves all ActiveX controls and layout)
            Diagram diagram = new Diagram("input.vsdx");

            // -----------------------------------------------------------------
            // Place any modifications to the diagram here (e.g., shape edits)
            // -----------------------------------------------------------------

            // Prepare save options to keep the same format (VSDX) and default settings
            DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vsdx);

            // Save the modified diagram to a new file, preserving ActiveX controls and layout
            diagram.Save("output.vsdx", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
