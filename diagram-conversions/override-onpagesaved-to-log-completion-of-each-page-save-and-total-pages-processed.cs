using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class MyPageSavingCallback : IPageSavingCallback
{
    // Called when a page starts saving; not needed for logging.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // No implementation required.
    }

    // Called when a page finishes saving.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // Log the completion of the current page.
        Console.WriteLine($"Page {args.PageIndex + 1} of {args.PageCount} saved.");

        // If this was the last page, log the total pages processed.
        if (!args.HasMorePages)
        {
            Console.WriteLine($"All {args.PageCount} pages have been processed.");
        }
    }
}

public class Program
{
    public static void Main()
    {
        try
        {

            // Load an existing diagram (lifecycle rule: load).
            Diagram diagram = new Diagram("input.vsdx");

            // Set up PDF save options and attach the page‑saving callback.
            PdfSaveOptions saveOptions = new PdfSaveOptions
            {
                PageSavingCallback = new MyPageSavingCallback()
            };

            // Save the diagram to PDF (lifecycle rule: save).
            diagram.Save("output.pdf", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}