using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CustomPageNamingCallback : IPageSavingCallback
{
    private readonly Diagram _diagram;

    public CustomPageNamingCallback(Diagram diagram)
    {
        _diagram = diagram ?? throw new ArgumentNullException(nameof(diagram));
    }

    // Called before a page is saved
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // args.PageIndex is zero‑based
        int pageIndex = args.PageIndex;
        if (pageIndex >= 0 && pageIndex < _diagram.Pages.Count)
        {
            Page page = _diagram.Pages[pageIndex];
            // Set a custom title for the page
            page.Name = $"Custom Page {pageIndex + 1}";
            page.NameU = $"Custom_Page_{pageIndex + 1}";
        }
    }

    // Called after a page is saved
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // No additional processing needed; keep the default behavior
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Path for the resulting PDF file
            string outputPath = "output.pdf";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Assign the custom page‑saving callback
                pdfOptions.PageSavingCallback = new CustomPageNamingCallback(diagram);

                // Save the diagram as PDF with the custom page titles
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine("Diagram saved with custom page names.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}