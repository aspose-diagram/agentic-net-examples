using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class TimingPageSavingCallback : IPageSavingCallback
{
    // Stopwatch to measure time for each page
    private Stopwatch _stopwatch = new Stopwatch();

    // List to store elapsed milliseconds for each page
    private readonly List<double> _pageTimes = new List<double>();

    // Called before a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Restart stopwatch for the new page
        _stopwatch.Restart();
        Console.WriteLine($"Starting save of page {args.PageIndex + 1} of {args.PageCount}");
    }

    // Called after a page has been saved
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // Stop stopwatch and record elapsed time
        _stopwatch.Stop();
        double elapsedMs = _stopwatch.Elapsed.TotalMilliseconds;
        _pageTimes.Add(elapsedMs);
        Console.WriteLine($"Finished save of page {args.PageIndex + 1}. Time: {elapsedMs} ms");
    }

    // Calculates and prints the average processing time
    public void PrintAverage()
    {
        if (_pageTimes.Count == 0)
        {
            Console.WriteLine("No page timing data collected.");
            return;
        }

        double total = 0;
        foreach (double t in _pageTimes)
        {
            total += t;
        }

        double average = total / _pageTimes.Count;
        Console.WriteLine($"Average page save time: {average} ms");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file and output PDF file
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.SaveFormat = SaveFileFormat.Pdf; // Explicitly set format tracking

            // Assign the custom page saving callback to capture timestamps
            TimingPageSavingCallback callback = new TimingPageSavingCallback();
            pdfOptions.PageSavingCallback = callback;

            // Save the diagram to PDF using the configured options
            diagram.Save(outputPath, pdfOptions);

            // After saving, output the average processing time
            callback.PrintAverage();
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}