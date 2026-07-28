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

            // Path to the original Visio file (could be VSDX, VDX, etc.)
            string sourceFile = "input.vsdx";

            // Path for the new file that will contain the saved diagram
            string destinationFile = "output.vsdx";

            // Load the existing diagram. This preserves all existing objects,
            // including ActiveX controls and their configurations.
            Diagram diagram = new Diagram(sourceFile);

            // -----------------------------------------------------------------
            // Perform any required modifications to the diagram here.
            // For this task we only need to preserve the original layout,
            // so no changes are made.
            // -----------------------------------------------------------------

            // Create save options that keep the page size unchanged.
            // Setting AutoFitPageToDrawingContent to false ensures the layout
            // (positions of shapes, connectors, and ActiveX controls) is not altered.
            DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vsdx)
            {
                AutoFitPageToDrawingContent = false
            };

            // Save the diagram to a new file using the specified options.
            diagram.Save(destinationFile, saveOptions);

            // Release resources.
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
