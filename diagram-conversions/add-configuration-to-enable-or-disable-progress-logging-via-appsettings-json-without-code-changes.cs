using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramProgressLogging
{
    // Custom callback to log page saving progress
    public class ProgressLoggingCallback : IPageSavingCallback
    {
        private readonly bool _enableLogging;

        public ProgressLoggingCallback(bool enableLogging)
        {
            _enableLogging = enableLogging;
        }

        public void PageStartSaving(PageStartSavingArgs args)
        {
            // Log start of each page if logging is enabled
            if (_enableLogging)
            {
                Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
            }
        }

        public void PageEndSaving(PageEndSavingArgs args)
        {
            // Log end of each page if logging is enabled
            if (_enableLogging)
            {
                Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Path to optional configuration file
            string configPath = "appsettings.json";

            // Default to disabled logging
            bool enableProgressLogging = false;

            // Attempt to read configuration if file exists
            if (File.Exists(configPath))
            {
                try
                {
                    // Parse JSON and extract the EnableProgressLogging flag
                    string json = File.ReadAllText(configPath);
                    using JsonDocument doc = JsonDocument.Parse(json);
                    if (doc.RootElement.TryGetProperty("EnableProgressLogging", out JsonElement elem) &&
                        elem.ValueKind == JsonValueKind.True)
                    {
                        enableProgressLogging = true;
                    }
                }
                catch (Exception ex)
                {
                    // Report any errors while reading config but continue with defaults
                    Console.Error.WriteLine($"Error reading config: {ex.Message}");
                }
            }

            // Paths to input diagram and output PDF
            string inputPath = "input.vsdx";
            // Guard: ensure input file exists before proceeding
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            string outputPath = "output.pdf";

            try
            {
                // Load the diagram from the input file
                Diagram diagram = new Diagram(inputPath);

                // Set up PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Assign the progress logging callback if enabled
                if (enableProgressLogging)
                {
                    pdfOptions.PageSavingCallback = new ProgressLoggingCallback(true);
                }

                // Save the diagram as PDF using the configured options
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine("Diagram saved successfully.");
            }
            catch (Exception ex)
            {
                // Capture any Aspose or I/O errors and report them
                Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            }
        }
    }
}