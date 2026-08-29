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

            // Configure PDF save options to reduce file size
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // Lower JPEG quality for embedded images (range 0‑100)
                JpegQuality = 75,

                // Use Flate compression for text streams (default, set explicitly)
                TextCompression = Aspose.Diagram.Saving.PdfTextCompression.Flate
            };

            // Export the diagram to PDF using the configured options
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
