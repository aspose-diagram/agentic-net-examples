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
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];

                // Retrieve page dimensions (in inches)
                double widthInches = page.PageSheet.PageProps.PageWidth.Value;
                double heightInches = page.PageSheet.PageProps.PageHeight.Value;

                // Configure image export options
                ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);
                // Set canvas size to match the page dimensions
                options.PageSize = new PageSize((float)widthInches, (float)heightInches);
                // Export only the current page
                options.PageIndex = i;
                options.PageCount = 1;

                // Generate a thumbnail file name per page
                string outputPath = $"thumbnail_page_{page.ID}.png";

                // Save the page as an image thumbnail
                diagram.Save(outputPath, options);
            }

            // Release resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
