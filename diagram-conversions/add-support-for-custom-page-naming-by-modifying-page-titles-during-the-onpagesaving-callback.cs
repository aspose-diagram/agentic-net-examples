using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class CustomPageSavingCallback : IPageSavingCallback
{
    private readonly Diagram _diagram;
    private readonly IList<string> _customTitles;

    public CustomPageSavingCallback(Diagram diagram, IList<string> customTitles)
    {
        _diagram = diagram;
        _customTitles = customTitles;
    }

    // Called when a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Ensure the page will be output
        args.IsToOutput = true;

        // Apply a custom title if one is defined for this page index
        if (args.PageIndex >= 0 && args.PageIndex < _customTitles.Count)
        {
            // The Name property of a Page is used as the title in the output
            _diagram.Pages[args.PageIndex].Name = _customTitles[args.PageIndex];
        }
    }

    // Called when a page finishes saving
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // Indicate whether more pages remain to be processed
        args.HasMorePages = args.PageIndex < args.PageCount - 1;
    }
}

public class Program
{
    public static void Main()
    {
        try
        {

            // Load an existing diagram (lifecycle rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Define custom titles for each page (adjust count as needed)
            var customTitles = new List<string>
            {
                "Project Overview",
                "Detailed Design",
                "Implementation Plan"
            };

            // Configure PDF save options and attach the callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                PageSavingCallback = new CustomPageSavingCallback(diagram, customTitles)
            };

            // Save the diagram to PDF (lifecycle rule)
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}