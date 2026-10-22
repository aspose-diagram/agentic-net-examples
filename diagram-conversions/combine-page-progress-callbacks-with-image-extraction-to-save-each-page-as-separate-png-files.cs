using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output folder path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Ensure output directory exists.
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // ---------- PDF Save with Page Saving Callback (progress tracking) ----------
            // The callback is only valid for PdfSaveOptions, so we perform a temporary PDF save
            // to trigger progress events while still extracting images per page.
            string tempPdfPath = Path.Combine(outputFolder, "temp_progress.pdf");
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new PageProgressCallback();
            // Explicitly set the format (optional, but clarifies intent).
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Perform the temporary PDF save to invoke the callback.
            diagram.Save(tempPdfPath, pdfOptions);

            // Optionally delete the temporary PDF after progress tracking.
            try
            {
                File.Delete(tempPdfPath);
            }
            catch
            {
                // Ignore deletion errors; the file is not critical.
            }

            // ---------- Export each page as a separate PNG ----------
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Configure image save options for a single page.
                ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                imgOptions.PageIndex = i;      // Zero‑based page index.
                imgOptions.PageCount = 1;      // Export only the current page.
                imgOptions.ExportHiddenPage = false; // Do not export hidden pages.

                string pngPath = Path.Combine(outputFolder, $"Page_{i + 1}.png");

                // Save the current page as PNG.
                diagram.Save(pngPath, imgOptions);
                Console.WriteLine($"Saved page {i + 1} to {pngPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Implementation of IPageSavingCallback to report progress during PDF save.
    private class PageProgressCallback : IPageSavingCallback
    {
        // Called before a page starts saving.
        public void PageStartSaving(PageStartSavingArgs args)
        {
            Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}...");
        }

        // Called after a page has been saved.
        public void PageEndSaving(PageEndSavingArgs args)
        {
            Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");
            // Continue processing remaining pages.
            args.HasMorePages = true;
        }
    }
}