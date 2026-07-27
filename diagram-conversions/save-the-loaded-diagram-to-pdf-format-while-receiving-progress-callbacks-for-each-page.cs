using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class MyPageSavingCallback : IPageSavingCallback
{
    // Called before each page is saved
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}");
    }

    // Called after each page is saved
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}");
        // Continue processing remaining pages (do not set args.HasMorePages = false)
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source Visio file and the target PDF file
            string inputPath = "input.vsdx";
            string outputPath = "output.pdf";

            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options and attach the page‑saving callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";               // Fallback font for missing characters
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;     // Explicitly set the format tracker
            pdfOptions.PageSavingCallback = new MyPageSavingCallback();

            // Save the diagram to PDF, invoking the callback for each page
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}