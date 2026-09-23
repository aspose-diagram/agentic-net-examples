using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the source Visio diagram (VSDX, VDX, etc.)
            string inputPath = "input.vsdx";

            // Path for the exported PDF file
            string outputPath = "output.pdf";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options for high‑quality print output
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";                     // Fallback font
            pdfOptions.Compliance = PdfCompliance.PdfA1b;        // PDF/A‑1b compliance for reliable printing
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;          // Explicitly set the format

            // Save the diagram as a PDF using the configured options
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine("Diagram exported to PDF successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
