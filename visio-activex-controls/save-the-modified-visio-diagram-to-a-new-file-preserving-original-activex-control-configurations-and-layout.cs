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

            // Load the existing diagram preserving all its content, including ActiveX controls
            Diagram diagram = new Diagram(sourceFile);

            // -------------------------------------------------
            // Perform any required modifications here.
            // For this task we only need to save the diagram,
            // so no changes are made to the diagram object.
            // -------------------------------------------------

            // Configure save options to keep the original layout.
            // Setting AutoFitPageToDrawingContent to false ensures the page size
            // is not altered during the save operation, thus preserving layout.
            DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vsdx);
            saveOptions.AutoFitPageToDrawingContent = false;

            // Save the diagram to a new file using the specified options.
            diagram.Save(destinationFile, saveOptions);

            // Release resources held by the Diagram object.
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
