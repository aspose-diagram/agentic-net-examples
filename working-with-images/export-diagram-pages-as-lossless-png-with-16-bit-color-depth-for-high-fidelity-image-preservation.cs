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

        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page and export it as a high‑resolution PNG
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Prepare the output file name (e.g., Page_1.png, Page_2.png, ...)
                string outputPath = $"Page_{i + 1}.png";

                // Configure image save options for lossless PNG export
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    // Export only the current page
                    PageIndex = i,
                    PageCount = 1,

                    // Use a high resolution to preserve detail (300 DPI)
                    Resolution = 300f

                    // ImageColorMode defaults to full color; omitted to avoid unsupported enum member
                };

                try
                {
                    // Save the current page as PNG using the configured options
                    diagram.Save(outputPath, saveOptions);
                }
                catch (Exception ex)
                {
                    // Log any errors that occur while saving a specific page
                    Console.Error.WriteLine($"Error saving page {i + 1}: {ex.Message}");
                }
            }

            Console.WriteLine("Export completed.");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during diagram loading or overall processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}