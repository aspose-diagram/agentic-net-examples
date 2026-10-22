using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output PDF file path.
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
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Configure global font settings.
            // Add the system fonts folder so that fonts used in the diagram can be found.
            string systemFontsPath = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
            if (Directory.Exists(systemFontsPath))
            {
                // Set the font folder (recursive search).
                FontConfigs.SetFontFolder(systemFontsPath, true);
            }

            // Set a default fallback font.
            FontConfigs.DefaultFontName = "Arial";

            // Load the Visio diagram inside a using block (required pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options for high quality output.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Explicitly set the format tracker.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Do not export hidden pages. Use native bool, not BOOL enum.
                pdfOptions.ExportHiddenPage = false; // corrected type

                // Use PDF/A-1b compliance for archival quality.
                pdfOptions.Compliance = PdfCompliance.PdfA1b;

                // Apply Flate compression to text streams (lossless).
                pdfOptions.TextCompression = PdfTextCompression.Flate;

                // Set a default font to ensure text is rendered correctly.
                pdfOptions.DefaultFont = "Arial";

                // Save the diagram as a PDF with the configured options.
                diagram.Save(outputPath, pdfOptions);
            }
        }
        catch (Exception ex)
        {
            // Log any unexpected errors.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
