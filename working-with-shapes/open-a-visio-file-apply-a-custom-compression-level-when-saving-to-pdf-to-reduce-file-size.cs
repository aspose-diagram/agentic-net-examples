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

            // Path to the source Visio file
            string sourceFile = "input.vsdx";

            // Path where the compressed PDF will be saved
            string pdfFile = "output.pdf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(sourceFile);

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Use Flate (ZIP) compression for text streams (default is Flate, set explicitly)
            pdfOptions.TextCompression = PdfTextCompression.Flate;

            // Reduce JPEG quality to lower the size of embedded images (0‑100)
            pdfOptions.JpegQuality = 50;

            // Save the diagram as PDF with the specified options
            diagram.Save(pdfFile, pdfOptions);

            // Clean up
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
