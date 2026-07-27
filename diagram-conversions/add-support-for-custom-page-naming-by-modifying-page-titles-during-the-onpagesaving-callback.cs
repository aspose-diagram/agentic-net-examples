using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace CustomPageNamingExample
{
    // Callback implementation that changes the page title before each page is saved
    public class CustomPageSavingCallback : IPageSavingCallback
    {
        private readonly Diagram _diagram;

        public CustomPageSavingCallback(Diagram diagram)
        {
            _diagram = diagram;
        }

        // Called when a page starts to be saved
        public void PageStartSaving(PageStartSavingArgs args)
        {
            // Ensure the page index is within the collection bounds
            if (args.PageIndex >= 0 && args.PageIndex < _diagram.Pages.Count)
            {
                // Set a custom name for the page; this name will be used as the title in the output
                _diagram.Pages[args.PageIndex].Name = $"Custom Title {args.PageIndex + 1}";
            }

            // Keep the page in the output (default is true, but we set it explicitly)
            args.IsToOutput = true;
        }

        // Called when a page finishes saving
        public void PageEndSaving(PageEndSavingArgs args)
        {
            // No special handling needed after a page is saved; just indicate that more pages may follow
            args.HasMorePages = args.PageIndex < args.PageCount - 1;
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram(@"InputDiagram.vsdx");

                // Prepare PDF save options and attach the custom callback
                PdfSaveOptions saveOptions = new PdfSaveOptions
                {
                    PageSavingCallback = new CustomPageSavingCallback(diagram)
                };

                // Save the diagram to PDF; the callback will rename each page during the save process
                diagram.Save(@"OutputDiagram.pdf", saveOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}