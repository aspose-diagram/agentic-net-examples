using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Custom callback to monitor page saving events and log memory usage.
    class MyPageSavingCallback : IPageSavingCallback
    {
        // Called before a page starts saving.
        public void PageStartSaving(PageStartSavingArgs args)
        {
            Console.WriteLine($"[Callback] Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
            LogMemoryUsage("Start");
        }

        // Called after a page has been saved.
        public void PageEndSaving(PageEndSavingArgs args)
        {
            Console.WriteLine($"[Callback] Finished saving page {args.PageIndex + 1} of {args.PageCount}.");
            LogMemoryUsage("End");
        }

        // Helper method to log current process memory usage.
        private void LogMemoryUsage(string stage)
        {
            Process proc = Process.GetCurrentProcess();
            long privateBytes = proc.PrivateMemorySize64;
            long gcBytes = GC.GetTotalMemory(false);
            Console.WriteLine($"[Memory] {stage} - PrivateMemory: {privateBytes / 1024 / 1024} MB, GC.GetTotalMemory: {gcBytes / 1024 / 1024} MB");
        }
    }

    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output PDF path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Prepare PDF save options with the custom page-saving callback.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new MyPageSavingCallback();
            // Set a default font to avoid missing font warnings.
            pdfOptions.DefaultFont = "Arial";

            // Log memory usage before conversion.
            Console.WriteLine("[Info] Starting conversion...");
            LogMemory("Before conversion");

            // Perform the conversion to PDF.
            diagram.Save(outputPath, pdfOptions);

            // Log memory usage after conversion.
            LogMemory("After conversion");
            Console.WriteLine("[Info] Conversion completed successfully.");
        }
        catch (Exception ex)
        {
            // Capture any errors from Aspose.Diagram operations.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }

    // Helper method to log current memory usage.
    private static void LogMemory(string label)
    {
        Process proc = Process.GetCurrentProcess();
        long privateBytes = proc.PrivateMemorySize64;
        long gcBytes = GC.GetTotalMemory(false);
        Console.WriteLine($"[Memory] {label} - PrivateMemory: {privateBytes / 1024 / 1024} MB, GC.GetTotalMemory: {gcBytes / 1024 / 1024} MB");
    }
}