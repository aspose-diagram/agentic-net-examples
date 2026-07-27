using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportDiagramPages
{
    static void Main()
    {
        try
        {

            // Load the diagram file (replace with your actual file path)
            string inputFile = "input.vsdx";
            Diagram diagram = new Diagram(inputFile);

            // Get total number of pages in the diagram
            int totalPages = diagram.Pages.Count;

            // Loop through each page and save it as a separate PNG with transparent background
            for (int i = 0; i < totalPages; i++)
            {
                // Configure image save options for PNG format
                ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    // Render only the current page
                    PageIndex = i,
                    PageCount = 1,

                    // Render only foreground pages (background will be transparent if not present)
                    SaveForegroundPagesOnly = true
                };

                // Build output file name (e.g., Page_1.png, Page_2.png, ...)
                string outputFile = $"Page_{i + 1}.png";

                // Save the current page as PNG using the configured options
                diagram.Save(outputFile, options);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
