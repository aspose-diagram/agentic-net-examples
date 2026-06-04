using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramConversion
{
    // Implements page saving callback to report progress during PDF export
    public class CustomPageSavingCallback : IPageSavingCallback
    {
        public void PageStartSaving(PageStartSavingArgs args)
        {
            Console.WriteLine($"[PDF] Starting page {args.PageIndex + 1} of {args.PageCount}");
        }

        public void PageEndSaving(PageEndSavingArgs args)
        {
            Console.WriteLine($"[PDF] Finished page {args.PageIndex + 1} of {args.PageCount}");
        }
    }

    public class Program
    {
        // Entry point
        public static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramConversion <inputFolder> <outputFolder>");
                return;
            }

            string inputFolder = args[0];
            string outputFolder = args[1];

            // Ensure output directory exists
            Directory.CreateDirectory(outputFolder);

            // Get all Visio files (VSDX) in the input folder
            string[] diagramFiles = Directory.GetFiles(inputFolder, "*.vsdx", SearchOption.TopDirectoryOnly);
            int totalFiles = diagramFiles.Length;
            Console.WriteLine($"Found {totalFiles} diagram file(s) to process.");

            int processedCount = 0;
            foreach (string diagramPath in diagramFiles)
            {
                try
                {
                    processedCount++;
                    string fileName = Path.GetFileName(diagramPath);
                    Console.WriteLine($"Processing ({processedCount}/{totalFiles}): {fileName}");

                    // Load diagram from file
                    Diagram diagram = new Diagram(diagramPath);

                    // Prepare PDF save options with progress callback
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.PageSavingCallback = new CustomPageSavingCallback();

                    // Define output PDF path
                    string outputPdfPath = Path.Combine(outputFolder,
                        Path.GetFileNameWithoutExtension(diagramPath) + ".pdf");

                    // Save diagram as PDF using the options
                    diagram.Save(outputPdfPath, pdfOptions);

                    Console.WriteLine($"Saved PDF: {outputPdfPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing '{diagramPath}': {ex.Message}");
                }
            }

            Console.WriteLine("All files processed.");
        }
    }
}