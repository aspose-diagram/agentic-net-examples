using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path and output PDF path as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options and attach the page‑saving callback.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.PageSavingCallback = new PageProgressCallback();

                // Perform the conversion.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine("Conversion completed successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}

// Implements per‑page progress reporting for PDF conversion.
class PageProgressCallback : IPageSavingCallback
{
    // Called before a page starts saving.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // args.PageIndex is zero‑based; display as 1‑based for user friendliness.
        Console.WriteLine($"[Page {args.PageIndex + 1}/{args.PageCount}] Starting export...");
    }

    // Called after a page has been saved.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"[Page {args.PageIndex + 1}/{args.PageCount}] Completed.");

        // Example of a simple animated progress indicator using console characters.
        // Here we just print a rotating bar based on the page index.
        char[] spinner = new[] { '/', '-', '\\', '|' };
        char current = spinner[args.PageIndex % spinner.Length];
        Console.WriteLine($"Progress: {current}");
    }
}