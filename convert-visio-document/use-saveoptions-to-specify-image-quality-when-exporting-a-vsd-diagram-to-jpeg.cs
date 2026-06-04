using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportDiagramToJpeg
{
    static void Main()
    {
        try
        {

            // Load the VSD diagram from file
            Diagram diagram = new Diagram("input.vsd");

            // Create image save options for JPEG format
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);

            // Set the desired JPEG quality (0‑100). Lower value = higher compression.
            saveOptions.JpegQuality = 70;

            // Export the diagram to a JPEG file using the specified quality
            diagram.Save("output.jpg", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
