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

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsd");

            // Create PDF save options with high‑resolution settings for print quality
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
