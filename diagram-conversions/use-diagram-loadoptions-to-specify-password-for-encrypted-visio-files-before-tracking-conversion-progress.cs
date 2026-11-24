using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (adjust as needed)
        string inputPath = "encrypted_input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "converted_output.pdf";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // NOTE: Aspose.Diagram does not support setting a password via LoadOptions.
            // Encrypted Visio files must be opened without a password using the Diagram constructor.
            // If password handling is required, it is not available in this version of the API.
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options and attach a page-saving callback to track progress
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new ConversionProgressCallback();

            // Explicitly set the save format for the options (required for some pipelines)
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Save the diagram to PDF while tracking progress via the callback
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Conversion completed successfully. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during loading or saving
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

// Implementation of IPageSavingCallback to monitor conversion progress
class ConversionProgressCallback : IPageSavingCallback
{
    // Called before a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}...");
    }

    // Called after a page has finished saving
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");

        // Example: stop processing further pages if a condition is met
        // args.HasMorePages = false; // Uncomment to abort after a certain condition
    }
}