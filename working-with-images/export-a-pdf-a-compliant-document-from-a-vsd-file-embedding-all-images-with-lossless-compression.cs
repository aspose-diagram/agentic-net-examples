using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (VSD format)
        string inputPath = "input.vsd";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output PDF/A file path
        string outputPath = "output.pdf";

        // Guard: ensure the input file exists
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
                // Configure PDF/A export options
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // Set PDF/A compliance (PDF/A-1b)
                    Compliance = PdfCompliance.PdfA1b,
                    // Use high resolution for raster images
                    HorizontalResolution = 300,
                    VerticalResolution = 300,
                    // Set JPEG quality to maximum (lossless as possible)
                    JpegQuality = 100,
                    // Ensure the output format is PDF
                    SaveFormat = SaveFileFormat.Pdf
                };

                // Save the diagram as PDF/A
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"PDF/A document saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}