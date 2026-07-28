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

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Path for the resulting PDF file
            string outputPath = "output.pdf";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Export hidden pages (required for archival purposes)
                pdfOptions.ExportHiddenPage = true;

                // Compress images by reducing JPEG quality (adjust as needed)
                pdfOptions.JpegQuality = 75; // Value between 1 and 100

                // Optional: compress text streams using Flate compression
                pdfOptions.TextCompression = PdfTextCompression.Flate;

                // Explicitly set the save format (PDF)
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Save the diagram as PDF with the configured options
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine("PDF saved successfully with image compression and hidden pages exported.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
