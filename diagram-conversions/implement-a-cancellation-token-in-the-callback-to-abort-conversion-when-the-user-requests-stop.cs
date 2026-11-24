using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
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

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a cancellation token source that can be triggered by the user.
        var cts = new CancellationTokenSource();

        // Start a background task that listens for a "stop" command from the console.
        Task.Run(() =>
        {
            Console.WriteLine("Type 'stop' and press Enter to abort the conversion...");
            while (!cts.IsCancellationRequested)
            {
                string line = Console.ReadLine();
                if (line != null && line.Trim().Equals("stop", StringComparison.OrdinalIgnoreCase))
                {
                    cts.Cancel();
                    Console.WriteLine("Cancellation requested. The conversion will stop after the current page.");
                    break;
                }
            }
        });

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options and assign the custom page‑saving callback.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new CancelablePageSavingCallback(cts.Token);

            // Save the diagram to PDF using the configured options.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram successfully saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or saving.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

/// <summary>
/// Implements IPageSavingCallback to allow cancellation of the PDF conversion
/// based on a CancellationToken supplied by the user.
/// </summary>
class CancelablePageSavingCallback : IPageSavingCallback
{
    private readonly CancellationToken _token;

    public CancelablePageSavingCallback(CancellationToken token)
    {
        _token = token;
    }

    // Called before a page starts saving.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
    }

    // Called after a page has been saved.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");

        // If cancellation was requested, stop further page processing.
        if (_token.IsCancellationRequested)
        {
            args.HasMorePages = false; // Abort remaining pages.
            Console.WriteLine("Conversion aborted by user request.");
        }
    }
}