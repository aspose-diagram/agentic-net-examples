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

            // Paths for input Visio file and output PDF
            string inputPath = "input.vsdx";
            string outputPath = "output.pdf";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options with text compression to reduce file size
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.TextCompression = PdfTextCompression.Flate;

                // Save the diagram as PDF using the configured options
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine("Diagram exported to PDF with compression.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
