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

            // Load the source VSD file
            Diagram diagram = new Diagram("input.vsd");

            // Set up PDF/A save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.Compliance = PdfCompliance.PdfA1b; // PDF/A-1b compliance
            pdfOptions.JpegQuality = 100;                 // Preserve maximum image quality (lossless for non‑JPEG images)

            // Export the diagram to a PDF/A compliant file with embedded images
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
