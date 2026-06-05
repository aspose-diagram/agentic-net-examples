using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportDiagramToPng
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Create image save options for PNG format
            ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);

            // Ensure background (page) is included in the rendered image
            options.SaveForegroundPagesOnly = false;

            // NOTE: Aspose.Diagram does not expose a direct property to set a solid background color
            // for the rendered image. To achieve a specific background color, you would need to add
            // a rectangle shape covering the page and set its fill color before saving.

            // Save the diagram as PNG with the specified options
            diagram.Save("output.png", options);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
