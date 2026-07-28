using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class EmbedMissingFontsToPdf
{
    static void Main()
    {
        try
        {

            // Load the source Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram(@"C:\Input\sample.vsdx");

            // Create PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Specify a default font to be used when the original font is missing.
            // This ensures that Unicode characters are rendered correctly in the PDF.
            pdfOptions.DefaultFont = "Arial Unicode MS";

            // Save the diagram as PDF using the configured options
            diagram.Save(@"C:\Output\sample.pdf", pdfOptions);

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
