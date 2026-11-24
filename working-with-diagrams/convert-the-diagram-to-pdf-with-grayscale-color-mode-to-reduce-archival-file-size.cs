using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Set default font to ensure proper text rendering
                pdfOptions.DefaultFont = "Arial";

                // Export hidden pages is not needed for archival PDF
                pdfOptions.ExportHiddenPage = false;

                // Explicitly set the save format (required by Aspose.Diagram)
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // NOTE: Aspose.Diagram does not provide a direct grayscale mode for PDF export.
                // The closest alternative is to export the diagram as PDF using default options.
                // If grayscale conversion is essential, it would need to be performed in a
                // post‑processing step using a PDF library that supports color space conversion.

                // Save the diagram as PDF with the configured options
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"Diagram successfully saved to PDF: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}