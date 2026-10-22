using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsd";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.pdf";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the VSD diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Set the save format explicitly
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // NOTE: Aspose.Diagram does not expose a direct property for PDF/A compliance
                // or lossless image compression in PdfSaveOptions. The default PDF export
                // embeds images as they are, which is the closest achievable result.

                // Assign a page saving callback to demonstrate per‑page processing
                pdfOptions.PageSavingCallback = new CustomPageSavingCallback();

                // Save the diagram as PDF
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"PDF exported successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

// Implementation of the IPageSavingCallback interface for PDF export
class CustomPageSavingCallback : IPageSavingCallback
{
    // Called before a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}");
    }

    // Called after a page has finished saving
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}");
        // No need to stop further processing; continue with remaining pages
    }
}