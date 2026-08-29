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

            // Desired output PDF file path
            string outputPath = "output.pdf";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Set a fallback font to avoid missing glyphs
                pdfOptions.DefaultFont = "Arial";
                // Apply ZIP (Flate) compression to text streams in the PDF
                pdfOptions.TextCompression = PdfTextCompression.Flate;

                // Save the diagram as a PDF with the specified options
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine("PDF generation completed successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
