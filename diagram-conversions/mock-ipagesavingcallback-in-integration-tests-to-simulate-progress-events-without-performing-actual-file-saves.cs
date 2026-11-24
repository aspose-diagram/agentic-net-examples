using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate that an input file path was provided.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options and attach the mock callback.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf; // Explicitly set format tracker.
                pdfOptions.PageSavingCallback = new MockPageSavingCallback();

                // Save to a memory stream to avoid writing a physical file.
                using (MemoryStream ms = new MemoryStream())
                {
                    diagram.Save(ms, pdfOptions);
                    Console.WriteLine($"Diagram processed. Memory stream size: {ms.Length} bytes.");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Mock implementation of IPageSavingCallback for integration testing.
    private class MockPageSavingCallback : IPageSavingCallback
    {
        // Called before a page starts saving.
        public void PageStartSaving(PageStartSavingArgs args)
        {
            Console.WriteLine($"[Mock] Start saving page {args.PageIndex + 1} of {args.PageCount}");
        }

        // Called after a page has finished saving.
        public void PageEndSaving(PageEndSavingArgs args)
        {
            Console.WriteLine($"[Mock] End saving page {args.PageIndex + 1} of {args.PageCount}");

            // Stop processing after the third page (index 2).
            if (args.PageIndex >= 2)
            {
                // According to the API, HasMorePages is a plain bool.
                args.HasMorePages = false;
                Console.WriteLine("[Mock] Stopping further page processing.");
            }
            else
            {
                // Continue processing remaining pages.
                args.HasMorePages = true;
            }
        }
    }
}