using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class MemoryProfilingCallback : IPageSavingCallback
{
    // Holds memory usage captured before the current page starts rendering
    private long _memoryBefore;

    // Called before a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Capture memory usage before rendering the page
        _memoryBefore = GC.GetTotalMemory(forceFullCollection: false);
        Console.WriteLine($"[Start] Page {args.PageIndex + 1}/{args.PageCount}: Memory before = {_memoryBefore} bytes");
    }

    // Called after a page has been saved
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // Force a full garbage collection to get an accurate after value
        long memoryAfter = GC.GetTotalMemory(forceFullCollection: true);
        long diff = memoryAfter - _memoryBefore;
        Console.WriteLine($"[End]   Page {args.PageIndex + 1}/{args.PageCount}: Memory after = {memoryAfter} bytes, Diff = {diff} bytes");
        // Optionally, stop processing further pages if a leak is detected
        // args.HasMorePages = false; // Uncomment to abort on detection
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: DiagramMemoryProfiler <inputVisioFile> <outputPdfFile>");
            return;
        }

        // Assign input and output paths
        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram (no using block as Diagram is not IDisposable)
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options with the custom callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // Explicitly set the format tracker
                SaveFormat = SaveFileFormat.Pdf,
                // Use a default font to avoid missing font warnings
                DefaultFont = "Arial",
                // Assign the memory profiling callback
                PageSavingCallback = new MemoryProfilingCallback()
            };

            // Save the diagram to PDF while profiling memory per page
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
            return;
        }

        Console.WriteLine("Conversion completed.");
    }
}