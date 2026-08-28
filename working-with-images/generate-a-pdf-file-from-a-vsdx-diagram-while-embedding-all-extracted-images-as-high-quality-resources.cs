using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input VSDX path and output PDF path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <input.vsdx> <output.pdf>");
            return;
        }

        // Input diagram file path.
        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output PDF file path.
        string outputPath = args[1];
        // Guard: ensure the output directory exists (create if missing).
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the Visio diagram from the VSDX file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate all pages and shapes to locate foreign (image) shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify image shapes by TypeValue.Foreign.
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData != null)
                    {
                        // Extract raw image bytes.
                        byte[] imageBytes = shape.ForeignData.Value;
                        // Log image extraction (size in bytes) – images will be embedded automatically in PDF.
                        Console.WriteLine($"Extracted image from shape ID {shape.ID}: {imageBytes.Length} bytes");
                    }
                }
            }

            // Configure PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // Use a common fallback font.
                DefaultFont = "Arial",
                // Do not export hidden pages.
                ExportHiddenPage = false
                // AutoFitPageToDrawingContent property is unavailable in this version; omitted.
            };

            // Assign a custom page‑saving callback to log page processing.
            pdfOptions.PageSavingCallback = new CustomPageSavingCallback();

            // Save the diagram as a PDF using the configured options.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"PDF successfully saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

// Custom callback to receive page‑saving events during PDF export.
class CustomPageSavingCallback : IPageSavingCallback
{
    // Called before a page starts saving.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}");
    }

    // Called after a page has finished saving.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished saving page {args.PageIndex + 1}");
        // Continue processing remaining pages (default behavior).
        args.HasMorePages = true;
    }
}