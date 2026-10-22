using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the input Visio file path as the first argument.");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output PDF path (same folder, same name with .pdf extension)
        string outputPath = Path.ChangeExtension(inputPath, ".pdf");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options and assign the custom page saving callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.SaveFormat = SaveFileFormat.Pdf; // Explicitly set format tracker
            pdfOptions.PageSavingCallback = new CustomPageSavingCallback();

            // Save the diagram as PDF using the configured options
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram successfully saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

// Custom callback implementation to log page saving progress
class CustomPageSavingCallback : IPageSavingCallback
{
    // Called before a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Start saving page index {args.PageIndex} of pages {args.PageCount}");
    }

    // Called after a page has finished saving
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"End saving page index {args.PageIndex} of pages {args.PageCount}");
    }
}