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

            // Input Visio file (existing diagram)
            string inputPath = "input.vsdx";

            // Output Visio file (modified diagram)
            string outputPath = "output.vsdx";

            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // -------------------------------------------------
            // Perform any modifications to the diagram here.
            // -------------------------------------------------

            // Prepare save options to preserve the original page layout
            DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vsdx);
            saveOptions.AutoFitPageToDrawingContent = false; // keep original page size and layout

            // Save the modified diagram back to a Visio file
            diagram.Save(outputPath, saveOptions);

            // Release resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
