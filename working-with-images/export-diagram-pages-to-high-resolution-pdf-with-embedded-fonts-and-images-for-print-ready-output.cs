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

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Path for the exported PDF
            string outputPath = "output.pdf";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options for print‑ready output
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Use PDF/A‑1b compliance (suitable for printing)
            pdfOptions.Compliance = PdfCompliance.PdfA1b;

            // Specify a fallback font for any missing fonts in the diagram
            pdfOptions.DefaultFont = "Arial";

            // Do not include hidden pages in the print PDF
            pdfOptions.ExportHiddenPage = false;

            // Ensure all pages are exported
            pdfOptions.PageCount = diagram.Pages.Count;

            // Save the diagram as a high‑resolution PDF
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram successfully exported to PDF: {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
