using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramPageLogging
{
    // Custom logger that implements IPageSavingCallback
    // Writes start/end notifications to a text file.
    public class PageLogger : IPageSavingCallback, IDisposable
    {
        private readonly StreamWriter _writer;

        // Constructor receives the path of the log file.
        public PageLogger(string logFilePath)
        {
            // Append to existing log file, create if it does not exist.
            _writer = new StreamWriter(logFilePath, append: true);
        }

        // Called when a page starts saving.
        public void PageStartSaving(PageStartSavingArgs args)
        {
            // Log page index (zero‑based) and total page count.
            _writer.WriteLine($"Page start: Index={args.PageIndex}, Total={args.PageCount}");
            _writer.Flush();
        }

        // Called when a page finishes saving.
        public void PageEndSaving(PageEndSavingArgs args)
        {
            // Log page index, total count and whether more pages follow.
            _writer.WriteLine($"Page end: Index={args.PageIndex}, Total={args.PageCount}, HasMorePages={args.HasMorePages}");
            _writer.Flush();
        }

        // Dispose the StreamWriter when done.
        public void Dispose()
        {
            _writer?.Dispose();
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load the diagram (replace with your actual file).
                Diagram diagram = new Diagram("input.vsdx");

                // Prepare PDF save options and attach the custom logger.
                PdfSaveOptions saveOptions = new PdfSaveOptions();

                // Use the logger to capture page start/end events.
                using (PageLogger logger = new PageLogger("PageSavingLog.txt"))
                {
                    saveOptions.PageSavingCallback = logger;

                    // Save the diagram to PDF; the logger will be invoked for each page.
                    diagram.Save("output.pdf", saveOptions);
                }

                // At this point the logger has been disposed and the log file is complete.

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}