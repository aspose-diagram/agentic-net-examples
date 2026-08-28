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

            // Create image save options for JPEG format
            ImageSaveOptions jpegOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
            // Set the compression quality to 80%
            jpegOptions.JpegQuality = 80;

            // Save the diagram as a JPEG image using the specified options
            diagram.Save("output.jpg", jpegOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
