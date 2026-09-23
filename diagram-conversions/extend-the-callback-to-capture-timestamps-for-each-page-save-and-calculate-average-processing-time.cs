using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class MyPageSavingCallback : IPageSavingCallback
{
    // Store start time for each page index
    private readonly Dictionary<int, DateTime> _pageStartTimes = new Dictionary<int, DateTime>();
    // List of elapsed times for completed pages
    private readonly List<TimeSpan> _pageDurations = new List<TimeSpan>();

    // Called before a page is saved
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Record the start timestamp for the current page
        _pageStartTimes[args.PageIndex] = DateTime.UtcNow;
    }

    // Called after a page is saved
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // Retrieve the start timestamp and calculate the elapsed time
        if (_pageStartTimes.TryGetValue(args.PageIndex, out DateTime start))
        {
            TimeSpan duration = DateTime.UtcNow - start;
            _pageDurations.Add(duration);
        }

        // Example: stop processing after the first page (optional)
        // args.HasMorePages = false;
    }

    // Returns the average processing time across all saved pages
    public TimeSpan GetAverageProcessingTime()
    {
        if (_pageDurations.Count == 0)
            return TimeSpan.Zero;

        long totalTicks = 0;
        foreach (var ts in _pageDurations)
            totalTicks += ts.Ticks;

        return new TimeSpan(totalTicks / _pageDurations.Count);
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Instantiate and assign the custom page-saving callback
            MyPageSavingCallback callback = new MyPageSavingCallback();
            pdfOptions.PageSavingCallback = callback;

            // Save the diagram to PDF using the options with the callback
            string outputPath = "output.pdf";
            diagram.Save(outputPath, pdfOptions);

            // After saving, calculate and display the average processing time
            TimeSpan average = callback.GetAverageProcessingTime();
            Console.WriteLine($"Average page processing time: {average.TotalMilliseconds} ms");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}