using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class JsonProgressCallback : IPageSavingCallback
{
    private readonly string _bucketPath;
    private readonly string _logFilePath;

    public JsonProgressCallback(string bucketPath)
    {
        _bucketPath = bucketPath;
        // Ensure the bucket (directory) exists
        if (!Directory.Exists(_bucketPath))
        {
            Directory.CreateDirectory(_bucketPath);
        }
        _logFilePath = Path.Combine(_bucketPath, "progress.json");
    }

    // Called before a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        try
        {
            var data = new
            {
                Event = "PageStartSaving",
                PageIndex = args.PageIndex,
                PageCount = args.PageCount,
                Timestamp = DateTime.UtcNow
            };
            string json = JsonSerializer.Serialize(data);
            File.AppendAllText(_logFilePath, json + Environment.NewLine);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error in PageStartSaving callback: {ex.Message}");
        }
    }

    // Called after a page has been saved
    public void PageEndSaving(PageEndSavingArgs args)
    {
        try
        {
            var data = new
            {
                Event = "PageEndSaving",
                PageIndex = args.PageIndex,
                PageCount = args.PageCount,
                Timestamp = DateTime.UtcNow
            };
            string json = JsonSerializer.Serialize(data);
            File.AppendAllText(_logFilePath, json + Environment.NewLine);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error in PageEndSaving callback: {ex.Message}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and bucket (directory) path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <bucketDirectoryPath>");
            return;
        }

        string inputDiagramPath = args[0];
        string bucketDirectoryPath = args[1];

        // Guard: verify input diagram file exists
        if (!File.Exists(inputDiagramPath))
        {
            Console.Error.WriteLine($"Input diagram file not found: {inputDiagramPath}");
            return;
        }

        // Guard: verify bucket directory path is provided (will be created if missing)
        if (string.IsNullOrWhiteSpace(bucketDirectoryPath))
        {
            Console.Error.WriteLine("Bucket directory path is invalid.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputDiagramPath);

            // Prepare PDF save options and assign the progress callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new JsonProgressCallback(bucketDirectoryPath);

            // Define output PDF path (same directory as input, with .pdf extension)
            string outputPdfPath = Path.ChangeExtension(inputDiagramPath, ".pdf");

            // Save the diagram as PDF while progress is reported via the callback
            diagram.Save(outputPdfPath, pdfOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during processing: {ex.Message}");
        }
    }
}