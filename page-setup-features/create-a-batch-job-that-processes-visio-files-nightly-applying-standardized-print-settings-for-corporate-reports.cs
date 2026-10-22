using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Verify input folder exists.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Create output folder if it does not exist.
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        // Get all Visio files (VSDX, VSD, VDX) in the input folder.
        string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in visioFiles)
        {
            // Process only supported Visio extensions.
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx")
            {
                continue;
            }

            // Guard: ensure the file exists before processing.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Iterate through all pages to apply standardized print settings.
                    foreach (Page page in diagram.Pages)
                    {
                        // Access the PrintProps collection for the current page.
                        PrintProps printProps = page.PageSheet.PrintProps;

                        // Set orientation to Landscape.
                        printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                        // Set scaling to 75% (both X and Y).
                        printProps.ScaleX.Value = 0.75;
                        printProps.ScaleY.Value = 0.75;

                        // Enable "Fit to sheet" (OnPage) and define a single page layout.
                        printProps.OnPage.Value = BOOL.True;
                        printProps.PagesX.Value = 1;
                        printProps.PagesY.Value = 1;

                        // Set margins (0.5 inch = 36 points) for all sides.
                        double marginPoints = 0.5 * 72.0; // 1 inch = 72 points.
                        printProps.PageTopMargin.Value = marginPoints;
                        printProps.PageBottomMargin.Value = marginPoints;
                        printProps.PageLeftMargin.Value = marginPoints;
                        printProps.PageRightMargin.Value = marginPoints;
                    }

                    // Determine output file path.
                    string fileName = Path.GetFileName(filePath);
                    string outputPath = Path.Combine(outputFolder, fileName);

                    // Save the updated diagram back to VSDX format.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine($"Processed and saved: {filePath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing of the current file.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}