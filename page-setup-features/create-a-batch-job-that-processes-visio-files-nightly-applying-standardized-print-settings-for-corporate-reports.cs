using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
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

        // Verify output folder exists.
        if (!Directory.Exists(outputFolder))
        {
            Console.Error.WriteLine($"Output folder does not exist: {outputFolder}");
            return;
        }

        // Retrieve all Visio files in the input folder.
        string[] files = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in files)
        {
            // Process only supported Visio extensions.
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".vsdx" && ext != ".vsd" && ext != ".vdx")
                continue;

            // Guard against missing file.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the Visio diagram inside a using block for automatic disposal.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Apply standardized print settings to every page.
                    foreach (Page page in diagram.Pages)
                    {
                        // Set page orientation to Landscape.
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                        // Set scaling to 75 % (both X and Y).
                        page.PageSheet.PrintProps.ScaleX.Value = 0.75;
                        page.PageSheet.PrintProps.ScaleY.Value = 0.75;

                        // Enable "Fit to sheet" (1 × 1 page per sheet).
                        page.PageSheet.PrintProps.OnPage.Value = BOOL.True;
                        page.PageSheet.PrintProps.PagesX.Value = 1;
                        page.PageSheet.PrintProps.PagesY.Value = 1;

                        // Set uniform margins of 0.5 inches (36 points).
                        page.PageSheet.PrintProps.PageTopMargin.Value = 36;
                        page.PageSheet.PrintProps.PageBottomMargin.Value = 36;
                        page.PageSheet.PrintProps.PageLeftMargin.Value = 36;
                        page.PageSheet.PrintProps.PageRightMargin.Value = 36;
                    }

                    // Build the output file path (preserve original name, save as VSDX).
                    string fileName = Path.GetFileNameWithoutExtension(filePath);
                    string outputPath = Path.Combine(outputFolder, fileName + ".vsdx");

                    // Save the updated diagram with the required overload.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine($"Processed and saved: {filePath}");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}