using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportDiagramToPdf
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Configure PDF save options for high‑resolution (print quality) output
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // Set DPI to 300 for both horizontal and vertical resolutions
                HorizontalResolution = 300,
                VerticalResolution = 300,

                // Optional: ensure the PDF conforms to a common standard (e.g., PDF/A‑1b)
                // Compliance = PdfCompliance.PdfA1b
            };

            // Save the diagram as a PDF using the configured options
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
