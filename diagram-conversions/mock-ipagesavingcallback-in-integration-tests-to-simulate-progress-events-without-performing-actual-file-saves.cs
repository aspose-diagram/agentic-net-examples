using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class MockPageSavingCallback : IPageSavingCallback
{
    // Called before a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"[Callback] Start saving page {args.PageIndex + 1} of {args.PageCount}");
    }

    // Called after a page has been saved
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"[Callback] End saving page {args.PageIndex + 1} of {args.PageCount}");
        // Example: stop further processing after the first page (for test purposes)
        if (args.PageIndex >= 0)
        {
            args.HasMorePages = false;
            Console.WriteLine("[Callback] Requested to stop further page processing.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Path to a sample Visio file – adjust as needed for your environment
        string diagramPath = "sample.vsdx";
        if (!System.IO.File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Input file not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(diagramPath);

            // Create PDF save options and assign the mock callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new MockPageSavingCallback();

            // NOTE: We intentionally do NOT call diagram.Save(...) here.
            // The purpose is to demonstrate how the callback can be mocked
            // and injected without performing an actual file save.
            Console.WriteLine("Mock IPageSavingCallback has been configured. In integration tests, Aspose.Diagram will invoke the callback during PDF export.");

            // OPTIONAL: Simulate callback invocation manually (purely for illustration)
            // Since the actual args constructors may not be publicly accessible,
            // this block simply logs what would happen.
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine($"[Simulation] Would invoke PageStartSaving for page {i + 1}");
                Console.WriteLine($"[Simulation] Would invoke PageEndSaving for page {i + 1}");
            }

            // Clean up resources
            diagram.Dispose();
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}