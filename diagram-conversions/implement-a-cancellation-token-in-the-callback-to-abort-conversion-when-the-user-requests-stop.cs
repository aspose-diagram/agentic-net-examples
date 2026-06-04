using System;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class PageSavingCallback : IPageSavingCallback
{
    private readonly CancellationToken _token;

    public PageSavingCallback(CancellationToken token)
    {
        _token = token;
    }

    // Called before a page is saved
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // No action needed at start of page saving
    }

    // Called after a page is saved
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // If cancellation is requested, stop further page processing
        if (_token.IsCancellationRequested)
        {
            args.HasMorePages = false;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Paths for input diagram and output PDF
            string inputPath = "input.vsdx";
            string outputPath = "output.pdf";

            // Load the diagram
            using var diagram = new Diagram(inputPath);

            // Prepare PDF save options
            var pdfOptions = new PdfSaveOptions();

            // Cancellation token source to allow user to stop conversion
            var cts = new CancellationTokenSource();

            // Listen for user input to trigger cancellation
            Task.Run(() =>
            {
                Console.WriteLine("Press 'c' to cancel conversion...");
                while (Console.ReadKey(true).KeyChar != 'c')
                {
                    // Wait until 'c' is pressed
                }
                cts.Cancel();
                Console.WriteLine("Cancellation requested.");
            });

            // Assign the page saving callback with the cancellation token
            pdfOptions.PageSavingCallback = new PageSavingCallback(cts.Token);

            // Perform the conversion
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine("Conversion completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}