using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect exactly two arguments: input folder and output folder.
        if (args.Length != 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Validate input folder existence.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists; create if necessary.
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

        // Get all Visio files in the input folder (common extensions).
        string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in visioFiles)
        {
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx" && extension != ".vssx" && extension != ".vss" && extension != ".vstx" && extension != ".vst")
            {
                // Skip non-Visio files.
                continue;
            }

            // Guard: ensure the file still exists before processing.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found (skipped): {filePath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block as required.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Prepare PDF save options with a page-saving callback.
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.PageSavingCallback = new SimplePageSavingCallback();

                    // Determine output PDF path.
                    string outputFileName = Path.GetFileNameWithoutExtension(filePath) + ".pdf";
                    string outputPath = Path.Combine(outputFolder, outputFileName);

                    // Save the diagram as PDF.
                    diagram.Save(outputPath, pdfOptions);
                    Console.WriteLine($"Converted '{filePath}' to '{outputPath}'.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}

// Implementation of IPageSavingCallback to track conversion progress.
class SimplePageSavingCallback : IPageSavingCallback
{
    // Called before a page starts saving.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Starting page {args.PageIndex + 1} of {args.PageCount}.");
    }

    // Called after a page has been saved.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished page {args.PageIndex + 1}.");
        // Example: stop further processing after the first page (optional).
        // args.HasMorePages = false;
    }
}