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

            // Load the Visio file
            Diagram diagram = new Diagram("input.vsdx");

            // Configure PDF save options with custom compression
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Use Flate (ZIP) compression for text streams
            pdfOptions.TextCompression = PdfTextCompression.Flate;
            // Reduce JPEG quality for images embedded in the PDF (0-100)
            pdfOptions.JpegQuality = 50;

            // Save the diagram as a PDF using the configured options
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
