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

            // Input Visio file path (change as needed)
            string inputPath = "input.vsdx";

            // Output thumbnail path
            string outputPath = "thumbnail.png";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Get the first page
                Page page = diagram.Pages[0];

                // Page width in inches
                double pageWidthInches = page.PageSheet.PageProps.PageWidth.Value;

                // Default DPI for image rendering (Aspose uses 96 DPI if not set)
                const float dpi = 96f;

                // Desired thumbnail width in pixels
                const float targetWidthPx = 100f;

                // Calculate scale factor to achieve the target width
                float scale = targetWidthPx / ((float)pageWidthInches * dpi);

                // Configure image save options for PNG thumbnail
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    PageIndex = 0,          // Export the first page
                    Scale = scale           // Apply scaling to reach 100px width
                };

                // Save the thumbnail
                diagram.Save(outputPath, saveOptions);
            }

            Console.WriteLine("Thumbnail PNG saved to: " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
