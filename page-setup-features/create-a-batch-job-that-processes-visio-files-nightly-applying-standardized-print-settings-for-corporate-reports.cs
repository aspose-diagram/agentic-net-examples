using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Folder containing source Visio files
            string sourceFolder = @"C:\Visio\Input";
            // Folder where processed PDFs will be saved
            string outputFolder = @"C:\Visio\Output";

            // Ensure output folder exists
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Process all Visio files (VSDX, VSD, VDX, etc.) in the source folder
            string[] visioFiles = Directory.GetFiles(sourceFolder, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in visioFiles)
            {
                // Filter supported Visio extensions
                string ext = Path.GetExtension(filePath).ToLowerInvariant();
                if (ext != ".vsdx" && ext != ".vsd" && ext != ".vdx")
                {
                    continue;
                }

                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Apply standardized print settings to each page
                    foreach (Page page in diagram.Pages)
                    {
                        // Access the PrintProps cell collection
                        var printProps = page.PageSheet.PrintProps;

                        // Set orientation to Landscape
                        printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                        // Set scaling to 75%
                        printProps.ScaleX.Value = 0.75;
                        printProps.ScaleY.Value = 0.75;

                        // Enable Fit to Sheet (single page)
                        printProps.OnPage.Value = BOOL.True;
                        printProps.PagesX.Value = 1;
                        printProps.PagesY.Value = 1;

                        // Set margins (0.5 inch on all sides)
                        double marginInInches = 0.5;
                        printProps.PageTopMargin.Value = marginInInches;
                        printProps.PageBottomMargin.Value = marginInInches;
                        printProps.PageLeftMargin.Value = marginInInches;
                        printProps.PageRightMargin.Value = marginInInches;
                    }

                    // Prepare PDF save options
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.DefaultFont = "Arial"; // Fallback font for missing characters

                    // Build output file path (same name, .pdf extension)
                    string outputFileName = Path.GetFileNameWithoutExtension(filePath) + ".pdf";
                    string outputPath = Path.Combine(outputFolder, outputFileName);

                    // Save the diagram as PDF with the applied print settings
                    diagram.Save(outputPath, pdfOptions);
                }
                catch (Exception ex)
                {
                    // Log error to console; in production replace with proper logging
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
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
