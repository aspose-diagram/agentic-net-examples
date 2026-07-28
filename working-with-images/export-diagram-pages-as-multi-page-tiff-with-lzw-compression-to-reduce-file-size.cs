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

            // Load the Visio diagram from file
            Diagram diagram = new Diagram("input.vsdx");

            // Set up image save options for a multi‑page TIFF with LZW compression
            ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Tiff);
            options.TiffCompression = TiffCompression.Lzw;   // Apply LZW compression
            options.PageCount = int.MaxValue;                // Render all pages (default)
            options.PageIndex = 0;                           // Start from the first page

            // Save all pages as a single multi‑page TIFF file
            diagram.Save("output.tiff", options);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
