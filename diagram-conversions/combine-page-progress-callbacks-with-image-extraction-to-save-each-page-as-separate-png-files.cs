using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class PageSavingCallback : IPageSavingCallback
{
    private readonly Diagram _diagram;

    public PageSavingCallback(Diagram diagram)
    {
        _diagram = diagram;
    }

    // Called before a page starts saving (PDF rendering)
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
    }

    // Called after a page has been saved (PDF rendering)
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished saving page {args.PageIndex + 1}.");

        // Export the same page as a PNG image
        string pngPath = $"Page_{args.PageIndex + 1}.png";
        ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
        imgOptions.PageIndex = args.PageIndex; // zero‑based page index
        _diagram.Save(pngPath, imgOptions);
        Console.WriteLine($"Exported PNG: {pngPath}");
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            using (Diagram diagram = new Diagram("input.vsdx"))
            {
                // Configure PDF save options with the page‑saving callback
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                pdfOptions.DefaultFont = "Arial";
                pdfOptions.PageSavingCallback = new PageSavingCallback(diagram);

                // Save as PDF to trigger the callbacks; PNGs are generated inside the callback
                diagram.Save("output.pdf", pdfOptions);
            }

            Console.WriteLine("Processing completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}