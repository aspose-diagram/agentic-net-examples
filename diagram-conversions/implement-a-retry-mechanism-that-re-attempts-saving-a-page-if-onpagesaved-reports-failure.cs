using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramPageSaveRetry
{
    // Custom callback to capture pages that failed during the initial save.
    // The callback is invoked for each page when using PdfSaveOptions.
    public class RetryPageSavingCallback : IPageSavingCallback
    {
        // Store indexes of pages that need to be retried.
        public static List<int> FailedPageIndexes { get; } = new List<int>();

        // Called before a page starts saving – not used here.
        public void PageStartSaving(PageStartSavingArgs args)
        {
            // No action needed at start.
        }

        // Called after a page has been saved.
        public void PageEndSaving(PageEndSavingArgs args)
        {
            // Simulate a failure condition.
            // In a real scenario, you would inspect args for error information.
            // For demonstration, treat odd‑numbered pages as failures.
            if (args.PageIndex % 2 == 1) // zero‑based index
            {
                // Record the failed page for later retry.
                FailedPageIndexes.Add(args.PageIndex);
            }
        }
    }

    class Program
    {
        // Maximum number of retry attempts per page.
        private const int MaxRetryAttempts = 3;

        static void Main()
        {
            try
            {

                // Path to the source Visio diagram.
                const string inputPath = "input.vsdx";

                // Path for the primary PDF output.
                const string outputPdf = "output.pdf";

                // Ensure the input file exists.
                if (!File.Exists(inputPath))
                {
                    throw new FileNotFoundException($"Input file not found: {inputPath}");
                }

                // Load the diagram.
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Configure PDF save options with the custom callback.
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        DefaultFont = "Arial",
                        PageSavingCallback = new RetryPageSavingCallback()
                    };

                    // Initial save – pages that meet the simulated failure condition
                    // will be recorded by the callback.
                    diagram.Save(outputPdf, pdfOptions);

                    // If any pages failed, attempt retries.
                    if (RetryPageSavingCallback.FailedPageIndexes.Count > 0)
                    {
                        Console.WriteLine("Retrying failed pages...");

                        foreach (int pageIndex in RetryPageSavingCallback.FailedPageIndexes)
                        {
                            bool success = false;
                            int attempt = 0;

                            while (!success && attempt < MaxRetryAttempts)
                            {
                                attempt++;

                                try
                                {
                                    // Create new PDF options targeting a single page.
                                    PdfSaveOptions retryOptions = new PdfSaveOptions
                                    {
                                        DefaultFont = "Arial",
                                        // Render only the specific page.
                                        PageIndex = pageIndex,
                                        PageCount = 1
                                    };

                                    // Save the specific page to a temporary file.
                                    string tempFile = $"output_page_{pageIndex}_retry_{attempt}.pdf";
                                    diagram.Save(tempFile, retryOptions);

                                    // If no exception, the retry succeeded.
                                    Console.WriteLine($"Page {pageIndex} saved successfully on attempt {attempt}.");
                                    success = true;
                                }
                                catch (Exception ex)
                                {
                                    // Log the failure and continue to next attempt.
                                    Console.WriteLine($"Attempt {attempt} for page {pageIndex} failed: {ex.Message}");
                                    if (attempt >= MaxRetryAttempts)
                                    {
                                        Console.WriteLine($"Page {pageIndex} could not be saved after {MaxRetryAttempts} attempts.");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("All pages saved successfully on the first pass.");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}