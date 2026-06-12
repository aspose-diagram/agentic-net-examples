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

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // -------------------------------------------------
            // Perform any required modifications to the diagram
            // -------------------------------------------------
            // Example: diagram.Pages[0].Shapes[0].Text.Value = "Updated";

            // Configure save options to keep the original page layout
            DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vdx);
            saveOptions.AutoFitPageToDrawingContent = false; // preserve original layout

            // Save the modified diagram back to a Visio file
            diagram.Save("output.vdx", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
