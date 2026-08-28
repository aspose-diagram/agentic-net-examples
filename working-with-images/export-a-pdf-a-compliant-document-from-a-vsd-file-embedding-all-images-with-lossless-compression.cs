using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportPdfA
{
    static void Main()
    {
        try
        {

            // Path to the source VSD file
            string inputVsdPath = "input.vsd";

            // Path where the PDF/A file will be saved
            string outputPdfPath = "output.pdf";

            // Load the Visio diagram from file (uses the Diagram(string) constructor)
            Diagram diagram = new Diagram(inputVsdPath);

            // Configure PDF save options for PDF/A compliance
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // Set PDF/A-1b compliance (use PdfA1a for PDF/A-1a if required)
                Compliance = PdfCompliance.PdfA1b,

                // Use maximum JPEG quality to avoid lossy compression;
                // images that are not JPEG will be embedded losslessly.
                JpegQuality = 100,

                // Ensure the whole page is captured
                EnlargePage = true
            };

            // Save the diagram as a PDF/A compliant document with the specified options
            diagram.Save(outputPdfPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
