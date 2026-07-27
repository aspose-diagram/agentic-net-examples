using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class CustomPageSavingCallback : IPageSavingCallback
{
    private readonly string _logFilePath;

    public CustomPageSavingCallback(string logFilePath)
    {
        _logFilePath = logFilePath;
    }

    // Called when a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Log start of page saving
        File.AppendAllText(_logFilePath,
            $"Page start: Index={args.PageIndex + 1}, Total={args.PageCount}{Environment.NewLine}");

        // Ensure the page is actually output
        args.IsToOutput = true;
    }

    // Called when a page finishes saving
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // Log end of page saving
        File.AppendAllText(_logFilePath,
            $"Page end: Index={args.PageIndex + 1}, Total={args.PageCount}, HasMorePages={args.HasMorePages}{Environment.NewLine}");
    }
}

public class Program
{
    public static void Main()
    {
        try
        {

            // Load an existing diagram file
            Diagram diagram = new Diagram("input.vsdx");

            // Configure PDF save options with the custom page saving callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                PageSavingCallback = new CustomPageSavingCallback("PageSavingLog.txt")
            };

            // Save the diagram to PDF using the configured options
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}