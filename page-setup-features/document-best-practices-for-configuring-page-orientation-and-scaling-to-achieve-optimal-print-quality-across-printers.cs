using System;
using Aspose.Diagram;
using Aspose.Diagram.Printing;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages to apply consistent print settings
                foreach (Page page in diagram.Pages)
                {
                    // ----- Page Orientation -----
                    // Use Landscape for wider diagrams; Portrait for taller ones.
                    // Landscape often yields better utilization of printer paper width.
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                    // ----- Scaling -----
                    // Scale to 75% of the original size to fit more content while preserving readability.
                    // Values are fractional (1.0 = 100%). Adjust per printer DPI and content density.
                    page.PageSheet.PrintProps.ScaleX.Value = 0.75;
                    page.PageSheet.PrintProps.ScaleY.Value = 0.75;

                    // ----- Fit to Sheet (optional) -----
                    // Enable "Fit to sheet" to force the diagram onto a single printed page.
                    // This is useful when the target printer has limited paper size.
                    page.PageSheet.PrintProps.OnPage.Value = BOOL.True; // Enable fit-to-page
                    page.PageSheet.PrintProps.PagesX.Value = 1;        // Number of pages horizontally
                    page.PageSheet.PrintProps.PagesY.Value = 1;        // Number of pages vertically

                    // ----- Margins -----
                    // Set small margins (0.25 inches) to maximize printable area.
                    // Margins are in inches.
                    page.PageSheet.PrintProps.PageTopMargin.Value = 0.25;
                    page.PageSheet.PrintProps.PageBottomMargin.Value = 0.25;
                    page.PageSheet.PrintProps.PageLeftMargin.Value = 0.25;
                    page.PageSheet.PrintProps.PageRightMargin.Value = 0.25;
                }

                // Save the updated diagram to PDF for printing verification
                string outputPath = "output.pdf";
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial"; // Fallback font for Unicode characters
                diagram.Save(outputPath, pdfOptions);

                // Clean up resources
                diagram.Dispose();

                Console.WriteLine("Print configuration applied and diagram saved to PDF.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }