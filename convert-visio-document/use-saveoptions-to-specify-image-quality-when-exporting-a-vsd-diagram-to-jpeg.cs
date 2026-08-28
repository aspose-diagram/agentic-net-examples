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

            // Load the VSD diagram (constructor that accepts a file path is used here)
            Diagram diagram = new Diagram("input.vsd");

            // Create ImageSaveOptions for JPEG format
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
            // Set the desired JPEG quality (0‑100). Lower value = higher compression.
            saveOptions.JpegQuality = 75;

            // Export the diagram to a JPEG file using the specified quality setting
            diagram.Save("output.jpg", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
