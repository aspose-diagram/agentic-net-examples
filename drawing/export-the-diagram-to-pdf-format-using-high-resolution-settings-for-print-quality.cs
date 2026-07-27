using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportDiagramToPdf
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Set high‑resolution options for print‑quality PDF output
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.HorizontalResolution = 300; // 300 DPI horizontal
            pdfOptions.VerticalResolution = 300;   // 300 DPI vertical

            // Save the diagram as a PDF using the configured options
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
