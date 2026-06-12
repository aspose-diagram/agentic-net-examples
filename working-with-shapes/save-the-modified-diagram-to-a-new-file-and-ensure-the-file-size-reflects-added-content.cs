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

            // Load the existing Visio diagram from a file
            string sourceFile = "input.vsdx";
            Diagram diagram = new Diagram(sourceFile);

            // Add a simple rectangle shape to the active page (if a page exists)
            if (diagram.Pages.Count > 0)
            {
                // Parameters: PinX, PinY, Master name, Page index (0 = active page)
                diagram.AddShape(2.0, 2.0, "Rectangle", 0);
            }

            // Prepare save options – use VDX format and auto‑fit the page to the new content
            DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vdx)
            {
                AutoFitPageToDrawingContent = true
            };

            // Save the modified diagram to a new file
            string outputFile = "output.vdx";
            diagram.Save(outputFile, saveOptions);

            // Verify that the file size reflects the added content
            long sizeInBytes = new FileInfo(outputFile).Length;
            Console.WriteLine($"Saved diagram size: {sizeInBytes} bytes");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
