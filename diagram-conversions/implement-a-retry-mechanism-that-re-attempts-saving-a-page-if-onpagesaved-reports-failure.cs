using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class RetryPageSavingCallback : IPageSavingCallback
{
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
    }

    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");
        // No explicit failure flag is provided by the API.
        // If an exception occurs during saving, it will be caught in the retry loop.
    }
}

class Program
{
    static void Main()
    {
        try
        {

            const string inputPath = "input.vsdx";
            const string outputPath = "output.pdf";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";
                pdfOptions.PageSavingCallback = new RetryPageSavingCallback();

                const int maxRetries = 3;
                int attempt = 0;
                bool saved = false;

                while (attempt < maxRetries && !saved)
                {
                    try
                    {
                        diagram.Save(outputPath, pdfOptions);
                        saved = true;
                        Console.WriteLine("Diagram saved successfully.");
                    }
                    catch (Exception ex)
                    {
                        attempt++;
                        Console.WriteLine($"Save attempt {attempt} failed: {ex.Message}");
                        if (attempt >= maxRetries)
                        {
                            Console.WriteLine("Maximum retry attempts reached. Save operation aborted.");
                            throw;
                        }
                        else
                        {
                            Console.WriteLine("Retrying save operation...");
                        }
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}