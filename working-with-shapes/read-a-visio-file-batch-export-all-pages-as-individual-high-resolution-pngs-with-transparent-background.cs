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
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Directory where individual page PNGs will be saved
        string outputDir = "output_pages";
        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure image export options (high‑resolution PNG)
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            saveOptions.Resolution = 300; // DPI

            // Export each page as a separate PNG file
            int pageIndex = 0;
            foreach (Page page in diagram.Pages)
            {
                // Set the page index to render the current page
                saveOptions.PageIndex = pageIndex;

                // Build a safe file name using the page name (fallback to index if name is empty)
                string safePageName = string.IsNullOrWhiteSpace(page.Name) ? $"Page_{pageIndex}" : page.Name;
                foreach (char c in Path.GetInvalidFileNameChars())
                {
                    safePageName = safePageName.Replace(c, '_');
                }

                string outputPath = Path.Combine(outputDir, $"{safePageName}.png");

                // Save the current page as PNG
                diagram.Save(outputPath, saveOptions);

                pageIndex++;
            }

            Console.WriteLine("All pages have been exported as high‑resolution PNGs.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}