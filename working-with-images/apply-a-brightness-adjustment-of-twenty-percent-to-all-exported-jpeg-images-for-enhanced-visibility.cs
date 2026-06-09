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

            // Load the source Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Prepare JPEG save options and increase brightness by 20%
            // Default brightness is 0.5; adding 0.2 gives 0.7 (range 0‑1)
            ImageSaveOptions jpegOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
            jpegOptions.ImageBrightness = 0.7f;

            // Export the diagram pages to JPEG files using the configured options
            // When saving to JPEG, Aspose.Diagram creates separate files for each page
            diagram.Save("output.jpg", jpegOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
