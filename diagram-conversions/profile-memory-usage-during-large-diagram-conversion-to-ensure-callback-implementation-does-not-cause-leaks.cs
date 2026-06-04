using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramMemoryProfiling
{
    // Callback to monitor memory usage during PDF page saving
    public class PageMemoryCallback : IPageSavingCallback
    {
        // Called before a page starts saving
        public void PageStartSaving(PageStartSavingArgs args)
        {
            long memoryBytes = GC.GetTotalMemory(forceFullCollection: true);
            Console.WriteLine($"[Start] Page {args.PageIndex + 1}/{args.PageCount} - Memory: {memoryBytes} bytes");
        }

        // Called after a page has been saved
        public void PageEndSaving(PageEndSavingArgs args)
        {
            long memoryBytes = GC.GetTotalMemory(forceFullCollection: true);
            Console.WriteLine($"[End]   Page {args.PageIndex + 1}/{args.PageCount} - Memory: {memoryBytes} bytes");
        }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "largeDiagram.vsdx";
                string outputPath = "largeDiagram.pdf";

                try
                {
                    // Load the diagram
                    using (Diagram diagram = new Diagram(inputPath))
                    {
                        // Configure PDF save options with a default font and format tracker
                        PdfSaveOptions pdfOptions = new PdfSaveOptions
                        {
                            DefaultFont = "Arial",
                            SaveFormat = SaveFileFormat.Pdf
                        };

                        // Assign the memory profiling callback
                        pdfOptions.PageSavingCallback = new PageMemoryCallback();

                        // Save the diagram to PDF using the options
                        diagram.Save(outputPath, pdfOptions);
                    }

                    Console.WriteLine("Diagram conversion completed successfully.");
                }
                catch (Exception ex)
                {
                    // Report any errors that occur during processing
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}