using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class PdfPageCallback : IPageSavingCallback
{
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
    }

    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished saving page {args.PageIndex + 1}.");
        // Example: stop after first page (not required, just demonstration)
        // args.HasMorePages = false;
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output PDF file path
            string outputPath = "output.pdf";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Apply a preset theme to each page
            foreach (Page page in diagram.Pages)
            {
                // Apply the Bubble theme to the page
                page.PresetTheme = PresetThemeValue.Bubble;
                // Optionally set a variant
                page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            }

            // Verify visual consistency: check that all pages have the same dimensions
            double firstWidth = diagram.Pages[0].PageSheet.PageProps.PageWidth.Value;
            double firstHeight = diagram.Pages[0].PageSheet.PageProps.PageHeight.Value;

            for (int i = 1; i < diagram.Pages.Count; i++)
            {
                Page p = diagram.Pages[i];
                double w = p.PageSheet.PageProps.PageWidth.Value;
                double h = p.PageSheet.PageProps.PageHeight.Value;

                if (Math.Abs(w - firstWidth) > 0.001 || Math.Abs(h - firstHeight) > 0.001)
                {
                    throw new Exception($"Page {p.Name} dimensions ({w}x{h}) differ from first page ({firstWidth}x{firstHeight}).");
                }
            }

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            pdfOptions.ExportHiddenPage = false;
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;
            pdfOptions.PageSavingCallback = new PdfPageCallback();

            // Save the diagram as PDF
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}