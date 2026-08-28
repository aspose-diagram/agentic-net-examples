using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramPageSaveEventDemo
{
    // Implements the page saving callback for PDF export.
    // PageStartSaving is called before a page is rendered.
    // PageEndSaving is called after a page has been rendered.
    public class MyPageSavingCallback : IPageSavingCallback
    {
        // Store the output PDF path so we can reference it in the callback.
        private readonly string _outputPdfPath;

        // Constructor receives the PDF output path from the caller.
        public MyPageSavingCallback(string outputPdfPath)
        {
            _outputPdfPath = outputPdfPath;
        }

        public void PageStartSaving(PageStartSavingArgs args)
        {
            // Log the start of page rendering.
            Console.WriteLine($"[Info] Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
        }

        public void PageEndSaving(PageEndSavingArgs args)
        {
            // Log the completion of page rendering.
            Console.WriteLine($"[Info] Finished saving page {args.PageIndex + 1} of {args.PageCount}.");

            // Example downstream processing: copy the generated PDF to a backup folder.
            try
            {
                // Use the stored PDF path (the whole document contains all pages).
                string sourcePdf = _outputPdfPath;
                // Build a backup folder next to the PDF.
                string backupFolder = Path.Combine(Path.GetDirectoryName(sourcePdf) ?? string.Empty, "Backup");
                Directory.CreateDirectory(backupFolder);

                // Create a backup file name that includes the page number for illustration.
                string backupFile = Path.Combine(backupFolder,
                    $"Page_{args.PageIndex + 1}_{Path.GetFileName(sourcePdf)}");

                // Copy the whole PDF as a placeholder for per‑page handling.
                File.Copy(sourcePdf, backupFile, overwrite: true);
                Console.WriteLine($"[Info] Backup created: {backupFile}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during downstream processing.
                Console.WriteLine($"[Error] Downstream processing failed: {ex.Message}");
            }
        }
    }

    public class Program
    {
        public static void Main()
        {
            // Input Visio file (replace with actual path).
            string inputPath = "input.vsdx";
            // Guard: ensure the input file exists before proceeding.
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            // Output PDF file.
            string outputPath = "output.pdf";

            try
            {
                // Load the diagram from the input file.
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Configure PDF save options.
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        DefaultFont = "Arial",
                        // Assign the custom callback to handle per‑page events.
                        PageSavingCallback = new MyPageSavingCallback(outputPath)
                    };

                    // Save the diagram as PDF; the callback will be invoked for each page.
                    diagram.Save(outputPath, pdfOptions);
                    Console.WriteLine($"[Info] Diagram saved to {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during loading or saving.
                Console.Error.WriteLine($"[Error] Operation failed: {ex.Message}");
            }
        }
    }
}