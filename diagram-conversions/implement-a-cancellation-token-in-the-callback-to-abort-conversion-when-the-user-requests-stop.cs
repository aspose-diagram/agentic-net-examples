using System;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CustomPageSavingCallback : IPageSavingCallback
{
    private readonly CancellationToken _cancellationToken;

    public CustomPageSavingCallback(CancellationToken cancellationToken)
    {
        _cancellationToken = cancellationToken;
    }

    // Called before a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // No action needed here for cancellation
    }

    // Called after a page has been saved
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // If cancellation is requested, stop further page processing
        if (_cancellationToken.IsCancellationRequested)
        {
            args.HasMorePages = false;
        }
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.pdf";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Set up cancellation support
            var cts = new CancellationTokenSource();

            // Start a background task to listen for user cancellation
            Task.Run(() =>
            {
                Console.WriteLine("Press 'c' then Enter to cancel the conversion...");
                while (true)
                {
                    string line = Console.ReadLine();
                    if (!string.IsNullOrEmpty(line) && line.Equals("c", StringComparison.OrdinalIgnoreCase))
                    {
                        cts.Cancel();
                        Console.WriteLine("Cancellation requested.");
                        break;
                    }
                }
            });

            // Configure PDF save options and assign the callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new CustomPageSavingCallback(cts.Token);

            // Perform the save operation
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine("Conversion completed or cancelled.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}