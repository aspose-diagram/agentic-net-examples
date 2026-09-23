using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output directories (adjust paths as needed)
            string inputFolder = @"C:\Visio\Input";
            string outputFolder = @"C:\Visio\Output";

            // Ensure the output directory exists
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Retrieve all Visio files in the input folder
            string[] files = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in files)
            {
                // Process only supported Visio extensions
                string ext = Path.GetExtension(filePath).ToLowerInvariant();
                if (ext != ".vsdx" && ext != ".vsd" && ext != ".vdx" && ext != ".vssx" && ext != ".vstx")
                {
                    continue;
                }

                try
                {
                    // Load the Visio diagram
                    Diagram diagram = new Diagram(filePath);

                    // Apply standardized print settings to each page
                    foreach (Page page in diagram.Pages)
                    {
                        // Set orientation to Landscape
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                        // Set scaling to 75%
                        page.PageSheet.PrintProps.ScaleX.Value = 0.75;
                        page.PageSheet.PrintProps.ScaleY.Value = 0.75;

                        // Fit to a single sheet (1x1)
                        page.PageSheet.PrintProps.OnPage.Value = BOOL.True;
                        page.PageSheet.PrintProps.PagesX.Value = 1;
                        page.PageSheet.PrintProps.PagesY.Value = 1;

                        // Set uniform margins of 0.5 inch
                        double marginInches = 0.5;
                        page.PageSheet.PrintProps.PageTopMargin.Value = marginInches;
                        page.PageSheet.PrintProps.PageBottomMargin.Value = marginInches;
                        page.PageSheet.PrintProps.PageLeftMargin.Value = marginInches;
                        page.PageSheet.PrintProps.PageRightMargin.Value = marginInches;
                    }

                    // Define output PDF path
                    string fileName = Path.GetFileNameWithoutExtension(filePath);
                    string outputPath = Path.Combine(outputFolder, fileName + ".pdf");

                    // Save the diagram as PDF with a default font fallback
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.DefaultFont = "Arial";

                    diagram.Save(outputPath, pdfOptions);
                }
                catch (Exception ex)
                {
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
