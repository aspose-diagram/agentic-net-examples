using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramPageSaveRetry
{
    // Callback implementation for PDF page saving events
    public class PageSavingCallback : IPageSavingCallback
    {
        // Called before a page starts saving
        public void PageStartSaving(PageStartSavingArgs args)
        {
            Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
        }

        // Called after a page has been saved
        public void PageEndSaving(PageEndSavingArgs args)
        {
            Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");
            // No built‑in failure indicator; retry logic is handled in the main loop.
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file (replace with actual path)
                string sourcePath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(sourcePath);

                // Get total number of pages
                int pageCount = diagram.Pages.Count;

                // Maximum number of retry attempts per page
                const int maxRetries = 3;

                // Iterate through each page and save individually with retry logic
                for (int i = 0; i < pageCount; i++)
                {
                    int attempt = 0;
                    bool success = false;
                    string outputPath = $"output_page_{i + 1}.pdf";

                    while (attempt < maxRetries && !success)
                    {
                        attempt++;
                        try
                        {
                            // Configure PDF save options for the specific page
                            PdfSaveOptions pdfOptions = new PdfSaveOptions
                            {
                                // Export only the current page
                                PageIndex = i,
                                // Assign the callback to receive page events
                                PageSavingCallback = new PageSavingCallback()
                            };

                            // Save the current page
                            diagram.Save(outputPath, pdfOptions);
                            success = true;
                            Console.WriteLine($"Page {i + 1} saved successfully on attempt {attempt}.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error saving page {i + 1} on attempt {attempt}: {ex.Message}");
                            if (attempt >= maxRetries)
                            {
                                Console.WriteLine($"Failed to save page {i + 1} after {maxRetries} attempts.");
                            }
                            else
                            {
                                Console.WriteLine($"Retrying page {i + 1}...");
                            }
                        }
                    }
                }

                // Clean up
                diagram.Dispose();

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}