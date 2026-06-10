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

            // Create PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Ensure hidden pages are exported (default is true, set explicitly for clarity)
            pdfOptions.ExportHiddenPage = true;

            // Compress images by reducing JPEG quality (e.g., 75 out of 100)
            pdfOptions.JpegQuality = 75;

            // Save the diagram as PDF with the specified options
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
