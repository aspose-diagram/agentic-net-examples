using System;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramMemoryProfiling
{
    // Callback to profile memory usage per page during PDF export
    public class MemoryProfilingCallback : IPageSavingCallback
    {
        // Called before a page starts saving
        public void PageStartSaving(PageStartSavingArgs args)
        {
            long memoryBytes = Process.GetCurrentProcess().PrivateMemorySize64;
            Console.WriteLine($"[Start] Page {args.PageIndex + 1}/{args.PageCount} - Memory: {memoryBytes / 1024 / 1024} MB");
        }

        // Called after a page has been saved
        public void PageEndSaving(PageEndSavingArgs args)
        {
            long memoryBytes = Process.GetCurrentProcess().PrivateMemorySize64;
            Console.WriteLine($"[End]   Page {args.PageIndex + 1}/{args.PageCount} - Memory: {memoryBytes / 1024 / 1024} MB");
            // Continue processing remaining pages
            args.HasMorePages = true;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "large_diagram.vsdx";
                // Output PDF file path
                string outputPath = "large_diagram.pdf";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Configure PDF save options with the custom callback
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.DefaultFont = "Arial";
                    pdfOptions.PageSavingCallback = new MemoryProfilingCallback();

                    // Save the diagram to PDF while profiling memory usage
                    diagram.Save(outputPath, pdfOptions);
                }

                Console.WriteLine("Conversion completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}