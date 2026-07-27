using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

// Custom callback to capture timestamps for each page and compute average processing time
class PageTimingCallback : IPageSavingCallback
{
    // Stores the start time of the current page
    private DateTime _pageStartTime;

    // List of processing times (in milliseconds) for all pages
    private readonly List<double> _pageDurations = new List<double>();

    // Called when a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Record the start timestamp for this page
        _pageStartTime = DateTime.UtcNow;
    }

    // Called when a page finishes saving
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // Calculate the elapsed time for the page
        var elapsedMs = (DateTime.UtcNow - _pageStartTime).TotalMilliseconds;
        _pageDurations.Add(elapsedMs);

        // If this was the last page, compute and display the average processing time
        if (!args.HasMorePages)
        {
            double average = _pageDurations.Count > 0 ? _pageDurations.Average() : 0;
            Console.WriteLine($"Average page processing time: {average:F2} ms");
        }
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram (using the provided load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Configure PDF save options and attach the custom callback
            PdfSaveOptions saveOptions = new PdfSaveOptions
            {
                PageSavingCallback = new PageTimingCallback()
            };

            // Save the diagram to PDF (using the provided save rule)
            diagram.Save("output.pdf", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}