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

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Configure image save options for multi‑page TIFF with LZW compression
            ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Tiff);
            options.TiffCompression = TiffCompression.Lzw; // LZW compression
            // options.PageCount = int.MaxValue; // optional: render all pages (default)

            // Save the diagram as a multi‑page TIFF file
            diagram.Save("output.tiff", options);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
