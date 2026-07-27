using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramConversion
{
    // Implements page saving callback to report progress during PDF export
    public class ConsolePageSavingCallback : IPageSavingCallback
    {
        public void PageStartSaving(PageStartSavingArgs args)
        {
            Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
        }

        public void PageEndSaving(PageEndSavingArgs args)
        {
            Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");
            // Example: stop after first page (uncomment if needed)
            // args.HasMorePages = false;
        }
    }

    public class Program
    {
        // Entry point
        public static void Main(string[] args)
        {
            // Determine input and output directories
            string inputFolder;
            string outputFolder;

            if (args.Length >= 2)
            {
                inputFolder = args[0];
                outputFolder = args[1];
            }
            else
            {
                Console.WriteLine("Usage: DiagramConversion <inputFolder> <outputFolder>");
                return;
            }

            if (!Directory.Exists(inputFolder))
            {
                Console.WriteLine($"Input folder does not exist: {inputFolder}");
                return;
            }

            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
                Console.WriteLine($"Created output folder: {outputFolder}");
            }

            // Get all Visio files (VSDX, VDX, VSSX, etc.) in the input folder
            string[] diagramFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
            int totalFiles = diagramFiles.Length;
            int processedCount = 0;

            foreach (string filePath in diagramFiles)
            {
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                // Process only supported Visio formats
                if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx" && extension != ".vssx")
                {
                    continue;
                }

                processedCount++;
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                string outputPath = Path.Combine(outputFolder, fileName + ".pdf");

                try
                {
                    Console.WriteLine($"[{processedCount}/{totalFiles}] Loading diagram: {filePath}");
                    // Load diagram (no LoadOptions needed for this version)
                    Diagram diagram = new Diagram(filePath);

                    // Prepare PDF save options with progress callback
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.PageSavingCallback = new ConsolePageSavingCallback();

                    Console.WriteLine($"Saving to PDF: {outputPath}");
                    diagram.Save(outputPath, pdfOptions);

                    Console.WriteLine($"Successfully converted: {fileName}.pdf");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Conversion process completed.");
        }
    }
}