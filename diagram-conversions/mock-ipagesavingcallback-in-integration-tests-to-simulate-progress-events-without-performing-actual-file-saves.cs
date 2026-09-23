using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class MyPageSavingCallback : IPageSavingCallback
{
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}");
    }

    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}");
        // Example: stop after the first page to demonstrate control flow
        if (args.PageIndex == 0)
        {
            args.HasMorePages = false;
            Console.WriteLine("Stopping further page processing.");
        }
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Load a diagram (replace with a valid path in your test environment)
            string diagramPath = "sample.vsdx";
            Diagram diagram;
            try
            {
                diagram = new Diagram(diagramPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                throw;
            }

            // Configure PDF save options and attach the mock callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new MyPageSavingCallback();

            // Attempt to save; in a test you may ignore the output file or redirect to a stream
            string outputPath = "output.pdf";
            try
            {
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine("Diagram save operation completed (or simulated).");
            }
            catch (Exception ex)
            {
                // In a mock scenario the save may be intentionally bypassed
                Console.WriteLine($"Save operation simulated; caught exception: {ex.Message}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}