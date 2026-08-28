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
            Diagram diagram = new Diagram("input.vsdx");

            // Configure image save options for a multi‑page TIFF
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Tiff);
            // Use LZW compression to reduce file size (default is Lzw, set explicitly for clarity)
            saveOptions.TiffCompression = TiffCompression.Lzw;
            // PageCount defaults to MaxValue, which renders all pages; set explicitly if needed
            // saveOptions.PageCount = int.MaxValue;

            // Save all pages as a single multi‑page TIFF file
            diagram.Save("output.tiff", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
