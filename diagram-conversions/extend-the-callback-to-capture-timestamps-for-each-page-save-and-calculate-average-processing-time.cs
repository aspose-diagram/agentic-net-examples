using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class TimingPageSavingCallback : IPageSavingCallback
{
    private readonly Stopwatch _stopwatch = new Stopwatch();
    private readonly List<double> _pageDurations = new List<double>();

    // Called when a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Restart the stopwatch for the new page
        _stopwatch.Restart();
    }

    // Called when a page finishes saving
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // Stop the stopwatch and record the elapsed time for this page
        _stopwatch.Stop();
        _pageDurations.Add(_stopwatch.Elapsed.TotalMilliseconds);

        // If this was the last page, calculate and output the average processing time
        if (!args.HasMorePages)
        {
            double average = _pageDurations.Count > 0
                ? _pageDurations.Average()
                : 0.0;

            Console.WriteLine($"Average page processing time: {average:F2} ms");
        }
    }
}

public class Program
{
    public static void Main()
    {
        try
        {

            // Load the diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Configure PDF save options and attach the timing callback
            PdfSaveOptions saveOptions = new PdfSaveOptions
            {
                PageSavingCallback = new TimingPageSavingCallback()
            };

            // Save the diagram to PDF (replace with your desired output path)
            diagram.Save("output.pdf", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}