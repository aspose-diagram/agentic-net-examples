using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VsdxToJpegConverter
{
    static void Main()
    {
        try
        {

            // Load the VSDX diagram from file
            Diagram diagram = new Diagram("input.vsdx");

            // Set JPEG save options with 80% compression quality
            ImageSaveOptions jpegOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
            jpegOptions.JpegQuality = 80; // Compression quality (0-100)

            // Save the diagram as a JPEG image using the specified options
            diagram.Save("output.jpg", jpegOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
