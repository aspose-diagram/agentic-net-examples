using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Entry point of the console application.
    static void Main(string[] args)
    {
        // Resolve input Visio file path.
        string inputPath = args.Length > 0 ? args[0] : PromptFilePath("Enter the path to the Visio file to convert:");
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Resolve output PDF file path.
        string outputPath = args.Length > 1 ? args[1] : PromptFilePath("Enter the desired output PDF file path:");
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Error: Output directory \"{outputDir}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options with a page‑saving callback to report progress.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.ExportHiddenPage = false;               // Do not export hidden pages. (Changed from BOOL.False to bool)
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;        // Explicitly set the format.
            pdfOptions.DefaultFont = "Arial";                  // Fallback font.
            pdfOptions.PageSavingCallback = new PageProgressCallback();

            // Save the diagram to PDF while the callback reports per‑page progress.
            diagram.Save(outputPath, pdfOptions);

            // Clean up resources.
            diagram.Dispose();

            Console.WriteLine("Conversion completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }

    // Helper method to read a file path from the console.
    private static string PromptFilePath(string message)
    {
        Console.WriteLine(message);
        return Console.ReadLine()?.Trim() ?? string.Empty;
    }
}

// Implementation of IPageSavingCallback to report per‑page progress.
class PageProgressCallback : IPageSavingCallback
{
    // Called before a page starts saving.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Simple animated circle using Unicode characters.
        Console.WriteLine($"[Page {args.PageIndex + 1}/{args.PageCount}] Starting export... ◐");
    }

    // Called after a page has finished saving.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // Indicate completion of the page.
        Console.WriteLine($"[Page {args.PageIndex + 1}/{args.PageCount}] Completed. ✔");
        // Example of stopping further processing (not used here):
        // args.HasMorePages = false;
    }
}
