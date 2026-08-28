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

            // Path to the source Visio diagram
            string sourceFile = "input.vsdx";

            // Path for the high‑resolution TIFF output
            string outputFile = "output.tiff";

            // Load the diagram (create/load lifecycle)
            Diagram diagram = new Diagram(sourceFile);

            // Configure image save options for TIFF
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Tiff)
            {
                // Set a high DPI resolution (e.g., 300 DPI)
                Resolution = 300,

                // Preserve original fonts (default behavior). 
                // If needed, you can specify a fallback font:
                // DefaultFont = "Arial",

                // Optional: use LZW compression for TIFF
                TiffCompression = TiffCompression.Lzw
            };

            // Render and save the diagram as a TIFF image (save lifecycle)
            diagram.Save(outputFile, saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
