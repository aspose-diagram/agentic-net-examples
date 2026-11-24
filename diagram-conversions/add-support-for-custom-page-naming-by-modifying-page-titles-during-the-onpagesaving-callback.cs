using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CustomPageNamingCallback : IPageSavingCallback
{
    private readonly Diagram _diagram;

    // Constructor receives the diagram to allow page modifications.
    public CustomPageNamingCallback(Diagram diagram)
    {
        _diagram = diagram;
    }

    // Called before a page starts saving.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Retrieve the page based on the current index.
        // Page collection is zero‑based.
        if (args.PageIndex >= 0 && args.PageIndex < _diagram.Pages.Count)
        {
            Page page = _diagram.Pages[args.PageIndex];
            // Set a custom name for the page.
            page.Name = $"Custom Page {args.PageIndex + 1}";
        }
    }

    // Called after a page has been saved.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // No additional actions needed after saving each page.
        // This method must be present to satisfy the interface.
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default).
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Output PDF file path (second argument or default).
        string outputPath = args.Length > 1 ? args[1] : "output.pdf";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the diagram inside a using block as required.
        using (Diagram diagram = new Diagram(inputPath))
        {
            try
            {
                // Create PDF save options and assign the custom callback.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.PageSavingCallback = new CustomPageNamingCallback(diagram);

                // Save the diagram to PDF using the options.
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine($"PDF saved successfully to: {outputPath}");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing.
                Console.Error.WriteLine($"Error during PDF conversion: {ex.Message}");
            }
        }
    }
}