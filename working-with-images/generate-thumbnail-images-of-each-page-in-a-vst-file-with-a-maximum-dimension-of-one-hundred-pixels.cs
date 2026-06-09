using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the VST (stencil) file
            const string vstPath = "stencil.vst";

            // Maximum dimension (width or height) for the thumbnail in pixels
            const int maxPixelSize = 100;

            // Load the stencil diagram
            using (Diagram diagram = new Diagram(vstPath))
            {
                // Iterate through each page in the stencil
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Retrieve the current page
                    Page page = diagram.Pages[i];

                    // Page dimensions are stored in inches
                    double pageWidthInches = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeightInches = page.PageSheet.PageProps.PageHeight.Value;

                    // Convert dimensions to pixels (assuming 96 DPI)
                    const double dpi = 96.0;
                    double widthPixels = pageWidthInches * dpi;
                    double heightPixels = pageHeightInches * dpi;

                    // Determine the scaling factor so that the larger side becomes maxPixelSize
                    double maxCurrentPixels = Math.Max(widthPixels, heightPixels);
                    float scale = (float)(maxPixelSize / maxCurrentPixels);

                    // Configure image save options
                    ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    saveOptions.PageIndex = i;          // Export the specific page
                    saveOptions.Scale = scale;          // Apply scaling to meet the size constraint

                    // Output file name for the thumbnail
                    string outputPath = $"thumbnail_page_{i + 1}.png";

                    // Save the thumbnail image
                    diagram.Save(outputPath, saveOptions);
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
