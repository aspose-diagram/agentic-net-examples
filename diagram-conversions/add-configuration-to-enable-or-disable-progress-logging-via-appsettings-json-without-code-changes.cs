using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Entry point of the console application
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output PDF file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load configuration from appsettings.json (optional)
        bool enableProgressLogging = LoadProgressLoggingSetting();

        Diagram diagram = null;
        try
        {
            // Load the Visio diagram
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Prepare PDF save options
        PdfSaveOptions pdfOptions = new PdfSaveOptions();

        // If progress logging is enabled, assign a custom callback
        if (enableProgressLogging)
        {
            pdfOptions.PageSavingCallback = new ProgressLoggingCallback();
        }

        try
        {
            // Save the diagram as PDF using the configured options
            diagram.Save(outputPath, pdfOptions);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }

    // Reads the "EnableProgressLogging" flag from appsettings.json.
    // Returns false if the file or the setting is missing or invalid.
    private static bool LoadProgressLoggingSetting()
    {
        const string configFile = "appsettings.json";

        if (!File.Exists(configFile))
        {
            // Configuration file not found; default to disabled logging
            return false;
        }

        try
        {
            string json = File.ReadAllText(configFile);
            using (JsonDocument doc = JsonDocument.Parse(json))
            {
                JsonElement root = doc.RootElement;

                if (root.TryGetProperty("EnableProgressLogging", out JsonElement element) &&
                    element.ValueKind == JsonValueKind.True ||
                    element.ValueKind == JsonValueKind.False)
                {
                    return element.GetBoolean();
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading configuration: {ex.Message}");
        }

        // If any error occurs, fallback to disabled logging
        return false;
    }
}

// Custom callback that logs page saving progress to the console.
class ProgressLoggingCallback : IPageSavingCallback
{
    // Called before a page starts saving.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}...");
    }

    // Called after a page has been saved.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");
        // Example: stop further processing after the first page (optional)
        // args.HasMorePages = false;
    }
}