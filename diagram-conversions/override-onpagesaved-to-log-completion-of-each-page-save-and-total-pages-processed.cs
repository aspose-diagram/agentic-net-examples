using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class PageSaveLogger : IPageSavingCallback
{
    // Called when a page starts to be saved.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Log the start of the page save operation.
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
    }

    // Called when a page has finished saving.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // Log the completion of the current page.
        Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");

        // When the last page is saved, log the total number of pages processed.
        if (!args.HasMorePages)
        {
            Console.WriteLine($"All {args.PageCount} pages have been saved.");
        }
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path).
            Diagram diagram = new Diagram("input.vsdx");

            // Set up PDF save options and attach the page‑saving callback.
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                PageSavingCallback = new PageSaveLogger()
            };

            // Save the diagram to PDF; the callback will log progress.
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}