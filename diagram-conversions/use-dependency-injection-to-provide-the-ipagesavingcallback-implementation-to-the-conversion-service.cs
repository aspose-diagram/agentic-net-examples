using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramConversionApp
{
    // Custom implementation of IPageSavingCallback to receive page saving events.
    public class CustomPageSavingCallback : IPageSavingCallback
    {
        // Called before a page starts saving.
        public void PageStartSaving(PageStartSavingArgs args)
        {
            Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
        }

        // Called after a page has been saved.
        public void PageEndSaving(PageEndSavingArgs args)
        {
            Console.WriteLine($"Finished saving page {args.PageIndex + 1}.");
            // Example: stop processing after the first page.
            // if (args.PageIndex == 0) args.HasMorePages = false;
        }
    }

    // Service that performs diagram conversion using the injected callback.
    public class DiagramConversionService
    {
        private readonly IPageSavingCallback _callback;

        // Callback is provided via constructor injection.
        public DiagramConversionService(IPageSavingCallback callback)
        {
            _callback = callback;
        }

        // Converts a Visio file to PDF, applying the page saving callback.
        public void ConvertToPdf(string inputPath, string outputPath)
        {
            // Load the diagram from file.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options and assign the callback.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.PageSavingCallback = _callback;

                // Save the diagram as PDF using the options.
                diagram.Save(outputPath, pdfOptions);
            }
        }
    }

    // Entry point of the console application.
    public class Program
    {
        public static void Main(string[] args)
        {
            // Expect input and output file paths as arguments.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramConversionApp <input.vsdx> <output.pdf>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Manual dependency injection: create the callback and pass it to the service.
            IPageSavingCallback callback = new CustomPageSavingCallback();
            DiagramConversionService conversionService = new DiagramConversionService(callback);

            // Perform the conversion.
            conversionService.ConvertToPdf(inputPath, outputPath);

            Console.WriteLine("Conversion completed.");
        }
    }
}