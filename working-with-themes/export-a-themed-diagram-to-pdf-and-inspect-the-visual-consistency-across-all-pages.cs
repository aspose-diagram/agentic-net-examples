using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input Visio file and output PDF file paths.
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = "output.pdf";

        // Guard: ensure the input file exists before proceeding.
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
                // Apply a preset theme to each page for visual consistency.
                foreach (Page page in diagram.Pages)
                {
                    // Set the page theme and variant.
                    page.PresetTheme = PresetThemeValue.Bubble;
                    page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                }

                // Create PDF save options and configure required properties.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Explicitly set the format tracker.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                // Set a fallback font for any missing fonts.
                pdfOptions.DefaultFont = "Arial";
                // Do not export hidden pages (property expects a bool, not BOOL).
                pdfOptions.ExportHiddenPage = false;
                // Apply text compression to reduce PDF size.
                pdfOptions.TextCompression = PdfTextCompression.Flate;
                // Assign a custom callback to monitor page saving events.
                pdfOptions.PageSavingCallback = new PdfPageCallback();

                // Export the entire diagram to a PDF file.
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"Diagram successfully exported to PDF: {outputPath}");

                // Inspect visual consistency across all pages.
                int pageCount = diagram.Pages.Count;
                Console.WriteLine($"Diagram contains {pageCount} page(s).");

                foreach (Page page in diagram.Pages)
                {
                    // Count non-deleted shapes on the page.
                    int shapeCount = 0;
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Del == BOOL.False)
                        {
                            shapeCount++;
                        }
                    }

                    // Log page-level information for consistency verification.
                    Console.WriteLine($"Page ID {page.ID}: Shape count = {shapeCount}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Custom implementation of IPageSavingCallback to log page save progress.
    private class PdfPageCallback : IPageSavingCallback
    {
        public void PageStartSaving(PageStartSavingArgs args)
        {
            Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
        }

        public void PageEndSaving(PageEndSavingArgs args)
        {
            Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");
            // Continue processing remaining pages.
        }
    }
}
