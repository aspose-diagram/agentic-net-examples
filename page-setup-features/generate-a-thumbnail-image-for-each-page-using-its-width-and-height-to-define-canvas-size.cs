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

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                int pageIndex = 0;

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Configure image save options:
                    // - Export as PNG
                    // - Export only the current page
                    // - Set canvas size to match page dimensions
                    // - Apply a scaling factor to create a thumbnail
                    ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);
                    options.PageIndex = pageIndex;      // zero‑based page index
                    options.PageCount = 1;              // export a single page
                    options.PageSize = new PageSize((float)pageWidth, (float)pageHeight);
                    options.Scale = 0.2f;               // 20 % of original size for thumbnail

                    // Define output file name for the thumbnail
                    string outputPath = $"Page_{pageIndex}_thumb.png";

                    // Save the thumbnail image
                    diagram.Save(outputPath, options);

                    pageIndex++;
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
