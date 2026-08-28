using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Standardized print settings applied to each page
    private static void ApplyPrintSettings(Page page)
    {
        // Orientation: Landscape
        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

        // Scaling: 75%
        page.PageSheet.PrintProps.ScaleX.Value = 0.75;
        page.PageSheet.PrintProps.ScaleY.Value = 0.75;

        // Fit to sheet: enable and set to 1x1 pages
        page.PageSheet.PrintProps.OnPage.Value = BOOL.True;
        page.PageSheet.PrintProps.PagesX.Value = 1;
        page.PageSheet.PrintProps.PagesY.Value = 1;

        // Margins: 0.5 inch (points = inches * 72)
        const double halfInchInPoints = 0.5 * 72.0;
        page.PageSheet.PrintProps.PageTopMargin.Value = halfInchInPoints;
        page.PageSheet.PrintProps.PageBottomMargin.Value = halfInchInPoints;
        page.PageSheet.PrintProps.PageLeftMargin.Value = halfInchInPoints;
        page.PageSheet.PrintProps.PageRightMargin.Value = halfInchInPoints;
    }

    static void Main()
    {
        try
        {

            // Folder containing Visio files to process
            string inputFolder = @"C:\Visio\Input";
            // Folder where processed PDFs will be saved
            string outputFolder = @"C:\Visio\Output";

            // Ensure output directory exists
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Process all supported Visio files (*.vsdx, *.vsd, *.vdx, *.vssx, etc.)
            string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in visioFiles)
            {
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                // Filter known Visio extensions
                if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx" &&
                    extension != ".vsx" && extension != ".vtx" && extension != ".vssx" &&
                    extension != ".vss" && extension != ".vstx" && extension != ".vst")
                {
                    continue;
                }

                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Apply print settings to every page
                    foreach (Page page in diagram.Pages)
                    {
                        ApplyPrintSettings(page);
                    }

                    // Prepare PDF save options with a fallback font
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.DefaultFont = "Arial";

                    // Build output PDF path (same file name, .pdf extension)
                    string outputFileName = Path.GetFileNameWithoutExtension(filePath) + ".pdf";
                    string outputPath = Path.Combine(outputFolder, outputFileName);

                    // Save as PDF
                    diagram.Save(outputPath, pdfOptions);
                }
                catch (Exception ex)
                {
                    // Log error to console and continue with next file
                    Console.WriteLine($"Error processing '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
