using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input VST file> [output folder]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output folder (optional argument). Use input file directory if not provided.
        string outputFolder = args.Length >= 2 ? args[1] : Path.GetDirectoryName(inputPath);
        if (string.IsNullOrEmpty(outputFolder))
        {
            Console.Error.WriteLine("Unable to determine output folder.");
            return;
        }

        // Ensure the output folder exists.
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

        try
        {
            // Load the VST (stencil) file.
            Diagram diagram = new Diagram(inputPath);

            // Fixed resolution (DPI) for image rendering.
            const float resolution = 96f;

            int pageCounter = 0;
            // Iterate through each page in the stencil.
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions in inches.
                double pageWidthInches = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeightInches = page.PageSheet.PageProps.PageHeight.Value;

                // Convert dimensions to pixels using the chosen resolution.
                double pixelWidth = pageWidthInches * resolution;
                double pixelHeight = pageHeightInches * resolution;

                // Determine scaling factor so that the larger side does not exceed 100 pixels.
                double maxPixelDim = Math.Max(pixelWidth, pixelHeight);
                float scale = maxPixelDim > 0 ? (float)(100.0 / maxPixelDim) : 1f;
                if (scale > 1f) scale = 1f; // Do not upscale smaller pages.

                // Configure image save options.
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    // Export only the current page.
                    PageIndex = pageCounter,
                    PageCount = 1,
                    Resolution = resolution,
                    Scale = scale
                };

                // Build output file name (e.g., page_1.png).
                string outputPath = Path.Combine(outputFolder, $"page_{pageCounter + 1}.png");

                // Save the thumbnail image.
                diagram.Save(outputPath, saveOptions);

                Console.WriteLine($"Thumbnail saved: {outputPath}");

                pageCounter++;
            }

            if (pageCounter == 0)
            {
                Console.WriteLine("No pages found in the stencil.");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}