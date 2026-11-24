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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: ensure the output folder exists (create if necessary).
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ---------- PDF export with page‑saving progress callback ----------
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.PageSavingCallback = new PageProgressCallback(); // attach callback
                pdfOptions.ExportHiddenPage = false;                         // bool, not BOOL
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;                  // explicit format

                string pdfOutputPath = Path.Combine(outputFolder, "document.pdf");
                diagram.Save(pdfOutputPath, pdfOptions);
                Console.WriteLine($"PDF saved to: {pdfOutputPath}");

                // ---------- Export each page as a separate PNG ----------
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    pngOptions.PageIndex = i;          // zero‑based page index
                    pngOptions.PageCount = 1;          // export only this page
                    pngOptions.ExportHiddenPage = false; // bool, not BOOL

                    string pngPath = Path.Combine(outputFolder, $"page_{i + 1}.png");
                    diagram.Save(pngPath, pngOptions);
                    Console.WriteLine($"Page {i + 1} saved as PNG: {pngPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Implementation of IPageSavingCallback to track PDF page‑saving progress.
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
            args.HasMorePages = true; // use bool, not BOOL
        }
    }
}
