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

            // Load the Visio diagram from a file
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Create image save options (PNG format) and disable comment export
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // RenderingSaveOptions (base class) provides IsExportComments to control comment visibility
            saveOptions.IsExportComments = false;

            // Save the diagram as an image without comment overlays
            string outputPath = "output.png";
            diagram.Save(outputPath, saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
