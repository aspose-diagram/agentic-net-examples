using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramMemoryProfiling
{
    // Callback to profile memory usage for each page during PDF export
    public class MemoryProfilingCallback : IPageSavingCallback
    {
        private long _pageStartMemory;

        // Called before a page starts rendering
        public void PageStartSaving(PageStartSavingArgs args)
        {
            // Ensure a clean state before measuring
            GC.Collect();
            GC.WaitForPendingFinalizers();

            _pageStartMemory = GC.GetTotalMemory(true);
            Console.WriteLine($"[Start] Page {args.PageIndex + 1}/{args.PageCount} - Memory: {(_pageStartMemory / 1024.0 / 1024.0):F2} MB");
        }

        // Called after a page has been rendered
        public void PageEndSaving(PageEndSavingArgs args)
        {
            // Force garbage collection again for accurate measurement
            GC.Collect();
            GC.WaitForPendingFinalizers();

            long endMemory = GC.GetTotalMemory(true);
            long delta = endMemory - _pageStartMemory;

            Console.WriteLine($"[End]   Page {args.PageIndex + 1}/{args.PageCount} - Memory: { (endMemory / 1024.0 / 1024.0):F2} MB (Δ { (delta / 1024.0 / 1024.0):F2} MB)");

            // Example: stop processing if memory usage exceeds a threshold
            // if (endMemory > 500 * 1024 * 1024) args.HasMorePages = false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file (large diagram)
                string inputPath = "largeDiagram.vsdx";

                // Output PDF file
                string outputPath = "largeDiagram.pdf";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Configure PDF save options with the memory profiling callback
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                pdfOptions.PageSavingCallback = new MemoryProfilingCallback();

                // Save the diagram to PDF while profiling memory per page
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine("Conversion completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}