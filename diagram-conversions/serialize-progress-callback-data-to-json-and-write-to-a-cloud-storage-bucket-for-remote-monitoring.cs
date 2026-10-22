using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output PDF file path
        string outputPath = "output.pdf";

        // Simulated cloud storage bucket folder (replace with actual bucket handling)
        string bucketFolder = "cloud_bucket";

        // Ensure bucket folder exists
        try
        {
            if (!Directory.Exists(bucketFolder))
                Directory.CreateDirectory(bucketFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create bucket folder: {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options with a custom page saving callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new ProgressCallback(bucketFolder);

            // Save the diagram as PDF using the configured options
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}

// Custom callback to track page saving progress and write JSON to cloud bucket
class ProgressCallback : IPageSavingCallback
{
    private readonly string _bucketFolder;

    public ProgressCallback(string bucketFolder)
    {
        _bucketFolder = bucketFolder;
    }

    // Called before a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        WriteProgress(args.PageIndex, args.PageCount, "Start");
    }

    // Called after a page has been saved
    public void PageEndSaving(PageEndSavingArgs args)
    {
        WriteProgress(args.PageIndex, args.PageCount, "End");
        // Continue processing remaining pages
        args.HasMorePages = true; // property expects a bool, not Aspose BOOL
    }

    // Helper method to serialize progress info to JSON and write to a file in the bucket
    private void WriteProgress(int pageIndex, int pageCount, string stage)
    {
        var progressInfo = new
        {
            PageIndex = pageIndex,
            PageCount = pageCount,
            Stage = stage,
            Timestamp = DateTime.UtcNow
        };

        string json = JsonSerializer.Serialize(progressInfo);

        // Write each JSON line to a file named progress.log inside the bucket folder
        string logPath = Path.Combine(_bucketFolder, "progress.log");
        try
        {
            File.AppendAllText(logPath, json + Environment.NewLine);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write progress to bucket: {ex.Message}");
        }
    }
}