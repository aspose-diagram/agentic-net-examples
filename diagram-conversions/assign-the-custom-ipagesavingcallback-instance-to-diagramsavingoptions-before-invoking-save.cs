using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Set the format tracker explicitly
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Assign custom page saving callback
            pdfOptions.PageSavingCallback = new MyPageSavingCallback();

            // Save the diagram as PDF using the configured options
            diagram.Save(outputPath, pdfOptions);
            Console.WriteLine($"Diagram successfully saved to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

// Custom implementation of IPageSavingCallback to handle page save events
class MyPageSavingCallback : IPageSavingCallback
{
    // Called before a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
        // Example: you could modify args here if needed
    }

    // Called after a page has finished saving
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");
        // To stop further page processing, you could set:
        // args.HasMorePages = false;
    }
}