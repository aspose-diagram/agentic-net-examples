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

            // Configure save options to auto‑fit the page to the drawing content
            DiagramSaveOptions saveOptions = new DiagramSaveOptions();
            saveOptions.AutoFitPageToDrawingContent = true;

            // Save the diagram; the auto‑fit layout is applied during saving
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
