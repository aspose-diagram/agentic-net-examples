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

            // Load the VSDX diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Configure JPEG quality (80%)
            ImageSaveOptions jpegOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
            jpegOptions.JpegQuality = 80;

            // Save the diagram as a JPEG image
            diagram.Save("output.jpg", jpegOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
