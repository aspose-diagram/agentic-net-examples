using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class FileLogger : IDisposable
{
    private readonly StreamWriter _writer;

    public FileLogger(string logFilePath)
    {
        // Open the log file for appending text.
        _writer = new StreamWriter(logFilePath, append: true);
    }

    // Write a single line to the log file.
    public void Log(string message)
    {
        _writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
        _writer.Flush();
    }

    public void Dispose()
    {
        _writer?.Dispose();
    }
}

// Custom callback that logs page start and end events.
class CustomPageSavingCallback : IPageSavingCallback
{
    private readonly FileLogger _logger;

    public CustomPageSavingCallback(FileLogger logger)
    {
        _logger = logger;
    }

    // Called before a page starts saving.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Log start of page saving with index and total count.
        _logger.Log($"Start saving page index {args.PageIndex} of pages {args.PageCount}");
    }

    // Called after a page has finished saving.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // Log end of page saving with index and total count.
        _logger.Log($"End saving page index {args.PageIndex} of pages {args.PageCount}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, output PDF file, log text file.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputPdfPath> <logFilePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string logPath = args[2];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Ensure the directory for the output PDF exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        // Ensure the directory for the log file exists.
        string logDir = Path.GetDirectoryName(logPath);
        if (!string.IsNullOrEmpty(logDir) && !Directory.Exists(logDir))
        {
            Console.Error.WriteLine($"Log directory does not exist: {logDir}");
            return;
        }

        // Initialize the logger.
        using (var logger = new FileLogger(logPath))
        {
            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(inputPath);

                // Configure PDF save options and assign the custom callback.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.PageSavingCallback = new CustomPageSavingCallback(logger);

                // Save the diagram as PDF while logging page events.
                diagram.Save(outputPath, pdfOptions);
            }
            catch (Exception ex)
            {
                // Write any errors to the error console.
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}