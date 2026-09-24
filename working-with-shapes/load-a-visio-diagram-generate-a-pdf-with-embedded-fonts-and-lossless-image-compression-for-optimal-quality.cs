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

            // Paths to the source Visio file and the target PDF file
            string inputPath = "input.vsdx";
            string outputPath = "output.pdf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Fallback font if any diagram fonts are missing
            pdfOptions.DefaultFont = "Arial";
            // Use PDF/A-1b compliance which embeds fonts and ensures high‑quality output
            pdfOptions.Compliance = PdfCompliance.PdfA1b;
            // Do not export hidden pages (optional, but keeps output clean)
            pdfOptions.ExportHiddenPage = false;

            // Save the diagram as a PDF with the specified options
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
