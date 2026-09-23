using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CustomPageSavingCallback : IPageSavingCallback
{
    private int _savedPages = 0;

    public int SavedPagesCount => _savedPages;

    // Called before a page starts saving (not used for logging here)
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // No action needed before page save
    }

    // Called after a page has been saved
    public void PageEndSaving(PageEndSavingArgs args)
    {
        _savedPages++;
        Console.WriteLine($"Page {args.PageIndex + 1} of {args.PageCount} saved.");
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with actual file path)
            using (Diagram diagram = new Diagram("input.vsdx"))
            {
                // Configure PDF save options with the custom callback
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                CustomPageSavingCallback callback = new CustomPageSavingCallback();
                pdfOptions.PageSavingCallback = callback;

                // Save the diagram to PDF (replace with desired output path)
                diagram.Save("output.pdf", pdfOptions);

                // Log total pages processed after saving completes
                Console.WriteLine($"Total pages processed: {callback.SavedPagesCount}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}