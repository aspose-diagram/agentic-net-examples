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
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Determine the output PDF path (same folder, same name with .pdf extension).
        string outputPdfPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + ".pdf");

        try
        {
            // ------------------------------------------------------------
            // Configure the font folder so Aspose.Diagram can locate TrueType fonts.
            // ------------------------------------------------------------
            string systemFontFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
            FontConfigs.SetFontFolder(systemFontFolder, true);

            // ------------------------------------------------------------
            // Load the Visio diagram inside a using block.
            // ------------------------------------------------------------
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Prepare PDF save options.
                // Set a default fallback font and export hidden pages.
                // ------------------------------------------------------------
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";
                pdfOptions.ExportHiddenPage = true; // Changed from BOOL.True to bool

                // ------------------------------------------------------------
                // Save the diagram to PDF using the configured options.
                // ------------------------------------------------------------
                diagram.Save(outputPdfPath, pdfOptions);
            }

            // ------------------------------------------------------------
            // Verify that fonts are embedded in the resulting PDF.
            // Load the PDF with Aspose.Pdf (fully qualified to avoid ambiguity).
            // ------------------------------------------------------------
            Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(outputPdfPath);

            int embeddedFontCount = 0;
            if (pdfDocument.Pages.Count > 0)
            {
                Aspose.Pdf.Page firstPage = pdfDocument.Pages[1];
                if (firstPage.Resources != null && firstPage.Resources.Fonts != null)
                {
                    embeddedFontCount = firstPage.Resources.Fonts.Count;
                }
            }

            // ------------------------------------------------------------
            // Count the fonts used in the original diagram.
            // ------------------------------------------------------------
            int diagramFontCount = 0;
            using (Diagram diagramForCount = new Diagram(inputPath))
            {
                // Explicit enumeration as required by the rules.
                foreach (Font font in diagramForCount.Fonts)
                {
                    diagramFontCount++;
                }
            }

            // ------------------------------------------------------------
            // Compare counts and report the result.
            // ------------------------------------------------------------
            if (embeddedFontCount >= diagramFontCount && embeddedFontCount > 0)
            {
                Console.WriteLine($"Success: All {diagramFontCount} diagram fonts are embedded in the PDF ({embeddedFontCount} fonts found).");
            }
            else
            {
                Console.Error.WriteLine($"Warning: Expected {diagramFontCount} embedded fonts, but found {embeddedFontCount} in the PDF.");
            }
        }
        catch (Exception ex)
        {
            // ------------------------------------------------------------
            // Report any errors that occurred during processing.
            // ------------------------------------------------------------
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}
