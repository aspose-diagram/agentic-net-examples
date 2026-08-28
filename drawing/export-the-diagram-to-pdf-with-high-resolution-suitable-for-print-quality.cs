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

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Configure PDF save options for high‑resolution (print quality) output
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.HorizontalResolution = 300; // 300 DPI horizontal
            pdfOptions.VerticalResolution = 300;   // 300 DPI vertical (if supported)

            // Save the diagram as a PDF using the configured options
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
