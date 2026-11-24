using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.pdf";

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (ensures proper disposal)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Explicitly set the format tracker
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Compress textual elements using Flate (ZIP) compression
                pdfOptions.TextCompression = PdfTextCompression.Flate;

                // Set high resolution and maximum JPEG quality for raster images
                pdfOptions.HorizontalResolution = 300;
                pdfOptions.VerticalResolution = 300;
                pdfOptions.JpegQuality = 100;

                // Export the diagram to PDF with the configured options
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine($"PDF successfully saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}