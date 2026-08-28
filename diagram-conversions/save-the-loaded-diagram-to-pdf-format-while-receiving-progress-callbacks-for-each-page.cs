using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

// Callback implementation to receive progress for each page
class PageSavingCallback : IPageSavingCallback
{
    // Called when a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Start saving page {args.PageIndex}");
    }

    // Called when a page finishes saving
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished saving page {args.PageIndex}");
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Configure PDF save options and attach the progress callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                PageSavingCallback = new PageSavingCallback()
            };

            // Save the diagram to PDF using the configured options
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}