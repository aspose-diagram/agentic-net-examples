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

            // Create image save options for TIFF format
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Tiff);

            // Set a high resolution (e.g., 300 DPI) to get a high‑resolution image
            saveOptions.Resolution = 300;

            // Preserve original font styles by specifying a default font that exists on the system.
            // If the original fonts are installed, they will be used automatically.
            saveOptions.DefaultFont = "Arial";

            // Optional: set TIFF compression (LZW is lossless and keeps quality)
            saveOptions.TiffCompression = TiffCompression.Lzw;

            // Save the diagram as a high‑resolution TIFF image
            diagram.Save("output.tiff", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
