using Aspose.Diagram;
using Aspose.Diagram.Saving;
using System;
using System.IO;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            var diagram = new Diagram("input.vsdx");

            // Add a simple rectangle shape to the first page (if a page exists)
            if (diagram.Pages.Count > 0)
            {
                // Parameters: PinX, PinY, Master name, Page index (1‑based)
                diagram.AddShape(2.0, 2.0, "Rectangle", 1);
            }

            // Prepare save options – use VDX format and auto‑fit the page to new content
            var saveOptions = new DiagramSaveOptions(SaveFileFormat.Vdx)
            {
                AutoFitPageToDrawingContent = true
            };

            // Save the modified diagram to a new file
            string outputPath = "output.vdx";
            diagram.Save(outputPath, saveOptions);

            // Verify that the file size reflects the added content
            long fileSize = new FileInfo(outputPath).Length;
            Console.WriteLine($"Saved diagram size: {fileSize} bytes");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
