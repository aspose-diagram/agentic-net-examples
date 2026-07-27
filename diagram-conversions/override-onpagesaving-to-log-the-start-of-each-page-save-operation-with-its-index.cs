using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class MyPageSavingCallback : IPageSavingCallback
{
    // Called when a page starts saving.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Log the start of the page save operation with its zero‑based index.
        Console.WriteLine($"Page {args.PageIndex + 1}/{args.PageCount} start saving.");
    }

    // Called when a page finishes saving.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // No action needed for this example.
    }
}

public class DiagramExport
{
    public static void Main()
    {
        try
        {

            // Load an existing diagram.
            Diagram diagram = new Diagram("input.vsdx");

            // Configure PDF save options and attach the callback.
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                PageSavingCallback = new MyPageSavingCallback()
            };

            // Save the diagram to PDF; the callback will log each page start.
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}