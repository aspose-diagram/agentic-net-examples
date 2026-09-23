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
            string vstPath = "stencil.vst";

            // Load the stencil diagram
            using (Diagram diagram = new Diagram(vstPath))
            {
                // Iterate through each page in the stencil
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    Page page = diagram.Pages[i];

                    // Retrieve page dimensions (in inches)
                    double pageWidthInches = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeightInches = page.PageSheet.PageProps.PageHeight.Value;

                    // Create image save options for PNG format
                    ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);

                    // Export only the current page
                    options.PageIndex = i;
                    options.PageCount = 1;

                    // Set resolution (dots per inch)
                    options.Resolution = 96f; // default screen DPI

                    // Calculate the scale factor so that the longest side is at most 100 pixels
                    double maxDimensionInches = Math.Max(pageWidthInches, pageHeightInches);
                    double maxDimensionPixels = maxDimensionInches * options.Resolution;
                    float scale = (float)(100.0 / maxDimensionPixels);
                    // Ensure scale does not exceed 1 (no up‑scaling)
                    options.Scale = scale < 1f ? scale : 1f;

                    // Build output file name
                    string outputFile = $"thumbnail_page_{i + 1}.png";

                    // Save the thumbnail image
                    diagram.Save(outputFile, options);
                    Console.WriteLine($"Saved thumbnail for page {i + 1} to '{outputFile}'.");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
