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

            // Load the Visio file
            Diagram diagram = new Diagram("input.vsdx");

            // Configure PDF save options to apply compression
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // Reduce JPEG image quality to lower the PDF size
                JpegQuality = 50
            };

            // Export the diagram to PDF using the compression settings
            diagram.Save("output.pdf", pdfOptions);

            // Clean up
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
