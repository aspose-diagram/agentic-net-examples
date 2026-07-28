using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram. Replace with your actual file path.
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages to apply consistent print settings.
            foreach (Page page in diagram.Pages)
            {
                // 1. Set page orientation.
                // Landscape often provides better utilization of printer paper width.
                page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                // 2. Define scaling factors.
                // Use 1.0 (100%) for exact size or adjust (e.g., 0.75) to shrink content.
                page.PageSheet.PrintProps.ScaleX.Value = 1.0; // 100% width
                page.PageSheet.PrintProps.ScaleY.Value = 1.0; // 100% height

                // 3. Enable "Fit to Sheet" to force the diagram onto a single printed page.
                page.PageSheet.PrintProps.OnPage.Value = BOOL.True;
                page.PageSheet.PrintProps.PagesX.Value = 1; // one sheet across
                page.PageSheet.PrintProps.PagesY.Value = 1; // one sheet down

                // 4. Set printable margins (in inches). 0.25" is a common safe margin.
                double marginInches = 0.25;
                page.PageSheet.PrintProps.PageTopMargin.Value = marginInches;
                page.PageSheet.PrintProps.PageBottomMargin.Value = marginInches;
                page.PageSheet.PrintProps.PageLeftMargin.Value = marginInches;
                page.PageSheet.PrintProps.PageRightMargin.Value = marginInches;
            }

            // Save the updated diagram to PDF.
            // PdfSaveOptions allows us to specify a fallback font for characters not present on the printer.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            pdfOptions.ExportHiddenPage = false; // Exclude hidden pages from the print output.

            diagram.Save("output.pdf", pdfOptions);

            // Clean up resources.
            diagram.Dispose();

            Console.WriteLine("Print configuration applied and diagram saved as PDF.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
