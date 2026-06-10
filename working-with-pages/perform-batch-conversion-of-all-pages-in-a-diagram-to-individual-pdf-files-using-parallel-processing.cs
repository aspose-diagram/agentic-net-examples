using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (first argument) or default.
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Output directory (second argument) or default.
                string outputDir = args.Length > 1 ? args[1] : "output";

                // Ensure the output directory exists.
                if (!Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // Load the diagram from the specified file.
                Diagram diagram = new Diagram(inputPath);

                int pageCount = diagram.Pages.Count;

                // Export each page to a separate PDF file using parallel processing.
                Parallel.For(0, pageCount, i =>
                {
                    // Configure PDF save options for the current page.
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        // Render only the page with index i.
                        PageIndex = i,
                        // Do not include hidden pages in the output.
                        ExportHiddenPage = false,
                        // Explicitly set the save format (optional, but safe).
                        SaveFormat = SaveFileFormat.Pdf
                    };

                    // Build the output file name (e.g., Page_1.pdf, Page_2.pdf, ...).
                    string outputPath = Path.Combine(outputDir, $"Page_{i + 1}.pdf");

                    // Save the diagram page as PDF.
                    diagram.Save(outputPath, pdfOptions);
                });

                Console.WriteLine($"Export completed. {pageCount} PDF files created in '{outputDir}'.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }