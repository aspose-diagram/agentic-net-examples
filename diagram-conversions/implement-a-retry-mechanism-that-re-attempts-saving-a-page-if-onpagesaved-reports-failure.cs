using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramPageRetryExample
{
    // Custom callback that retries saving a page when a failure is detected.
    // The failure condition is simulated here; replace with real detection logic as needed.
    public class RetryPageSavingCallback : IPageSavingCallback
    {
        private readonly int _maxRetries;
        private readonly Dictionary<int, int> _retryCounts = new Dictionary<int, int>();

        // Simulated set of page indexes that should fail the first time they are saved.
        // In a real scenario, replace this with actual failure detection.
        private readonly HashSet<int> _pagesToFail;

        public RetryPageSavingCallback(int maxRetries, IEnumerable<int> pagesToFail)
        {
            _maxRetries = maxRetries;
            _pagesToFail = new HashSet<int>(pagesToFail);
        }

        // Called before a page starts saving.
        public void PageStartSaving(PageStartSavingArgs args)
        {
            // Ensure the page is output.
            args.IsToOutput = true;
        }

        // Called after a page has been saved.
        public void PageEndSaving(PageEndSavingArgs args)
        {
            int pageIndex = args.PageIndex;

            // Check if this page is marked to fail.
            if (_pagesToFail.Contains(pageIndex))
            {
                // Determine how many times we have already retried this page.
                _retryCounts.TryGetValue(pageIndex, out int currentRetry);

                if (currentRetry < _maxRetries)
                {
                    // Indicate that there are more pages to process, causing Aspose.Diagram
                    // to re‑attempt saving the current page.
                    args.HasMorePages = true;

                    // Increment retry count for this page.
                    _retryCounts[pageIndex] = currentRetry + 1;

                    // Optionally log the retry attempt.
                    Console.WriteLine($"Retrying page {pageIndex} (attempt {currentRetry + 1}/{_maxRetries})");
                }
                else
                {
                    // Max retries reached; stop retrying this page.
                    args.HasMorePages = false;
                    Console.WriteLine($"Failed to save page {pageIndex} after {_maxRetries} attempts.");
                }
            }
            else
            {
                // Normal case – no retry needed.
                args.HasMorePages = false;
            }
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing diagram.
                Diagram diagram = new Diagram("input.vsdx");

                // Configure PDF save options and attach the retry callback.
                PdfSaveOptions saveOptions = new PdfSaveOptions
                {
                    // Retry page 2 and 4 up to 3 times each (zero‑based indexes).
                    PageSavingCallback = new RetryPageSavingCallback(
                        maxRetries: 3,
                        pagesToFail: new[] { 1, 3 })
                };

                // Save the diagram to PDF. The callback will handle retries automatically.
                diagram.Save("output.pdf", saveOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}