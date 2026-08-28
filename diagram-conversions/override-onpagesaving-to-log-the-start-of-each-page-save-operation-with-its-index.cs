using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class PageSavingLogger : IPageSavingCallback
{
    // Called when a page starts to be saved.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Log the start of the page save operation with its zero‑based index.
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
    }

    // Called when a page finishes saving. Not needed for this task, but required by the interface.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // No action required; method must be present to satisfy the interface.
    }
}

// Example of assigning the callback to a save operation (loading/saving code is assumed to be handled elsewhere):
// var diagram = new Diagram("input.vsdx");
// var pdfOptions = new PdfSaveOptions();
// pdfOptions.PageSavingCallback = new PageSavingLogger();
// diagram.Save("output.pdf", pdfOptions);

class Program
{
    static void Main(string[] args)
    {
        try
        {

            var obj = new PageSavingLogger();
            obj.PageStartSaving(null);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
