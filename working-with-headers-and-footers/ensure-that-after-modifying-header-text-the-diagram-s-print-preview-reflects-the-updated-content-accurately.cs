using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Determine preview output path (PDF format).
        string previewPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_preview.pdf");

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ----- Modify header text and formatting -----
                // Set header strings (left, center, right).
                diagram.HeaderFooter.HeaderLeft = "Company Confidential";
                diagram.HeaderFooter.HeaderCenter = "Report Title";
                diagram.HeaderFooter.HeaderRight = "Date: &d";

                // Adjust header margin (in inches).
                diagram.HeaderFooter.HeaderMargin.Value = 0.5;

                // Set header/footer text color.
                diagram.HeaderFooter.HeaderFooterColor = Color.Black;

                // Configure header/footer font.
                HeaderFooterFont headerFont = diagram.HeaderFooter.HeaderFooterFont;
                headerFont.FaceName = "Arial";          // Font face.
                headerFont.Height = 12;                 // Font size (points).
                headerFont.Weight = 700;                // Bold weight (700 = bold).
                headerFont.Italic = BOOL.False;         // Italic style (BOOL).
                headerFont.Underline = BOOL.False;      // Underline flag (BOOL).

                // ----- Export a PDF as a print‑preview substitute -----
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.ExportHiddenPage = false;    // Do not export hidden pages.
                pdfOptions.HorizontalResolution = 300; // High‑resolution DPI.
                pdfOptions.VerticalResolution = 300;
                pdfOptions.JpegQuality = 100;           // Maximum image quality.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf; // Explicit format tracking.

                // Save the diagram to PDF (acts as print preview).
                diagram.Save(previewPath, pdfOptions);

                Console.WriteLine($"Print preview PDF saved to: {previewPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}