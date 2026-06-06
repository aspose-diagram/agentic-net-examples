using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input folder containing Visio files
            string inputFolder = @"C:\Visio\Input";
            // Output folder for generated PDFs
            string outputFolder = @"C:\Visio\Output";

            // Ensure the output directory exists
            Directory.CreateDirectory(outputFolder);

            // Retrieve all Visio files (adjust pattern if other formats are needed)
            string[] visioFiles = Directory.GetFiles(inputFolder, "*.vsdx");

            foreach (string filePath in visioFiles)
            {
                try
                {
                    // Load the Visio diagram
                    Diagram diagram = new Diagram(filePath);

                    // Apply standardized print settings to each page
                    foreach (Page page in diagram.Pages)
                    {
                        var printProps = page.PageSheet.PrintProps;

                        // Orientation: Landscape
                        printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                        // Scaling: 75%
                        printProps.ScaleX.Value = 0.75;
                        printProps.ScaleY.Value = 0.75;

                        // Fit to sheet: 1x1 page
                        printProps.OnPage.Value = BOOL.True;
                        printProps.PagesX.Value = 1;
                        printProps.PagesY.Value = 1;

                        // Margins: 1 inch on each side
                        double marginInches = 1.0;
                        printProps.PageTopMargin.Value = marginInches;
                        printProps.PageBottomMargin.Value = marginInches;
                        printProps.PageLeftMargin.Value = marginInches;
                        printProps.PageRightMargin.Value = marginInches;
                    }

                    // Export the updated diagram to PDF with a default font fallback
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                    string pdfPath = Path.Combine(outputFolder, fileNameWithoutExt + ".pdf");
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.DefaultFont = "Arial";

                    diagram.Save(pdfPath, pdfOptions);

                    // Optional: overwrite the original file with updated print settings
                    // diagram.Save(filePath, SaveFileFormat.Vsdx);
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
