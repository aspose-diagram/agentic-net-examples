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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Set the format tracker explicitly as required.
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Assign the custom page‑saving callback.
            pdfOptions.PageSavingCallback = new CustomPageSavingCallback();

            // Save the diagram to PDF using the configured options.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram successfully saved to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

/// <summary>
/// Custom implementation of IPageSavingCallback to receive page‑saving events during PDF export.
/// </summary>
class CustomPageSavingCallback : IPageSavingCallback
{
    /// <summary>
    /// Called before a page starts saving.
    /// </summary>
    /// <param name="args">Event arguments containing page information.</param>
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
        // Additional per‑page initialization logic can be placed here.
    }

    /// <summary>
    /// Called after a page has been saved.
    /// </summary>
    /// <param name="args">Event arguments containing page information.</param>
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");
        // To stop further page processing, uncomment the following line:
        // args.HasMorePages = BOOL.False;
    }
}