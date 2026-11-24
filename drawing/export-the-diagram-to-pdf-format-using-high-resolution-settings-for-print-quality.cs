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
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options for high‑resolution (print quality) output.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Explicitly set the target format (required before saving).
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Set DPI to 300 for both horizontal and vertical resolution.
                pdfOptions.HorizontalResolution = 300;
                pdfOptions.VerticalResolution = 300;

                // Use maximum JPEG quality for raster images embedded in the PDF.
                pdfOptions.JpegQuality = 100;

                // Optional: do not export hidden pages.
                pdfOptions.ExportHiddenPage = false;

                // Save the diagram as PDF using the configured options.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Diagram successfully exported to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or saving to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}