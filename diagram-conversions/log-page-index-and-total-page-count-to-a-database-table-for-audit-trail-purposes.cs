using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class MyPageSavingCallback : IPageSavingCallback
{
    // This method is called before each page is saved.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Log page index and total page count (replace with DB logic as needed).
        LogPageInfo(args.PageIndex, args.PageCount);
    }

    // This method is called after each page is saved.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // No post‑save actions required.
    }

    private void LogPageInfo(int pageIndex, int pageCount)
    {
        try
        {
            // Placeholder for database logging – currently writes to console.
            Console.WriteLine($"AuditLog: PageIndex={pageIndex}, TotalPages={pageCount}, Timestamp={DateTime.UtcNow}");
        }
        catch (Exception ex)
        {
            // Write any logging errors to the error stream.
            Console.Error.WriteLine($"Failed to log page info: {ex.Message}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Path to the input Visio diagram.
        string diagramPath = "input.vsdx";
        // Guard: ensure the diagram file exists before proceeding.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram within a using block to ensure disposal.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Configure PDF save options and attach the custom page‑saving callback.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.PageSavingCallback = new MyPageSavingCallback();

                // Define the output PDF path.
                string outputPath = "output.pdf";

                // Save the diagram to PDF; the callback logs each page.
                diagram.Save(outputPath, pdfOptions);
            }
        }
        catch (Exception ex)
        {
            // Capture and report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}