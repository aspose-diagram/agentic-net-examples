using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramProcessing
{
    // Implements progress logging for PDF page saving.
    public class ProgressLoggingCallback : IPageSavingCallback
    {
        public void PageStartSaving(PageStartSavingArgs args)
        {
            Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
        }

        public void PageEndSaving(PageEndSavingArgs args)
        {
            Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load configuration from appsettings.json (expects a boolean property "EnableProgressLogging").
                bool enableProgressLogging = false;
                const string configFileName = "appsettings.json";

                if (File.Exists(configFileName))
                {
                    try
                    {
                        string json = File.ReadAllText(configFileName);
                        using JsonDocument doc = JsonDocument.Parse(json);
                        if (doc.RootElement.TryGetProperty("EnableProgressLogging", out JsonElement element) &&
                            element.ValueKind == JsonValueKind.True || element.ValueKind == JsonValueKind.False)
                        {
                            enableProgressLogging = element.GetBoolean();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to read configuration: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine($"Configuration file '{configFileName}' not found. Progress logging disabled by default.");
                }

                // Load the diagram.
                const string inputDiagramPath = "input.vsdx";
                Diagram diagram = new Diagram(inputDiagramPath);

                // Prepare PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Assign progress logging callback if enabled.
                if (enableProgressLogging)
                {
                    pdfOptions.PageSavingCallback = new ProgressLoggingCallback();
                }

                // Save the diagram as PDF.
                const string outputPdfPath = "output.pdf";
                diagram.Save(outputPdfPath, pdfOptions);

                Console.WriteLine("Diagram conversion completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}