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

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options for high‑resolution, print‑quality output.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Set the format tracker explicitly.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // High DPI settings (300 DPI) for raster content inside the PDF.
                pdfOptions.HorizontalResolution = 300;
                pdfOptions.VerticalResolution = 300;

                // Maximum JPEG quality for embedded raster images.
                pdfOptions.JpegQuality = 100;

                // Do not export hidden pages unless explicitly required.
                pdfOptions.ExportHiddenPage = false;

                // Optional: set a default font to avoid missing‑font warnings.
                pdfOptions.DefaultFont = "Arial";

                // Save the diagram as a PDF using the configured options.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Diagram successfully exported to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"Error during PDF export: {ex.Message}");
        }
    }
}