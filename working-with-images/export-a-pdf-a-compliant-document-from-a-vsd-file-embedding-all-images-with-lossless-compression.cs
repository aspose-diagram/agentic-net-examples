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

            // Load the VSD file
            Diagram diagram = new Diagram("input.vsd");

            // Configure PDF save options for PDF/A compliance and lossless image embedding
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // PDF/A-1b compliance
                Compliance = PdfCompliance.PdfA1b,
                // Set JPEG quality to maximum to avoid lossy compression
                JpegQuality = 100
            };

            // Save the diagram as a PDF/A compliant document
            diagram.Save("output.pdf", pdfOptions);

            // Release resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
