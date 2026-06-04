using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramPageLogging
{
    // Custom logger that implements IPageSavingCallback
    public class FilePageSavingLogger : IPageSavingCallback
    {
        private readonly string _logFilePath;

        public FilePageSavingLogger(string logFilePath)
        {
            _logFilePath = logFilePath;

            // Ensure the log file exists and is empty at start
            File.WriteAllText(_logFilePath, string.Empty);
        }

        // Called when a page starts saving
        public void PageStartSaving(PageStartSavingArgs args)
        {
            string message = $"Page {args.PageIndex + 1}/{args.PageCount} start saving at {DateTime.Now:O}";
            AppendLog(message);
        }

        // Called when a page finishes saving
        public void PageEndSaving(PageEndSavingArgs args)
        {
            string message = $"Page {args.PageIndex + 1}/{args.PageCount} end saving at {DateTime.Now:O}";
            AppendLog(message);
        }

        // Helper to append a line to the log file
        private void AppendLog(string text)
        {
            // Use a thread‑safe append operation
            File.AppendAllLines(_logFilePath, new[] { text });
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Configure PDF save options and attach the custom logger
                PdfSaveOptions saveOptions = new PdfSaveOptions
                {
                    PageSavingCallback = new FilePageSavingLogger("PageSaveLog.txt")
                };

                // Save the diagram to PDF; the logger will receive start/end callbacks for each page
                diagram.Save("output.pdf", saveOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}