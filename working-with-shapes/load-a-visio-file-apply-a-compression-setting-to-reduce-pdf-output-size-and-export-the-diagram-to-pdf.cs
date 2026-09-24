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

            // Path for the resulting PDF file
            string outputPath = "output.pdf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options with compression to reduce file size
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Apply Flate compression to PDF text streams
            pdfOptions.TextCompression = PdfTextCompression.Flate;
            // Explicitly set the save format
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;
            // Optional: specify a default font to avoid missing font warnings
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
