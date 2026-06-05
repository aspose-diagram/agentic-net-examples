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

            // Load the Visio diagram
            Diagram diagram = new Diagram(@"input.vsdx");

            // Configure PDF save options with compression settings
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Use Flate (ZIP) compression for all content streams except images
            pdfOptions.TextCompression = PdfTextCompression.Flate;

            // Set JPEG quality to balance file size and image quality (e.g., 80%)
            pdfOptions.JpegQuality = 80;

            // Keep default PDF compliance (PDF 1.5) or set explicitly
            pdfOptions.Compliance = PdfCompliance.Pdf15;

            // Save the diagram as a compressed PDF
            diagram.Save(@"output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
