using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputPdfFile]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Determine output PDF path.
        string outputPath;
        if (args.Length >= 2)
        {
            outputPath = args[1];
        }
        else
        {
            // Replace the original extension with .pdf
            outputPath = Path.ChangeExtension(inputPath, ".pdf");
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // -----------------------------------------------------------------
                // Set a global footer that shows page number and total pages.
                // Visio uses '&p' for current page number and '&P' for total pages.
                // -----------------------------------------------------------------
                diagram.HeaderFooter.FooterRight = "Page: &p of &P";

                // Optionally adjust footer margin (in inches) and font appearance.
                diagram.HeaderFooter.FooterMargin.Value = 0.2; // 0.2 inches from bottom edge

                // Configure footer font: Arial, 10pt, bold.
                HeaderFooterFont footerFont = diagram.HeaderFooter.HeaderFooterFont;
                footerFont.FaceName = "Arial";
                footerFont.Height = 10;          // Font size in points
                footerFont.Weight = 700;         // 700 = bold
                footerFont.Underline = BOOL.False; // No underline

                // -----------------------------------------------------------------
                // Prepare PDF save options.
                // -----------------------------------------------------------------
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Explicitly set the format tracker.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                // Do not export hidden pages.
                pdfOptions.ExportHiddenPage = false;
                // Enable text compression to reduce file size.
                pdfOptions.TextCompression = PdfTextCompression.Flate;
                // Set high resolution for raster images embedded in the PDF.
                pdfOptions.HorizontalResolution = 300;
                pdfOptions.VerticalResolution = 300;

                // -----------------------------------------------------------------
                // Save the diagram as PDF.
                // -----------------------------------------------------------------
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Successfully exported PDF to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors and write to standard error.
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}