using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine the VST file path from the first argument or use a default name.
        string vstPath = args.Length > 0 ? args[0] : "stencil.vst";
        // Verify that the VST file exists before proceeding.
        if (!File.Exists(vstPath))
        {
            Console.Error.WriteLine($"File not found: {vstPath}");
            return;
        }

        try
        {
            // Load the stencil (VST) file into a Diagram object.
            Diagram diagram = new Diagram(vstPath);

            // Define the maximum pixel dimension for thumbnails.
            const int maxPixelSize = 100;

            // Iterate over each page in the stencil.
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Retrieve the current page.
                Page page = diagram.Pages[i];

                // Obtain page width and height in inches.
                double pageWidthInches = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeightInches = page.PageSheet.PageProps.PageHeight.Value;

                // Choose a resolution (DPI) for rendering; 96 DPI is a common default.
                const float resolutionDpi = 96f;

                // Compute the scale factor so that the larger side becomes maxPixelSize pixels.
                double maxInches = Math.Max(pageWidthInches, pageHeightInches);
                float scale = (float)(maxPixelSize / (maxInches * resolutionDpi));

                // Configure image save options for PNG output.
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    // Set the page index to render the current page.
                    PageIndex = i,
                    // Apply the calculated scale to fit within the pixel limit.
                    Scale = scale,
                    // Use the chosen resolution.
                    Resolution = resolutionDpi,
                    // Export only the current page.
                    ExportHiddenPage = false
                };

                // Build the output file name for the thumbnail.
                string outputFile = $"thumbnail_page_{i + 1}.png";

                // Save the rendered page as a PNG thumbnail.
                diagram.Save(outputFile, saveOptions);

                // Inform the user about the generated thumbnail.
                Console.WriteLine($"Generated thumbnail: {outputFile}");
            }
        }
        catch (Exception ex)
        {
            // Write any exceptions to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}