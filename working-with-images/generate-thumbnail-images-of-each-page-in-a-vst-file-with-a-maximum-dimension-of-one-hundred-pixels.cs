using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input VST file> <output folder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Ensure the output directory exists.
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: Could not create output directory \"{outputFolder}\": {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the VST (stencil) file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the stencil.
            int pageIndex = 0;
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions in inches.
                double pageWidthInches = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeightInches = page.PageSheet.PageProps.PageHeight.Value;

                // Default DPI for Aspose image export is 96.
                const double dpi = 96.0;
                double pixelWidth = pageWidthInches * dpi;
                double pixelHeight = pageHeightInches * dpi;

                // Determine scaling factor so that the larger side does not exceed 100 pixels.
                double maxPixel = Math.Max(pixelWidth, pixelHeight);
                double scale = maxPixel > 100 ? 100.0 / maxPixel : 1.0;

                // Configure image save options for PNG thumbnail.
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    // Export only the current page.
                    PageIndex = pageIndex,
                    PageCount = 1,
                    // Apply the calculated scaling factor.
                    Scale = (float)scale,
                    // Do not export hidden pages.
                    ExportHiddenPage = false
                };

                // Build output file name.
                string outputPath = Path.Combine(outputFolder, $"thumbnail_page_{pageIndex + 1}.png");

                // Save the thumbnail.
                diagram.Save(outputPath, saveOptions);

                Console.WriteLine($"Generated thumbnail for page {pageIndex + 1}: {outputPath}");

                pageIndex++;
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}