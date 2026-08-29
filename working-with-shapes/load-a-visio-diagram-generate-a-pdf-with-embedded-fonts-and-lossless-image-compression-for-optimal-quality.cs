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

            // Paths to the source Visio file and the target PDF file
            string inputPath = "input.vsdx";
            string outputPath = "output.pdf";

            // Configure the folder that contains system fonts (adjust the path as needed)
            FontConfigs.SetFontFolder(@"C:\Windows\Fonts", true);

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";                     // Fallback font if a required font is missing
            pdfOptions.TextCompression = PdfTextCompression.Flate; // Lossless compression for PDF content streams

            // Save the diagram as a PDF with embedded fonts and lossless compression
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
