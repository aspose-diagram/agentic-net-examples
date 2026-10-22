using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the configuration file.
        const string configFileName = "appsettings.json";

        // Verify that the configuration file exists.
        if (!File.Exists(configFileName))
        {
            Console.Error.WriteLine($"Configuration file '{configFileName}' not found.");
            return;
        }

        // Load configuration JSON and retrieve the EnableProgressLogging flag.
        bool enableProgressLogging = false;
        try
        {
            string json = File.ReadAllText(configFileName);
            using JsonDocument doc = JsonDocument.Parse(json);
            if (doc.RootElement.TryGetProperty("EnableProgressLogging", out JsonElement elem) &&
                elem.ValueKind == JsonValueKind.True || elem.ValueKind == JsonValueKind.False)
            {
                enableProgressLogging = elem.GetBoolean();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        // Determine input and output file paths.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = "output.pdf";

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file '{inputPath}' not found.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            if (enableProgressLogging)
            {
                // Create PDF save options and attach the progress logging callback.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.PageSavingCallback = new ProgressLoggingCallback();

                // Save the diagram as PDF with progress logging.
                diagram.Save(outputPath, pdfOptions);
            }
            else
            {
                // Save the diagram as PDF without progress logging.
                diagram.Save(outputPath, SaveFileFormat.Pdf);
            }

            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}

// Implementation of the page saving callback that logs progress to the console.
class ProgressLoggingCallback : IPageSavingCallback
{
    // Called before a page starts saving.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"[Progress] Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
    }

    // Called after a page has been saved.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"[Progress] Finished saving page {args.PageIndex + 1} of {args.PageCount}.");
        // No need to modify args.HasMorePages; let the process continue normally.
    }
}