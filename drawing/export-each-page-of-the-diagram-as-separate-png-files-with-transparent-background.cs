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

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Get total number of pages in the diagram
            int pageCount = diagram.Pages.Count;

            // Loop through each page and export it as a separate PNG file
            for (int i = 0; i < pageCount; i++)
            {
                // Configure image save options for PNG format
                ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    // Render only the current page
                    PageIndex = i,
                    PageCount = 1,

                    // Ensure PNG format is used
                    SaveFormat = SaveFileFormat.Png
                };

                // Build output file name (e.g., Page_0.png, Page_1.png, ...)
                string outputFile = $"Page_{i}.png";

                // Save the current page as PNG with the specified options
                diagram.Save(outputFile, options);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
