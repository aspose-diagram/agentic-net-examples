using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Custom callback to report page saving progress.
    private class MyPageSavingCallback : IPageSavingCallback
    {
        // Called before a page starts saving.
        public void PageStartSaving(PageStartSavingArgs args)
        {
            // args.PageIndex is zero‑based; display a user‑friendly 1‑based index.
            Console.WriteLine($"[Progress] Starting to save page {args.PageIndex + 1} of {args.PageCount}...");
        }

        // Called after a page has been saved.
        public void PageEndSaving(PageEndSavingArgs args)
        {
            Console.WriteLine($"[Progress] Finished saving page {args.PageIndex + 1} of {args.PageCount}.");
            // If you need to stop further processing, you could set:
            // args.HasMorePages = false;
        }
    }

    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output PDF file path.
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
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare PDF save options and attach the progress callback.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.PageSavingCallback = new MyPageSavingCallback();

                // Optional: configure additional PDF options here if needed.
                // Example: pdfOptions.TextCompression = PdfTextCompression.Flate;

                // Save the diagram to PDF using the configured options.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine("Conversion completed successfully.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}