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
            string inputPath = "input.vsdx";

            // Desired PDF output path
            string outputPath = "output.pdf";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options with compression
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Use Flate compression for text streams (reduces size while preserving quality)
                pdfOptions.TextCompression = PdfTextCompression.Flate;

                // Adjust JPEG quality for images inside the PDF (optional, maintains visual quality)
                pdfOptions.JpegQuality = 90;

                // Save the diagram as PDF with the specified options
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"PDF exported successfully to: {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
