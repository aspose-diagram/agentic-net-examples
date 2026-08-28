using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        // Lock object to synchronize diagram saving (Diagram is not thread‑safe)
        private static readonly object _saveLock = new object();

        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (first argument) or default
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Output directory (second argument) or default
                string outputDir = args.Length > 1 ? args[1] : "output";

                // Ensure the output directory exists
                Directory.CreateDirectory(outputDir);

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Total number of pages in the diagram
                int pageCount = diagram.Pages.Count;

                // Create an array of page indices for parallel processing
                int[] pageIndices = new int[pageCount];
                for (int i = 0; i < pageCount; i++)
                    pageIndices[i] = i;

                // Export each page to a separate PDF file in parallel
                Parallel.ForEach(pageIndices, pageIndex =>
                {
                    // Build the output PDF file name (Page_1.pdf, Page_2.pdf, ...)
                    string outputPath = Path.Combine(outputDir, $"Page_{pageIndex + 1}.pdf");

                    // Configure PDF save options for a single page
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        // Render only the current page
                        PageIndex = pageIndex,
                        PageCount = 1,
                        // Do not export hidden pages
                        ExportHiddenPage = false,
                        // Explicitly set the format (optional but safe)
                        SaveFormat = SaveFileFormat.Pdf
                    };

                    // Diagram.Save is not thread‑safe, so synchronize the call
                    lock (_saveLock)
                    {
                        diagram.Save(outputPath, pdfOptions);
                    }

                    Console.WriteLine($"Saved page {pageIndex + 1} to '{outputPath}'.");
                });

                Console.WriteLine("Batch PDF export completed.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }