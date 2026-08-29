using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path as first argument
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine output PDF path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "output.pdf";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Inspect each page for visual consistency
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double width = page.PageSheet.PageProps.PageWidth.Value;
                double height = page.PageSheet.PageProps.PageHeight.Value;

                // Log page information to the console
                Console.WriteLine($"Page ID: {page.ID}, Name: {page.Name}, Width: {width:F2} in, Height: {height:F2} in");
            }

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // Use a common fallback font
                DefaultFont = "Arial",
                // Do not export hidden pages
                ExportHiddenPage = false,
                // Explicitly set the target format (optional but safe)
                SaveFormat = SaveFileFormat.Pdf,
                // Attach custom callback to monitor per‑page saving
                PageSavingCallback = new PageSavingLogger()
            };

            // Export the entire diagram to a single PDF file
            diagram.Save(outputPath, pdfOptions);
            Console.WriteLine($"Diagram successfully exported to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}

// Custom callback to log page‑saving events during PDF export
class PageSavingLogger : IPageSavingCallback
{
    // Called before a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}");
    }

    // Called after a page has been saved
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished saving page {args.PageIndex + 1}");
        // Example: stop after first page (uncomment to enable)
        // if (args.PageIndex == 0) args.HasMorePages = false;
    }
}