using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file path
            string inputPath = @"C:\Input\diagram.vsdx";

            // Output folder for TIFF images
            string outputFolder = @"C:\Output\PagesTiff";

            // Ensure the output directory exists
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];

                // Prepare high‑resolution TIFF save options
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Tiff);
                saveOptions.Resolution = 300f;          // 300 DPI for high quality
                saveOptions.PageIndex = i;              // Export the current page
                saveOptions.PageCount = 1;              // Export only this page

                // Build a file name using page index and name
                string safePageName = string.IsNullOrWhiteSpace(page.NameU) ? $"Page{i + 1}" : page.NameU;
                string outputPath = Path.Combine(outputFolder, $"{safePageName}.tiff");

                // Save the page as a TIFF image
                diagram.Save(outputPath, saveOptions);
            }

            Console.WriteLine("All pages have been exported to TIFF images.");

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
