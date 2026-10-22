using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path and output PDF file path as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options and assign the custom page-saving callback.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new PageProgressCallback();

            // Save the diagram to PDF using the configured options.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine("Conversion completed successfully.");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or saving.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

// Custom callback to report page conversion progress.
class PageProgressCallback : IPageSavingCallback
{
    // Called before a page starts saving.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // args.PageIndex is zero‑based; display as 1‑based for user friendliness.
        Console.WriteLine($"Starting conversion of page {args.PageIndex + 1} of {args.PageCount}...");
    }

    // Called after a page has finished saving.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // Calculate simple percentage progress.
        int percent = (int)(((args.PageIndex + 1) * 100.0) / args.PageCount);
        Console.WriteLine($"Finished page {args.PageIndex + 1}. Progress: {percent}%");
    }
}