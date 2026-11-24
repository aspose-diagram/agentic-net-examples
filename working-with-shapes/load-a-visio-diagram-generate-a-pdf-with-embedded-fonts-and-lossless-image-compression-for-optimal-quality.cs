using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Ensure the required arguments are provided: input Visio file and output PDF file.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify that the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options for high‑quality output.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Explicitly set the format tracker.
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;
            // High resolution for raster images embedded in the PDF.
            pdfOptions.HorizontalResolution = 300;
            pdfOptions.VerticalResolution = 300;
            // Maximum JPEG quality to avoid compression artifacts.
            pdfOptions.JpegQuality = 100;
            // Do not export hidden pages unless required.
            pdfOptions.ExportHiddenPage = false;

            // Note: Aspose.Diagram does not provide a direct option to embed fonts.
            // Fonts are embedded automatically when available; otherwise, a default font can be set.
            // Example (uncomment if needed): pdfOptions.DefaultFont = "Arial";

            // Save the diagram as a PDF using the configured options.
            diagram.Save(outputPath, pdfOptions);
            Console.WriteLine($"PDF successfully saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during loading or saving.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}