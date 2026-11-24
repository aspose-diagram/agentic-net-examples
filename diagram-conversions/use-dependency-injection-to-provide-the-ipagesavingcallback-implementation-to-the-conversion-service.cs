using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramConversionExample
{
    // Custom implementation of IPageSavingCallback to log page saving events
    public class LoggingPageSavingCallback : IPageSavingCallback
    {
        // Called before a page starts saving
        public void PageStartSaving(PageStartSavingArgs args)
        {
            Console.WriteLine($"[Callback] Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
        }

        // Called after a page has been saved
        public void PageEndSaving(PageEndSavingArgs args)
        {
            Console.WriteLine($"[Callback] Finished saving page {args.PageIndex + 1} of {args.PageCount}.");

            // Example: stop processing after the first page (uncomment if needed)
            // if (args.PageIndex == 0)
            // {
            //     args.HasMorePages = false;
            // }
        }
    }

    // Service responsible for converting Visio diagrams to PDF using DI for the callback
    public class ConversionService
    {
        private readonly IPageSavingCallback _pageSavingCallback;

        // Constructor injection of the IPageSavingCallback implementation
        public ConversionService(IPageSavingCallback pageSavingCallback)
        {
            _pageSavingCallback = pageSavingCallback ?? throw new ArgumentNullException(nameof(pageSavingCallback));
        }

        // Performs the conversion from a Visio file to PDF
        public void ConvertToPdf(string inputPath, string outputPath)
        {
            // Guard: ensure the input file exists
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Configure PDF save options and assign the injected callback
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // Example: set default font (optional)
                    DefaultFont = "Arial",
                    // Assign the callback provided via DI
                    PageSavingCallback = _pageSavingCallback
                };

                // Save the diagram as PDF using the configured options
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine($"Conversion completed successfully. PDF saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during loading or saving
                Console.Error.WriteLine($"Error during conversion: {ex.Message}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output PDF file path
            if (args.Length < 2)
            {
                Console.Error.WriteLine("Usage: DiagramConversionExample <inputVisioPath> <outputPdfPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Guard: ensure the output directory exists
            string outputDir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
                return;
            }

            // Manually perform dependency injection
            IPageSavingCallback callback = new LoggingPageSavingCallback();
            ConversionService conversionService = new ConversionService(callback);

            // Execute the conversion
            conversionService.ConvertToPdf(inputPath, outputPath);
        }
    }
}