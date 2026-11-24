using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path and output PDF file path as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Define corporate standard margins (e.g., 0.5 inch = 36 points).
            double marginPoints = 36.0;

            // Apply margins to each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Set top, bottom, left, and right margins via PrintProps.
                page.PageSheet.PrintProps.PageTopMargin.Value = marginPoints;
                page.PageSheet.PrintProps.PageBottomMargin.Value = marginPoints;
                page.PageSheet.PrintProps.PageLeftMargin.Value = marginPoints;
                page.PageSheet.PrintProps.PageRightMargin.Value = marginPoints;
            }

            // Configure PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.ExportHiddenPage = false; // Do not export hidden pages.
            // Assign a custom page saving callback to track conversion progress.
            pdfOptions.PageSavingCallback = new CustomPageSavingCallback();

            // Save the diagram as PDF with the configured options.
            diagram.Save(outputPath, pdfOptions);
            Console.WriteLine($"PDF saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}

// Custom implementation of IPageSavingCallback to monitor PDF page saving events.
class CustomPageSavingCallback : IPageSavingCallback
{
    // Called before a page starts saving.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
    }

    // Called after a page has finished saving.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");
        // Example: stop further processing after the first page (if needed).
        // args.HasMorePages = false;
    }
}