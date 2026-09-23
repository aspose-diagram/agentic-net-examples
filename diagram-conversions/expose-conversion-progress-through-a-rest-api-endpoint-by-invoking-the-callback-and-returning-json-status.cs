using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramConversionDemo
{
    // Callback to track PDF page saving progress
    public class PdfProgressCallback : IPageSavingCallback
    {
        private int _currentPage;
        private int _totalPages;
        private bool _completed;

        public void PageStartSaving(PageStartSavingArgs args)
        {
            // PageIndex is zero‑based; convert to 1‑based for reporting
            _currentPage = args.PageIndex + 1;
            _totalPages = args.PageCount;
        }

        public void PageEndSaving(PageEndSavingArgs args)
        {
            // When the last page finishes, mark conversion as completed
            if (args.PageIndex == args.PageCount - 1)
            {
                _completed = true;
            }
        }

        // Returns a simple status object that can be serialized to JSON
        public object GetStatus()
        {
            return new
            {
                CurrentPage = _currentPage,
                TotalPages = _totalPages,
                Completed = _completed
            };
        }
    }

    public class Converter
    {
        // Simulates a REST endpoint: converts a Visio diagram to PDF and returns JSON status
        public static string ConvertDiagramToPdf(string inputPath, string outputPath)
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare PDF save options and attach the progress callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            PdfProgressCallback callback = new PdfProgressCallback();
            pdfOptions.PageSavingCallback = callback;

            // Perform the conversion
            diagram.Save(outputPath, pdfOptions);

            // Retrieve conversion status and serialize to JSON
            string jsonStatus = JsonSerializer.Serialize(callback.GetStatus());
            return jsonStatus;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Example file paths (adjust as needed)
            string inputFile = "sample.vsdx";
            string outputFile = "output.pdf";

            // Ensure the input file exists
            if (!File.Exists(inputFile))
            {
                Console.WriteLine($"Input file not found: {inputFile}");
                return;
            }

            // Invoke the conversion and obtain JSON status
            string statusJson = Converter.ConvertDiagramToPdf(inputFile, outputFile);

            // Output the JSON status (simulating a REST response)
            Console.WriteLine(statusJson);
        }
    }
}