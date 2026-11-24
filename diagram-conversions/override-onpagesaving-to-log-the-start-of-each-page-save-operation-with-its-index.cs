using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class MyPageSavingCallback : IPageSavingCallback
{
    // Called before a page starts saving.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Log the start of the page save operation with its index.
        Console.WriteLine($"Start saving page index {args.PageIndex} of pages {args.PageCount}");
    }

    // Called after a page has finished saving.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // Optional: log the end of the page save operation.
        Console.WriteLine($"End saving page index {args.PageIndex} of pages {args.PageCount}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Determine input file path (first argument or default).
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output file path (same folder, .pdf extension).
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? Directory.GetCurrentDirectory(),
            Path.GetFileNameWithoutExtension(inputPath) + ".pdf");

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Explicitly set the format tracker.
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;
            // Assign the custom page saving callback.
            pdfOptions.PageSavingCallback = new MyPageSavingCallback();

            // Save the diagram to PDF using the configured options.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}