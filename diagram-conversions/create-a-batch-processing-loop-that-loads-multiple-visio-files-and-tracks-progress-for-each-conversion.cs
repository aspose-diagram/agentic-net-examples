using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> [outputFolder]");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args.Length >= 2 ? args[1] : inputFolder;

        // Guard: input folder must exist
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {outputFolder}. Error: {ex.Message}");
                return;
            }
        }

        // Supported Visio extensions
        string[] extensions = new[] { ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx", ".vss", ".vst", ".vssx", ".vstx" };

        // Gather all files with supported extensions
        string[] allFiles = Directory.GetFiles(inputFolder);
        var visioFiles = new System.Collections.Generic.List<string>();
        foreach (var file in allFiles)
        {
            if (Array.Exists(extensions, ext => ext.Equals(Path.GetExtension(file), StringComparison.OrdinalIgnoreCase)))
            {
                visioFiles.Add(file);
            }
        }

        int totalFiles = visioFiles.Count;
        if (totalFiles == 0)
        {
            Console.WriteLine("No Visio files found to process.");
            return;
        }

        Console.WriteLine($"Found {totalFiles} Visio file(s) to convert.");

        // Process each Visio file
        for (int i = 0; i < totalFiles; i++)
        {
            string inputPath = visioFiles[i];
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName + ".pdf");

            // Guard: input file must exist
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            Console.WriteLine($"[{i + 1}/{totalFiles}] Converting '{fileName}' to PDF...");

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Configure PDF save options with a page-saving callback for progress tracking
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.PageSavingCallback = new PageProgressCallback(fileName);

                // Save the diagram as PDF
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine($"[{i + 1}/{totalFiles}] Successfully saved PDF: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}

// Implementation of IPageSavingCallback to track per-page progress during PDF export
class PageProgressCallback : IPageSavingCallback
{
    private readonly string _documentName;

    public PageProgressCallback(string documentName)
    {
        _documentName = documentName;
    }

    // Called before a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"[{_documentName}] Starting page {args.PageIndex + 1} of {args.PageCount}.");
    }

    // Called after a page has been saved
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"[{_documentName}] Finished page {args.PageIndex + 1} of {args.PageCount}.");
        // No need to modify args.HasMorePages unless you want to stop early
    }
}