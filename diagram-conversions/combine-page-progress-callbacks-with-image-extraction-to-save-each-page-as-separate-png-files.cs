using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class PageSavingLogger : IPageSavingCallback
{
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"[Callback] Starting to save page {args.PageIndex + 1} of {args.PageCount}");
    }

    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"[Callback] Finished saving page {args.PageIndex + 1}");
        // Continue processing remaining pages
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string sourcePath = "input.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(sourcePath))
            {
                // -------------------------------------------------
                // 1. Use PDF save with page callback to report progress
                // -------------------------------------------------
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.PageSavingCallback = new PageSavingLogger();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf; // explicit format
                pdfOptions.ExportHiddenPage = false;

                // Save to a temporary PDF to trigger the callbacks
                diagram.Save("temp_progress.pdf", pdfOptions);

                // -------------------------------------------------
                // 2. Export each page as a separate PNG image
                // -------------------------------------------------
                int pageCount = diagram.Pages.Count;
                for (int i = 0; i < pageCount; i++)
                {
                    // Configure image save options for the current page
                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    imgOptions.PageIndex = i;   // zero‑based page index
                    imgOptions.PageCount = 1;   // export only this page
                    imgOptions.Resolution = 300; // optional DPI setting

                    string outputPath = $"Page_{i + 1}.png";
                    diagram.Save(outputPath, imgOptions);
                    Console.WriteLine($"Saved page {i + 1} as PNG: {outputPath}");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}