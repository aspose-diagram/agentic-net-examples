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
                // Iterate through each page in the diagram
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Retrieve the current page
                    Page page = diagram.Pages[i];

                    // Get page dimensions (in inches)
                    double pageWidthInches = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeightInches = page.PageSheet.PageProps.PageHeight.Value;

                    // Configure image save options for PNG thumbnail
                    ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
                    {
                        // Export only the current page
                        PageIndex = i,
                        PageCount = 1,
                        // Define the canvas size using the page dimensions
                        PageSize = new PageSize((float)pageWidthInches, (float)pageHeightInches)
                    };

                    // Build output file name for the thumbnail
                    string outputPath = $"Page_{i + 1}_thumb.png";

                    // Save the thumbnail image
                    diagram.Save(outputPath, saveOptions);
                }
            }

            Console.WriteLine("Thumbnails generated successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
