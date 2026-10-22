using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output PDF file path
        string outputPath = "output.pdf";

        try
        {
            // Load the Visio diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Export hidden pages for archival purposes
                pdfOptions.ExportHiddenPage = true; // BOOL replaced with native bool

                // Apply Flate compression to reduce file size (text compression)
                pdfOptions.TextCompression = PdfTextCompression.Flate;

                // Explicitly set the save format
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Optional: set a default font to avoid missing font warnings
                pdfOptions.DefaultFont = "Arial";

                // Save the diagram as PDF with the configured options
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"Diagram successfully saved to PDF: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
