using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class TimestampCallback : IPageSavingCallback
{
    // Stores the start time for each page index.
    private readonly Dictionary<int, DateTime> _startTimes = new Dictionary<int, DateTime>();
    // Accumulates total processing time in milliseconds.
    private double _totalMilliseconds = 0;
    // Counts how many pages have been processed.
    private int _pagesProcessed = 0;

    // Called before a page starts saving.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Record the current time for the page.
        _startTimes[args.PageIndex] = DateTime.Now;
    }

    // Called after a page has been saved.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // Retrieve the start time and calculate elapsed time.
        if (_startTimes.TryGetValue(args.PageIndex, out DateTime start))
        {
            double elapsedMs = (DateTime.Now - start).TotalMilliseconds;
            _totalMilliseconds += elapsedMs;
            _pagesProcessed++;
        }
    }

    // Returns the average processing time per page in milliseconds.
    public double GetAverageMilliseconds()
    {
        return _pagesProcessed > 0 ? _totalMilliseconds / _pagesProcessed : 0;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.pdf";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Instantiate the custom callback.
        TimestampCallback callback = new TimestampCallback();

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options and assign the callback.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = callback;
            // Explicitly set the format tracker (required by some versions).
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Save the diagram to PDF using the configured options.
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console.
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
            return;
        }

        // After saving, calculate and display the average page processing time.
        double averageMs = callback.GetAverageMilliseconds();
        Console.WriteLine($"Average page processing time: {averageMs:F2} ms");
    }
}