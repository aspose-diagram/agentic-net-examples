using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramPageCountValidation
{
    // Custom callback to validate page count during PDF saving
    public class PageCountValidator : IPageSavingCallback
    {
        private readonly Diagram _diagram;

        public PageCountValidator(Diagram diagram)
        {
            _diagram = diagram ?? throw new ArgumentNullException(nameof(diagram));
        }

        // Called before each page is saved
        public void PageStartSaving(PageStartSavingArgs args)
        {
            // args.PageCount is the total number of pages reported by the callback
            int reportedCount = args.PageCount;
            int actualCount = _diagram.Pages.Count;

            if (reportedCount != actualCount)
            {
                throw new Exception($"Page count mismatch: reported {reportedCount}, actual {actualCount}.");
            }

            // Optional: log successful validation for the current page
            Console.WriteLine($"Page {args.PageIndex + 1}/{reportedCount} validated successfully.");
        }

        // Called after each page is saved
        public void PageEndSaving(PageEndSavingArgs args)
        {
            // No additional validation needed here
        }
    }

    public class Program
    {
        public static void Main()
        {
            // Path to the input Visio file (replace with actual file path)
            string inputPath = "input.vsdx";

            // Path to the output PDF file
            string outputPath = "output.pdf";

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Create the custom callback, passing the loaded diagram
                var validator = new PageCountValidator(diagram);

                // Configure PDF save options and assign the callback
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.PageSavingCallback = validator;
                pdfOptions.SaveFormat = SaveFileFormat.Pdf; // Ensure correct format

                // Save the diagram as PDF (triggers the callback)
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine("Diagram saved successfully and page count validated.");
            }
            catch (Exception ex)
            {
                // Report any errors, including page count mismatches
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}