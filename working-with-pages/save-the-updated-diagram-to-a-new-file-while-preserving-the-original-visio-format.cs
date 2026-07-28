using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the original Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // -----------------------------------------------------------------
            // Perform any required modifications to the diagram here.
            // -----------------------------------------------------------------

            // Create save options that preserve the original Visio format (VSDX)
            DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vsdx);
            // Example of an additional option (optional):
            // saveOptions.AutoFitPageToDrawingContent = true;

            // Save the updated diagram to a new file while keeping the same format
            diagram.Save("output.vsdx", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
