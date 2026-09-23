using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramConversion
{
    // Callback to report page saving progress during PDF export
    class PdfPageSavingCallback : IPageSavingCallback
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

    class Program
    {
        static void Main(string[] args)
        {
            // Determine input and output folders
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

            // Ensure output folder exists
            Directory.CreateDirectory(outputFolder);

            // Supported Visio file extensions
            string[] extensions = new[] { "*.vsdx", "*.vsd", "*.vsdm", "*.vssx", "*.vstx" };

            // Gather all diagram files
            var diagramFiles = new System.Collections.Generic.List<string>();
            foreach (var ext in extensions)
            {
                diagramFiles.AddRange(Directory.GetFiles(inputFolder, ext, SearchOption.TopDirectoryOnly));
            }

            if (diagramFiles.Count == 0)
            {
                Console.WriteLine("No diagram files found in the input folder.");
                return;
            }

            Console.WriteLine($"Found {diagramFiles.Count} diagram file(s). Starting conversion...");

            int processedCount = 0;
            foreach (string filePath in diagramFiles)
            {
                try
                {
                    Console.WriteLine($"Processing: {Path.GetFileName(filePath)}");

                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Prepare PDF save options with page callback
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.PageSavingCallback = new PdfPageSavingCallback();

                    // Determine output PDF path
                    string outputFileName = Path.GetFileNameWithoutExtension(filePath) + ".pdf";
                    string outputPath = Path.Combine(outputFolder, outputFileName);

                    // Save as PDF
                    diagram.Save(outputPath, pdfOptions);

                    Console.WriteLine($"Saved PDF: {outputFileName}");
                    processedCount++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {Path.GetFileName(filePath)}: {ex.Message}");
                }
            }

            Console.WriteLine($"Conversion completed. {processedCount} of {diagramFiles.Count} file(s) processed successfully.");
        }
    }
}