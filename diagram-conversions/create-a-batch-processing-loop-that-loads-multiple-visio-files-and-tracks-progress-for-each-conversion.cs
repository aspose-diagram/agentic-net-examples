using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class PageSavingCallback : IPageSavingCallback
{
    // Called before a page starts saving.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"[Info] Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
    }

    // Called after a page has been saved.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"[Info] Finished saving page {args.PageIndex + 1} of {args.PageCount}.");

        // Example: stop processing further pages if needed.
        // args.HasMorePages = false;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Determine the input folder containing Visio files.
        string inputFolder;
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            inputFolder = args[0];
        }
        else
        {
            Console.Write("Enter the path to the folder containing Visio files: ");
            inputFolder = Console.ReadLine()?.Trim() ?? string.Empty;
        }

        // Validate input folder existence.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"[Error] Input folder does not exist: {inputFolder}");
            return;
        }

        // Determine (or create) the output folder for converted PDFs.
        string outputFolder = Path.Combine(inputFolder, "ConvertedPdf");
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[Error] Failed to create output folder: {outputFolder}. Exception: {ex.Message}");
                return;
            }
        }

        // Gather Visio files (common extensions) from the input folder.
        string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        string[] supportedExtensions = new[] { ".vsdx", ".vsd", ".vsd", ".vsx", ".vtx", ".vssx", ".vstx", ".vss", ".vst", ".vdx", ".vsx", ".vtx", ".mmd" };
        var filesToProcess = new System.Collections.Generic.List<string>();
        foreach (var file in visioFiles)
        {
            if (Array.Exists(supportedExtensions, ext => ext.Equals(Path.GetExtension(file), StringComparison.OrdinalIgnoreCase)))
            {
                filesToProcess.Add(file);
            }
        }

        if (filesToProcess.Count == 0)
        {
            Console.WriteLine("[Info] No Visio files found in the specified folder.");
            return;
        }

        Console.WriteLine($"[Info] Found {filesToProcess.Count} Visio file(s) to process.");

        // Process each Visio file.
        int fileIndex = 0;
        foreach (var inputPath in filesToProcess)
        {
            fileIndex++;
            Console.WriteLine($"[Info] Processing file {fileIndex}/{filesToProcess.Count}: {Path.GetFileName(inputPath)}");

            // Guard against missing file (should not happen as we enumerated them).
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"[Error] File not found: {inputPath}");
                continue;
            }

            // Prepare output PDF path.
            string outputFileName = Path.GetFileNameWithoutExtension(inputPath) + ".pdf";
            string outputPath = Path.Combine(outputFolder, outputFileName);

            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(inputPath);

                // Configure PDF save options with a page-saving callback for progress tracking.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.PageSavingCallback = new PageSavingCallback();

                // Save the diagram as PDF.
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine($"[Success] Converted to PDF: {outputFileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[Error] Failed to convert '{inputPath}'. Exception: {ex.Message}");
            }
        }

        Console.WriteLine("[Info] Batch conversion completed.");
    }
}