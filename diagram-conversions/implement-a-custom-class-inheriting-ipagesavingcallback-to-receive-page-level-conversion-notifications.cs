using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class MyPageSavingCallback : IPageSavingCallback
{
    // Invoked when a page starts to be saved.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Log start information.
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");

        // Example: cancel saving of a specific page.
        // args.IsToOutput = false;
    }

    // Invoked when a page has finished saving.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // Log completion information.
        Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");

        // Example: indicate whether more pages remain.
        // args.HasMorePages = args.PageIndex < args.PageCount - 1;
    }
}

// Demonstrates assigning the callback to PdfSaveOptions.
public class DiagramConverter
{
    public void ConvertToPdf(string sourceFile, string targetFile)
    {
        // Load the diagram (uses existing load rule).
        Diagram diagram = new Diagram(sourceFile);

        // Configure PDF save options.
        PdfSaveOptions pdfOptions = new PdfSaveOptions
        {
            // Attach the custom page‑saving callback.
            PageSavingCallback = new MyPageSavingCallback()
        };

        // Save the diagram as PDF (uses existing save rule).
        diagram.Save(targetFile, pdfOptions);
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            var obj = new MyPageSavingCallback();
            obj.PageStartSaving(null);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
