using System;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramConversion
{
    // Custom callback to throttle page saving and limit CPU usage.
    public class ThrottlingPageSavingCallback : IPageSavingCallback
    {
        // Milliseconds to pause after each page is saved.
        private readonly int _delayMilliseconds;

        public ThrottlingPageSavingCallback(int delayMilliseconds = 200)
        {
            _delayMilliseconds = delayMilliseconds;
        }

        // Called before a page starts saving.
        public void PageStartSaving(PageStartSavingArgs args)
        {
            // Ensure the page will be output (default is true).
            args.IsToOutput = true;
            // Optionally, you could add logic here to skip pages on very low‑end devices.
        }

        // Called after a page has been saved.
        public void PageEndSaving(PageEndSavingArgs args)
        {
            // Introduce a pause to throttle CPU usage.
            Thread.Sleep(_delayMilliseconds);

            // Indicate whether more pages remain to be processed.
            // The default is true; we keep it unchanged.
            args.HasMorePages = true;
        }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Load the source diagram.
                Diagram diagram = new Diagram("input.vsdx");

                // Configure PDF save options with the throttling callback.
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    PageSavingCallback = new ThrottlingPageSavingCallback(delayMilliseconds: 250)
                };

                // Save the diagram to PDF using the configured options.
                diagram.Save("output.pdf", pdfOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}