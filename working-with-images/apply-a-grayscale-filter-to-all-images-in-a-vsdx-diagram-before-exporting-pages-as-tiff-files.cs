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

            // Configure image save options for TIFF format with grayscale color mode
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Tiff);
            saveOptions.ImageColorMode = ImageColorMode.Grayscale; // Apply grayscale filter to all rendered pages

            // Export all pages of the diagram as a multi‑page TIFF file
            diagram.Save("output.tiff", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
