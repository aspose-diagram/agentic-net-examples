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

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Set a default font to be used when a required font is missing.
            // Aspose.Diagram does not provide a direct option to embed fonts,
            // but specifying a default font ensures that text is rendered correctly.
            pdfOptions.DefaultFont = "Arial";

            // Optional: set PDF/A compliance if needed
            // pdfOptions.Compliance = PdfCompliance.PdfA1b;

            // Save the diagram as PDF with the configured options
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
