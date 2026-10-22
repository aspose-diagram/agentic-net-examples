using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args == null || args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputPdfFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Lists to store memory usage before and after each page is saved.
        List<long> memoryBeforePage = new List<long>();
        List<long> memoryAfterPage = new List<long>();

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Create PDF save options and assign the custom page‑saving callback.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new PageMemoryProfiler(memoryBeforePage, memoryAfterPage);

            // Record total memory before the conversion starts.
            long totalMemoryBefore = GC.GetTotalMemory(true);
            Console.WriteLine($"Total memory before conversion: {totalMemoryBefore} bytes");

            // Perform the conversion to PDF. The callback will be invoked per page.
            diagram.Save(outputPath, pdfOptions);

            // Record total memory after the conversion completes.
            long totalMemoryAfter = GC.GetTotalMemory(true);
            Console.WriteLine($"Total memory after conversion: {totalMemoryAfter} bytes");

            // Output per‑page memory profiling results.
            for (int i = 0; i < memoryBeforePage.Count; i++)
            {
                long before = memoryBeforePage[i];
                long after = memoryAfterPage[i];
                Console.WriteLine($"Page {i + 1}: Memory before = {before} bytes, after = {after} bytes, delta = {after - before} bytes");
            }

            Console.WriteLine("Diagram conversion completed successfully.");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or saving.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }

    // Custom implementation of IPageSavingCallback to profile memory usage per page.
    private class PageMemoryProfiler : IPageSavingCallback
    {
        private readonly List<long> _before;
        private readonly List<long> _after;

        public PageMemoryProfiler(List<long> before, List<long> after)
        {
            _before = before;
            _after = after;
        }

        // Called before a page starts saving.
        public void PageStartSaving(PageStartSavingArgs args)
        {
            long mem = GC.GetTotalMemory(true);
            _before.Add(mem);
            Console.WriteLine($"Starting page {args.PageIndex + 1} of {args.PageCount}. Memory: {mem} bytes");
        }

        // Called after a page has been saved.
        public void PageEndSaving(PageEndSavingArgs args)
        {
            long mem = GC.GetTotalMemory(true);
            _after.Add(mem);
            Console.WriteLine($"Finished page {args.PageIndex + 1} of {args.PageCount}. Memory: {mem} bytes");
        }
    }
}