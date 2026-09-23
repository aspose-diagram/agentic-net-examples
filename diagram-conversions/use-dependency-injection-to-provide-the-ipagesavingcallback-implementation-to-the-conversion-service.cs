using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramConversion
{
    // Implementation of the page saving callback
    public class CustomPageSavingCallback : IPageSavingCallback
    {
        public void PageStartSaving(PageStartSavingArgs args)
        {
            Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}");
        }

        public void PageEndSaving(PageEndSavingArgs args)
        {
            Console.WriteLine($"Finished saving page {args.PageIndex + 1}");
            // Example: stop after the first page
            // if (args.PageIndex == 0) args.HasMorePages = false;
        }
    }

    // Service that performs diagram conversion using DI
    public class DiagramConversionService
    {
        private readonly IPageSavingCallback _callback;

        public DiagramConversionService(IPageSavingCallback callback)
        {
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }

        public void ConvertToPdf(string inputPath, string outputPath)
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options and assign the callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = _callback;

            // Save the diagram as PDF
            diagram.Save(outputPath, pdfOptions);
        }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputFile = "sample.vsdx";
                string outputFile = "sample.pdf";

                // Manual dependency injection
                IPageSavingCallback callback = new CustomPageSavingCallback();
                DiagramConversionService conversionService = new DiagramConversionService(callback);

                // Perform the conversion
                conversionService.ConvertToPdf(inputFile, outputFile);

                Console.WriteLine("Conversion completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}