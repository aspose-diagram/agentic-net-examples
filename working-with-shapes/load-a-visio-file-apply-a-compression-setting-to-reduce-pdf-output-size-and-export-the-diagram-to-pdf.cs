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

            // Paths to the input Visio file and the output PDF file
            string inputPath = "input.vsdx";
            string outputPath = "output.pdf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options with compression to reduce file size
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Use Flate (ZIP) compression for text streams
            pdfOptions.TextCompression = PdfTextCompression.Flate;
            // Explicitly set the save format
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;
            // Set a default font to avoid missing‑font issues
            pdfOptions.DefaultFont = "Arial";

            // Export the diagram to PDF using the configured options
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
