using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class MyPageSavingCallback : IPageSavingCallback
{
    // Called before a page starts saving.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
        // Additional logic before saving a page can be placed here.
    }

    // Called after a page has been saved.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");

        // Example: stop processing after the first page.
        if (args.PageIndex == 0)
        {
            // To stop further pages, assign false (plain bool) to HasMorePages.
            args.HasMorePages = false;
            Console.WriteLine("No more pages will be processed.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expect the input Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the input Visio file as a command‑line argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output PDF path (same folder, same name with .pdf extension).
        string outputPath = Path.ChangeExtension(inputPath, ".pdf");

        try
        {
            // Load the Visio diagram inside a using block (ensures proper disposal).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options and assign the custom page‑saving callback.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Explicitly set the save format for clarity.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Assign the callback to receive per‑page notifications.
                pdfOptions.PageSavingCallback = new MyPageSavingCallback();

                // Save the diagram to PDF using the configured options.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Diagram successfully saved to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}