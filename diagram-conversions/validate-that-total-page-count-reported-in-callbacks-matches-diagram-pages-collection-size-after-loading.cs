using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramPageCountValidation
{
    // Callback implementation to verify page count during PDF export
    public class MyPageSavingCallback : IPageSavingCallback
    {
        private readonly int _expectedPageCount;

        public MyPageSavingCallback(int expectedPageCount)
        {
            _expectedPageCount = expectedPageCount;
        }

        // Called before a page starts saving – no validation needed here
        public void PageStartSaving(PageStartSavingArgs args)
        {
            // Intentionally left blank
        }

        // Called after a page has been saved – perform the validation
        public void PageEndSaving(PageEndSavingArgs args)
        {
            if (args.PageCount != _expectedPageCount)
            {
                throw new Exception($"Page count mismatch: callback reported {args.PageCount}, but diagram contains {_expectedPageCount} pages.");
            }
        }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Path to the source Visio file (replace with an actual file path)
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Capture the expected page count from the loaded diagram
                int expectedPageCount = diagram.Pages.Count;

                // Configure PDF save options and assign the custom callback
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";
                pdfOptions.PageSavingCallback = new MyPageSavingCallback(expectedPageCount);

                // Path for the exported PDF (replace with desired output path)
                string outputPath = "output.pdf";

                // Save the diagram as PDF, triggering the callback validation
                diagram.Save(outputPath, pdfOptions);

                // If no exception was thrown, validation succeeded
                Console.WriteLine("Page count validation succeeded.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}