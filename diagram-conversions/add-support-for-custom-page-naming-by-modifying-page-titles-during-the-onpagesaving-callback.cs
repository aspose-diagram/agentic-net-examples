using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace CustomPageNamingExample
{
    // Callback class to modify page titles during PDF saving
    public class CustomPageNamingCallback : IPageSavingCallback
    {
        private readonly Diagram _diagram;

        // Receive the diagram instance to access its pages
        public CustomPageNamingCallback(Diagram diagram)
        {
            _diagram = diagram ?? throw new ArgumentNullException(nameof(diagram));
        }

        // Called before a page is saved
        public void PageStartSaving(PageStartSavingArgs args)
        {
            // args.PageIndex is zero‑based; retrieve the corresponding page
            if (args.PageIndex >= 0 && args.PageIndex < _diagram.Pages.Count)
            {
                Page page = _diagram.Pages[args.PageIndex];
                // Set a custom name for the page
                string customName = $"CustomPage_{args.PageIndex + 1}";
                page.Name = customName;
                page.NameU = customName; // universal name
            }
        }

        // Called after a page is saved
        public void PageEndSaving(PageEndSavingArgs args)
        {
            // No additional actions needed after saving each page
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the output PDF file
                string outputPath = "output.pdf";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Create PDF save options and assign the custom callback
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.PageSavingCallback = new CustomPageNamingCallback(diagram);

                    // Save the diagram as PDF; the callback will rename pages during saving
                    diagram.Save(outputPath, pdfOptions);
                }

                Console.WriteLine("Diagram saved to PDF with custom page names.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}