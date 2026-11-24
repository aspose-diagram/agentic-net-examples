using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ProgressCallback : IPageSavingCallback
{
    // Tracks the total number of pages in the document.
    public int TotalPages { get; private set; } = 0;

    // Tracks how many pages have been processed so far.
    public int PagesProcessed { get; private set; } = 0;

    // Called before a page starts saving.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Capture total page count on the first callback invocation.
        if (TotalPages == 0)
        {
            TotalPages = args.PageCount;
        }
        // Optionally, you could log start of page saving here.
    }

    // Called after a page has been saved.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // Increment the processed page counter.
        PagesProcessed++;

        // No need to stop further processing, so we leave HasMorePages unchanged.
        // If you wanted to abort early, you could set:
        // args.HasMorePages = BOOL.False;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output PDF file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare PDF save options and assign the custom page-saving callback.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            ProgressCallback callback = new ProgressCallback();
            pdfOptions.PageSavingCallback = callback;

            // Perform the conversion to PDF.
            diagram.Save(outputPath, pdfOptions);

            // After successful conversion, output JSON status.
            string jsonResult = $"{{\"status\":\"success\",\"pagesProcessed\":{callback.PagesProcessed},\"totalPages\":{callback.TotalPages}}}";
            Console.WriteLine(jsonResult);
        }
        catch (Exception ex)
        {
            // On error, output JSON with error details.
            string errorJson = $"{{\"status\":\"error\",\"message\":\"{ex.Message.Replace("\"", "\\\"")}\"}}";
            Console.Error.WriteLine(errorJson);
        }
    }
}