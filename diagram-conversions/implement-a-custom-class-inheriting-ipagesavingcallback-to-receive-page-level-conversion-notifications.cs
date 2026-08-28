using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

// Custom callback to receive page‑level notifications during PDF export
class MyPageSavingCallback : IPageSavingCallback
{
    // Called before a page is saved
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}");
    }

    // Called after a page is saved
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}");

        // Example: stop processing after the first page
        // if (args.PageIndex == 0) args.HasMorePages = false;
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (adjust the path as needed)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options and assign the custom callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            pdfOptions.PageSavingCallback = new MyPageSavingCallback();

            // Save the diagram as PDF
            string outputPath = "output.pdf";
            diagram.Save(outputPath, pdfOptions);

            // Clean up resources
            diagram.Dispose();

            Console.WriteLine("PDF export completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}