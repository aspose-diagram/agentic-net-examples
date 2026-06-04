using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class PageCountValidator : IPageSavingCallback
{
    private readonly int _expectedPageCount;

    public PageCountValidator(int expectedPageCount)
    {
        _expectedPageCount = expectedPageCount;
    }

    // Called before a page is saved
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Validate total page count reported by the callback
        if (args.PageCount != _expectedPageCount)
        {
            throw new Exception($"Page count mismatch at start: callback reports {args.PageCount}, but diagram has {_expectedPageCount} pages.");
        }
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {_expectedPageCount}.");
    }

    // Called after a page is saved
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // Validate total page count again after the page is saved
        if (args.PageCount != _expectedPageCount)
        {
            throw new Exception($"Page count mismatch at end: callback reports {args.PageCount}, but diagram has {_expectedPageCount} pages.");
        }
        Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {_expectedPageCount}.");
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Path to the input Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Expected page count from the diagram after loading
            int expectedPageCount = diagram.Pages.Count;

            // Configure PDF save options with the custom page-saving callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new PageCountValidator(expectedPageCount);

            // Save the diagram to PDF (the actual output path can be any valid location)
            string outputPath = "output.pdf";
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine("Diagram saved successfully with page count validation.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}