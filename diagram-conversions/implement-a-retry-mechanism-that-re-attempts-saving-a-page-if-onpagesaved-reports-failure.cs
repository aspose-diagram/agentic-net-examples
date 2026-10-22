using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Custom callback to track page saving progress
    private class MyPageSavingCallback : IPageSavingCallback
    {
        public void PageStartSaving(PageStartSavingArgs args)
        {
            // Log start of page saving
            Console.WriteLine($"Start saving page index {args.PageIndex} of {args.PageCount}");
        }

        public void PageEndSaving(PageEndSavingArgs args)
        {
            // Log end of page saving
            Console.WriteLine($"End saving page index {args.PageIndex} of {args.PageCount}");
        }
    }

    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output directory (second argument or default "output")
        string outputDir = args.Length > 1 ? args[1] : "output";

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            int pageCount = diagram.Pages.Count;

            // Iterate through each page and attempt to save with retries
            for (int i = 0; i < pageCount; i++)
            {
                int attempt = 0;
                const int maxAttempts = 3;
                bool saved = false;

                while (attempt < maxAttempts && !saved)
                {
                    try
                    {
                        // Configure PDF save options for a single page
                        PdfSaveOptions pdfOptions = new PdfSaveOptions();
                        pdfOptions.PageIndex = i;      // Zero‑based page index
                        pdfOptions.PageCount = 1;      // Export only this page
                        pdfOptions.PageSavingCallback = new MyPageSavingCallback();

                        // Build output file path for the current page
                        string outputPath = Path.Combine(outputDir, $"page_{i + 1}.pdf");

                        // Save the specific page
                        diagram.Save(outputPath, pdfOptions);

                        Console.WriteLine($"Successfully saved page {i + 1} to \"{outputPath}\"");
                        saved = true;
                    }
                    catch (Exception ex)
                    {
                        attempt++;
                        Console.Error.WriteLine($"Error saving page {i + 1} (attempt {attempt} of {maxAttempts}): {ex.Message}");

                        if (attempt >= maxAttempts)
                        {
                            Console.Error.WriteLine($"Failed to save page {i + 1} after {maxAttempts} attempts. Skipping.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any unexpected errors during loading or processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}