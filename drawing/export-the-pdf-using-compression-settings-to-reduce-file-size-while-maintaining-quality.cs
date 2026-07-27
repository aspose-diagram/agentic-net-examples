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

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Set up PDF save options with compression
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // Compress text and other content streams (Flate = ZIP compression)
                TextCompression = PdfTextCompression.Flate,
                // Reduce JPEG image quality to lower file size while keeping acceptable visual quality
                JpegQuality = 80,
                // Use default PDF compliance (PDF 1.5)
                Compliance = PdfCompliance.Pdf15
            };

            // Save the diagram as a compressed PDF
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
