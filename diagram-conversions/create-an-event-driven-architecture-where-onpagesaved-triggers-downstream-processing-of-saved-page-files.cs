using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputPdfFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options and assign the page‑saving callback.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;               // Explicitly set the format.
            pdfOptions.PageSavingCallback = new MyPageSavingCallback(); // Event‑driven processing.

            // Save the diagram as PDF; the callback will be invoked per page.
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or saving.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

/// <summary>
/// Implements IPageSavingCallback to receive notifications before and after each page is saved.
/// </summary>
class MyPageSavingCallback : IPageSavingCallback
{
    /// <summary>
    /// Called before a page starts saving. Here we simply log the start.
    /// </summary>
    /// <param name="args">Information about the page being saved.</param>
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Start saving page index {args.PageIndex} of {args.PageCount}");
    }

    /// <summary>
    /// Called after a page has been saved. Triggers downstream processing for the saved page.
    /// </summary>
    /// <param name="args">Information about the page that was saved.</param>
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"End saving page index {args.PageIndex} of {args.PageCount}");

        // Downstream processing: for demonstration, create a simple text file per page.
        ProcessSavedPage(args.PageIndex);
    }

    /// <summary>
    /// Example downstream processing logic for a saved page.
    /// </summary>
    /// <param name="pageIndex">Zero‑based index of the page that was saved.</param>
    private void ProcessSavedPage(int pageIndex)
    {
        try
        {
            // Create a marker file indicating that the page has been processed.
            string fileName = $"Processed_Page_{pageIndex}.txt";
            File.WriteAllText(fileName, $"Page {pageIndex} processed at {DateTime.Now}");
            Console.WriteLine($"Downstream processing completed for page {pageIndex}, marker file created: {fileName}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during downstream processing.
            Console.Error.WriteLine($"Error processing page {pageIndex}: {ex.Message}");
        }
    }
}