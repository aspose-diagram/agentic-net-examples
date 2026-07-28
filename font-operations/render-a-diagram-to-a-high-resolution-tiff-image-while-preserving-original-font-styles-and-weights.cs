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

            // Configure image save options for high‑resolution TIFF
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Tiff);
            saveOptions.Resolution = 300;               // Set DPI (e.g., 300 for high quality)
            saveOptions.TiffCompression = TiffCompression.Lzw; // Optional compression
            // Do NOT set DefaultFont to preserve original font styles and weights

            // Render and save the diagram as a TIFF image
            diagram.Save("output.tiff", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
