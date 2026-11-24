using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Output PDF file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "output.pdf";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the diagram inside a using block as required
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Apply a preset theme to each page for visual consistency
                // ------------------------------------------------------------
                foreach (Page page in diagram.Pages)
                {
                    // Set a known theme (Bubble) and a variant
                    page.PresetTheme = PresetThemeValue.Bubble;
                    page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                }

                // ------------------------------------------------------------
                // Perform auto‑spacing on each page to ensure layout uniformity
                // ------------------------------------------------------------
                AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions();
                autoSpaceOptions.DistanceInHorizontal = 0.5;
                autoSpaceOptions.DistanceInVertical = 0.5;

                foreach (Page page in diagram.Pages)
                {
                    // Auto‑space shapes on the current page
                    page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);
                }

                // ------------------------------------------------------------
                // Inspect visual consistency: log page count, shape count, size
                // ------------------------------------------------------------
                int pageCount = diagram.Pages.Count;
                Console.WriteLine($"Diagram contains {pageCount} page(s).");

                for (int i = 0; i < pageCount; i++)
                {
                    Page page = diagram.Pages[i];
                    int shapeCount = page.Shapes.Count;

                    // Retrieve page dimensions (in inches)
                    double width = page.PageSheet.PageProps.PageWidth.Value;
                    double height = page.PageSheet.PageProps.PageHeight.Value;

                    Console.WriteLine($"Page {i + 1}: Shapes = {shapeCount}, Size = {width} x {height} inches");
                }

                // ------------------------------------------------------------
                // Configure PDF save options with a page‑saving callback
                // ------------------------------------------------------------
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;               // Explicit format tracking
                pdfOptions.ExportHiddenPage = false;                     // Do not export hidden pages
                pdfOptions.TextCompression = PdfTextCompression.Flate;   // Reduce PDF size
                pdfOptions.PageSavingCallback = new PdfPageCallback();   // Attach callback

                // ------------------------------------------------------------
                // Save the diagram as PDF
                // ------------------------------------------------------------
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"PDF successfully saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

// ------------------------------------------------------------------------
// Implementation of IPageSavingCallback to log page start/end events
// ------------------------------------------------------------------------
class PdfPageCallback : IPageSavingCallback
{
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
    }

    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");
        // Example: stop after first page (uncomment to use)
        // if (args.PageIndex == 0) args.HasMorePages = false;
    }
}