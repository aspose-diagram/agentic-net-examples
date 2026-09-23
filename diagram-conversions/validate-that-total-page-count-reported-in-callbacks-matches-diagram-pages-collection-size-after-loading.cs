using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class MyPageSavingCallback : IPageSavingCallback
{
    private readonly int _expectedPageCount;

    public MyPageSavingCallback(int expectedPageCount)
    {
        _expectedPageCount = expectedPageCount;
    }

    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Validate that the total page count reported at the start of each page matches the diagram's page count.
        if (args.PageCount != _expectedPageCount)
        {
            throw new Exception($"PageStartSaving: Expected page count {_expectedPageCount}, but got {args.PageCount}.");
        }
    }

    public void PageEndSaving(PageEndSavingArgs args)
    {
        // Validate that the total page count reported at the end of each page matches the diagram's page count.
        if (args.PageCount != _expectedPageCount)
        {
            throw new Exception($"PageEndSaving: Expected page count {_expectedPageCount}, but got {args.PageCount}.");
        }

        // Continue processing remaining pages.
        args.HasMorePages = true;
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio diagram.
            string inputPath = "input.vsdx";

            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Capture the page count after loading.
            int diagramPageCount = diagram.Pages.Count;

            // Prepare PDF save options and assign the custom callback.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new MyPageSavingCallback(diagramPageCount);

            // Path for the exported PDF.
            string outputPath = "output.pdf";

            // Save the diagram to PDF using the options with the callback.
            diagram.Save(outputPath, pdfOptions);

            // If we reach this point without exception, the page counts matched.
            Console.WriteLine($"Successfully saved PDF. Diagram page count: {diagramPageCount}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}