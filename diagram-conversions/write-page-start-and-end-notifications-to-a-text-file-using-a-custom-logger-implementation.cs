using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Logger : IDisposable
{
    private readonly StreamWriter _writer;

    // Initialize the logger with the path to the log file.
    public Logger(string logFilePath)
    {
        // Ensure the directory for the log file exists.
        string directory = Path.GetDirectoryName(logFilePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        _writer = new StreamWriter(logFilePath, append: true);
    }

    // Write a single line to the log file with a timestamp.
    public void Log(string message)
    {
        _writer.WriteLine($"{DateTime.Now:O} {message}");
        _writer.Flush();
    }

    // Dispose the underlying StreamWriter.
    public void Dispose()
    {
        _writer?.Dispose();
    }
}

// Custom callback that logs page start and end events.
class PageSavingLogger : IPageSavingCallback
{
    private readonly Logger _logger;

    public PageSavingLogger(Logger logger)
    {
        _logger = logger;
    }

    // Called before a page starts saving.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        _logger.Log($"Start saving page index {args.PageIndex} of pages {args.PageCount}");
    }

    // Called after a page has finished saving.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        _logger.Log($"End saving page index {args.PageIndex} of pages {args.PageCount}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Define default input, output, and log file paths.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = "output.pdf";
        string logPath = "conversion_log.txt";

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the logger.
        using (Logger logger = new Logger(logPath))
        {
            try
            {
                // Load the Visio diagram inside a using block as required.
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Configure PDF save options and assign the custom page-saving callback.
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.PageSavingCallback = new PageSavingLogger(logger);

                    // Save the diagram to PDF using the configured options.
                    diagram.Save(outputPath, pdfOptions);
                }

                // Log successful completion.
                logger.Log("Diagram conversion completed successfully.");
            }
            catch (Exception ex)
            {
                // Write any errors to the error console and also log them.
                Console.Error.WriteLine($"Error during conversion: {ex.Message}");
                logger.Log($"Error during conversion: {ex}");
            }
        }
    }
}