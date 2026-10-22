using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CustomPageNamingCallback : IPageSavingCallback
{
    private readonly Diagram _diagram;

    public CustomPageNamingCallback(Diagram diagram)
    {
        _diagram = diagram;
    }

    // Called before a page starts saving to PDF
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Ensure the page index is valid
        if (args.PageIndex >= 0 && args.PageIndex < _diagram.Pages.Count)
        {
            // Set a custom name for the page
            _diagram.Pages[args.PageIndex].Name = $"Custom Page {args.PageIndex + 1}";
        }
    }

    // Called after a page has been saved to PDF
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // No additional actions needed after saving each page
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare PDF save options and assign the custom page‑saving callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new CustomPageNamingCallback(diagram);

            // Save the diagram to PDF using the options
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}