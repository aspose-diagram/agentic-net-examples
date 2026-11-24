using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputPdfFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Export hidden pages for archival purposes.
            pdfOptions.ExportHiddenPage = true;

            // Compress images: set high JPEG quality and DPI.
            pdfOptions.JpegQuality = 100;               // 0‑100, 100 = best quality.
            pdfOptions.HorizontalResolution = 300;      // DPI
            pdfOptions.VerticalResolution = 300;        // DPI

            // Apply Flate compression to PDF text streams to reduce file size.
            pdfOptions.TextCompression = PdfTextCompression.Flate;

            // Explicitly set the format tracker (required by the API).
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Save the diagram as a PDF with the configured options.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"PDF saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}