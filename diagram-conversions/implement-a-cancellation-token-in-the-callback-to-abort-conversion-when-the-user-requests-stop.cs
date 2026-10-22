using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputPdfFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Create a cancellation token source to allow user‑initiated abort.
        CancellationTokenSource cts = new CancellationTokenSource();

        // Start a background thread that listens for user input to cancel the operation.
        Thread cancelThread = new Thread(() =>
        {
            Console.WriteLine("Press 'c' and Enter to cancel the conversion at any time...");
            while (true)
            {
                string line = Console.ReadLine();
                if (line != null && line.Equals("c", StringComparison.OrdinalIgnoreCase))
                {
                    cts.Cancel();
                    Console.WriteLine("Cancellation requested.");
                    break;
                }
            }
        });
        cancelThread.IsBackground = true;
        cancelThread.Start();

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options and assign the custom page‑saving callback.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new CancelPageSavingCallback(cts.Token);

            // Perform the conversion; the callback will monitor the cancellation token.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Conversion completed successfully. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}

/// <summary>
/// Implements IPageSavingCallback to allow cancellation of PDF conversion based on a CancellationToken.
/// </summary>
class CancelPageSavingCallback : IPageSavingCallback
{
    private readonly CancellationToken _cancellationToken;

    public CancelPageSavingCallback(CancellationToken cancellationToken)
    {
        _cancellationToken = cancellationToken;
    }

    // Called before a page starts saving; no cancellation logic needed here.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // No action required.
    }

    // Called after a page has been saved; abort further pages if cancellation is requested.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        if (_cancellationToken.IsCancellationRequested)
        {
            // Instruct Aspose.Diagram to stop processing further pages.
            args.HasMorePages = false;
        }
    }
}