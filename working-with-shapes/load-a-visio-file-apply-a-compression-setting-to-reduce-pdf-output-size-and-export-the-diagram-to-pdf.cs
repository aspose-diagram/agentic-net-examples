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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options with compression to reduce file size.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Set the format tracker explicitly.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Apply Flate (ZIP) compression to textual content.
                pdfOptions.TextCompression = PdfTextCompression.Flate;

                // Optional: set a default fallback font.
                pdfOptions.DefaultFont = "Arial";

                // Optional: reduce image quality for embedded raster images.
                pdfOptions.JpegQuality = 75; // 0‑100, lower = higher compression.

                // Save the diagram as a PDF using the configured options.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"PDF successfully saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}