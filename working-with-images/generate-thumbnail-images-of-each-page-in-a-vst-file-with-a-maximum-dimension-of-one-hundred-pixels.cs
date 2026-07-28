using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the VST (stencil) file
            string vstPath = "input.vst";

            // Load the stencil diagram
            Diagram diagram = new Diagram(vstPath, LoadFileFormat.Vst);

            // Iterate through each page in the stencil
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Access the current page
                Page page = diagram.Pages[i];

                // Retrieve page dimensions (in inches)
                double pageWidthInches = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeightInches = page.PageSheet.PageProps.PageHeight.Value;

                // Assume default DPI of 96 for pixel calculation
                const double dpi = 96.0;
                double widthPixels = pageWidthInches * dpi;
                double heightPixels = pageHeightInches * dpi;

                // Determine scaling factor to keep the larger side at most 100 pixels
                double maxDimension = Math.Max(widthPixels, heightPixels);
                float scale = 1.0f;
                if (maxDimension > 100.0)
                {
                    scale = (float)(100.0 / maxDimension);
                }

                // Configure image save options for PNG thumbnail
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                saveOptions.PageIndex = i;          // Export only the current page
                saveOptions.Scale = scale;          // Apply scaling to meet max 100‑pixel constraint

                // Define output file name for the thumbnail
                string outputPath = $"thumbnail_page_{i + 1}.png";

                // Save the thumbnail image
                diagram.Save(outputPath, saveOptions);
            }

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
