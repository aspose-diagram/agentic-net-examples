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

            // Load the VSDX diagram from file
            Diagram diagram = new Diagram("input.vsdx");

            // Configure image save options:
            // - Save format: TIFF
            // - Color mode: Grayscale (applies to all rendered pages)
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Tiff);
            saveOptions.ImageColorMode = ImageColorMode.Grayscale;

            // Export all pages of the diagram as a multipage TIFF file
            diagram.Save("output.tiff", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
