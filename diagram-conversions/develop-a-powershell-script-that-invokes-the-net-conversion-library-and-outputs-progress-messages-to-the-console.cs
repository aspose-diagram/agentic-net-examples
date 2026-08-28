using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramConversion
{
    // Implements page saving callback to report progress during PDF generation
    public class PageSavingCallback : IPageSavingCallback
    {
        public void PageStartSaving(PageStartSavingArgs args)
        {
            Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
        }

        public void PageEndSaving(PageEndSavingArgs args)
        {
            Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");
            // Continue processing remaining pages
            args.HasMorePages = true;
        }
    }

    public class Program
    {
        // Entry point of the console application
        public static void Main(string[] args)
        {
            // Validate arguments: source folder and destination folder
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramConversion <sourceFolder> <outputFolder>");
                return;
            }

            string sourceFolder = args[0];
            string outputFolder = args[1];

            if (!Directory.Exists(sourceFolder))
            {
                Console.WriteLine($"Source folder does not exist: {sourceFolder}");
                return;
            }

            if (!Directory.Exists(outputFolder))
            {
                Console.WriteLine($"Output folder does not exist, creating: {outputFolder}");
                Directory.CreateDirectory(outputFolder);
            }

            // Supported Visio file extensions
            string[] extensions = new[] { ".vsdx", ".vsd", ".vdx", ".vssx", ".vss", ".vstx", ".vst" };
            string[] files = Directory.GetFiles(sourceFolder, "*.*", SearchOption.TopDirectoryOnly);
            var diagramFiles = Array.FindAll(files, f => Array.Exists(extensions, ext => ext.Equals(Path.GetExtension(f), StringComparison.OrdinalIgnoreCase)));

            int totalFiles = diagramFiles.Length;
            if (totalFiles == 0)
            {
                Console.WriteLine("No Visio files found in the source folder.");
                return;
            }

            Console.WriteLine($"Found {totalFiles} Visio file(s) to convert.");

            for (int i = 0; i < totalFiles; i++)
            {
                string inputPath = diagramFiles[i];
                string fileName = Path.GetFileName(inputPath);
                Console.WriteLine($"[{i + 1}/{totalFiles}] Processing: {fileName}");

                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(inputPath);

                    // Prepare PDF save options with a page-saving callback
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.PageSavingCallback = new PageSavingCallback();

                    // Determine output PDF path
                    string outputPath = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(inputPath) + ".pdf");

                    // Save diagram as PDF
                    diagram.Save(outputPath, pdfOptions);

                    Console.WriteLine($"Successfully saved PDF: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {fileName}: {ex.Message}");
                }
            }

            Console.WriteLine("Conversion process completed.");
        }
    }
}