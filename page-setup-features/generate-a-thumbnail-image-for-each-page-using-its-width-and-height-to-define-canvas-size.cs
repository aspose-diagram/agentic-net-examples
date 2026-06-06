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

            // Load the Visio diagram from a file
            string inputPath = "input.vsdx";
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Retrieve the current page
                    Page page = diagram.Pages[i];

                    // Get page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Configure image save options
                    ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    saveOptions.PageIndex = i; // Export the specific page
                    // Define canvas size using the page dimensions
                    saveOptions.PageSize = new PageSize((float)pageWidth, (float)pageHeight);

                    // Build output file name for the thumbnail
                    string outputPath = $"thumbnail_page_{i + 1}.png";

                    // Save the page as an image (thumbnail)
                    diagram.Save(outputPath, saveOptions);
                    Console.WriteLine($"Thumbnail saved: {outputPath}");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
