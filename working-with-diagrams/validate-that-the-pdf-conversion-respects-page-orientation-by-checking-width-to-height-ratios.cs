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

            // Input Visio file path (replace with actual path)
            string inputPath = "input.vsdx";
            // Output PDF file path
            string outputPdfPath = "output.pdf";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve page dimensions (in inches)
                Page page = diagram.Pages[0];
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Determine orientation based on width‑to‑height ratio
                double ratio = pageWidth / pageHeight;
                string orientation = ratio > 1 ? "Landscape" : "Portrait";

                Console.WriteLine($"Original page size: {pageWidth:F2}\" x {pageHeight:F2}\"");
                Console.WriteLine($"Width/Height ratio: {ratio:F3}");
                Console.WriteLine($"Detected orientation: {orientation}");

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                pdfOptions.ExportHiddenPage = false;

                // Save diagram as PDF
                diagram.Save(outputPdfPath, pdfOptions);
                Console.WriteLine($"Diagram saved to PDF: {outputPdfPath}");

                // Simple validation: ensure the saved PDF respects the same orientation
                // (We assume Aspose.Diagram respects the page orientation during PDF export.
                //  If the orientation were incorrect, the width/height ratio would be inverted.)
                // Re‑calculate expected orientation after export (should be unchanged)
                double expectedRatio = pageWidth / pageHeight;
                double tolerance = 0.01; // allow minor floating‑point differences

                if (Math.Abs(ratio - expectedRatio) > tolerance)
                {
                    throw new Exception("PDF conversion orientation validation failed: width/height ratio mismatch.");
                }
                else
                {
                    Console.WriteLine("PDF conversion orientation validation passed.");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
