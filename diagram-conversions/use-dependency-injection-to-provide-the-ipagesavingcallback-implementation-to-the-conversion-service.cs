using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramConversionDemo
{
    // Custom implementation of IPageSavingCallback to handle PDF page saving events
    public class CustomPageSavingCallback : IPageSavingCallback
    {
        // Called before a page starts saving
        public void PageStartSaving(PageStartSavingArgs args)
        {
            Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
        }

        // Called after a page has been saved
        public void PageEndSaving(PageEndSavingArgs args)
        {
            Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");

            // Example: stop processing after the first page
            if (args.PageIndex == 0)
            {
                args.HasMorePages = false;
                Console.WriteLine("Stopping further page processing as per callback logic.");
            }
        }
    }

    // Service that performs diagram conversion using injected IPageSavingCallback
    public class ConversionService
    {
        private readonly IPageSavingCallback _pageSavingCallback;

        public ConversionService(IPageSavingCallback pageSavingCallback)
        {
            _pageSavingCallback = pageSavingCallback ?? throw new ArgumentNullException(nameof(pageSavingCallback));
        }

        // Converts a Visio diagram to PDF using the injected callback
        public void ConvertToPdf(string inputPath, string outputPath)
        {
            if (string.IsNullOrWhiteSpace(inputPath))
                throw new ArgumentException("Input path must be provided.", nameof(inputPath));
            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("Output path must be provided.", nameof(outputPath));
            if (!File.Exists(inputPath))
                throw new FileNotFoundException("Input file not found.", inputPath);

            try
            {
                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Configure PDF save options and assign the callback
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        SaveFormat = SaveFileFormat.Pdf,
                        DefaultFont = "Arial",
                        PageSavingCallback = _pageSavingCallback
                    };

                    // Save the diagram as PDF
                    diagram.Save(outputPath, pdfOptions);
                    Console.WriteLine($"Diagram saved to PDF at: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during conversion: {ex.Message}");
            }
        }
    }

    // Application entry point
    public class Program
    {
        public static void Main(string[] args)
        {
            // Simple argument handling
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramConversionDemo <inputVisioFile> <outputPdfFile>");
                return;
            }

            string inputFile = args[0];
            if (!File.Exists(inputFile))
            {
                Console.Error.WriteLine($"File not found: {inputFile}");
                return;
            }

            string outputFile = args[1];

            // Manually inject the callback into the conversion service
            IPageSavingCallback callback = new CustomPageSavingCallback();
            ConversionService converter = new ConversionService(callback);
            converter.ConvertToPdf(inputFile, outputFile);
        }
    }
}