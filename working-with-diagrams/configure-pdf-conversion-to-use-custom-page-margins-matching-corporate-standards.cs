using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputPdfFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory disposal pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (adjust as needed for multi‑page documents).
                Page page = diagram.Pages[0];

                // -----------------------------------------------------------------
                // Set custom page margins (corporate standards) in inches.
                // 1 inch = 72 points, but PrintProps expects inches directly.
                // -----------------------------------------------------------------
                double topMarginInches = 0.5;    // 0.5 inch top margin
                double bottomMarginInches = 0.5; // 0.5 inch bottom margin
                double leftMarginInches = 0.75;  // 0.75 inch left margin
                double rightMarginInches = 0.75; // 0.75 inch right margin

                page.PageSheet.PrintProps.PageTopMargin.Value = topMarginInches;
                page.PageSheet.PrintProps.PageBottomMargin.Value = bottomMarginInches;
                page.PageSheet.PrintProps.PageLeftMargin.Value = leftMarginInches;
                page.PageSheet.PrintProps.PageRightMargin.Value = rightMarginInches;

                // -----------------------------------------------------------------
                // Configure PDF save options.
                // -----------------------------------------------------------------
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.ExportHiddenPage = false;                 // Do not export hidden pages (bool, not BOOL)
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;          // Explicitly set the format tracker
                pdfOptions.PageSavingCallback = new PdfPageSavingCallback(); // Track progress

                // Save the diagram as PDF using the configured options.
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine($"PDF successfully saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

// ---------------------------------------------------------------------
// Implementation of IPageSavingCallback to monitor PDF page saving.
// ---------------------------------------------------------------------
class PdfPageSavingCallback : IPageSavingCallback
{
    // Called before a page starts saving.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}");
    }

    // Called after a page has finished saving.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished saving page {args.PageIndex + 1}");
        // Continue processing remaining pages (default behavior).
    }
}