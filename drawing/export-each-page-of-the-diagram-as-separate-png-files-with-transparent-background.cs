using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";

        // Verify the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Configure PNG export options for the current page
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                pngOptions.PageIndex = i;      // zero‑based page index
                pngOptions.PageCount = 1;      // export only the current page

                // NOTE: Transparent background is not directly configurable via ImageSaveOptions
                // in the current Aspose.Diagram version. PNG export will retain any existing
                // transparency in the diagram content.

                // Build output file name (e.g., Page_1.png, Page_2.png, ...)
                string outputPath = $"Page_{i + 1}.png";

                // Save the current page as a PNG file using the configured options
                diagram.Save(outputPath, pngOptions);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}