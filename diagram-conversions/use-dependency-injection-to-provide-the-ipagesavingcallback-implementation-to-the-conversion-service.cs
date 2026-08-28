using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramConversionExample
{
    // Custom implementation of IPageSavingCallback to receive page saving events.
    public class MyPageSavingCallback : IPageSavingCallback
    {
        // Called before a page starts saving.
        public void PageStartSaving(PageStartSavingArgs args)
        {
            Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
        }

        // Called after a page has been saved.
        public void PageEndSaving(PageEndSavingArgs args)
        {
            Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");

            // Example: stop processing after the first page.
            // args.HasMorePages = false;
        }
    }

    // Service that performs diagram conversion using a provided IPageSavingCallback.
    public class DiagramConversionService
    {
        private readonly IPageSavingCallback _callback;

        // Dependency injection via constructor.
        public DiagramConversionService(IPageSavingCallback callback)
        {
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }

        // Converts the given diagram to PDF, applying the injected callback.
        public void Convert(Diagram diagram, string outputPath)
        {
            if (diagram == null) throw new ArgumentNullException(nameof(diagram));
            if (string.IsNullOrWhiteSpace(outputPath)) throw new ArgumentException("Output path must be provided.", nameof(outputPath));

            // Configure PDF save options and assign the callback.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = _callback;

            // Save the diagram as PDF using the options.
            diagram.Save(outputPath, pdfOptions);
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source Visio file and the desired PDF output.
                string inputPath = "input.vsdx";
                string outputPath = "output.pdf";

                // Load the diagram inside a using block to ensure proper disposal.
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Instantiate the conversion service with the custom callback via DI.
                    DiagramConversionService conversionService = new DiagramConversionService(new MyPageSavingCallback());

                    // Perform the conversion.
                    conversionService.Convert(diagram, outputPath);
                }

                Console.WriteLine("Conversion completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}