using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, output directory, page index, DPI
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <inputVisioPath> <outputDirectory> <pageIndex> <dpi>");
            return;
        }

        string inputPath = args[0];
        string outputDir = args[1];
        string pageIndexArg = args[2];
        string dpiArg = args[3];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard against missing output directory
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        // Parse page index
        if (!int.TryParse(pageIndexArg, out int pageIndex) || pageIndex < 0)
        {
            Console.Error.WriteLine("Invalid page index. It must be a non‑negative integer.");
            return;
        }

        // Parse DPI (resolution)
        if (!int.TryParse(dpiArg, out int dpi) || dpi <= 0)
        {
            Console.Error.WriteLine("Invalid DPI value. It must be a positive integer.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Verify the requested page exists
            if (pageIndex >= diagram.Pages.Count)
            {
                Console.Error.WriteLine($"Page index {pageIndex} is out of range. Diagram contains {diagram.Pages.Count} pages.");
                return;
            }

            // Prepare high‑resolution PNG export options
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
            {
                // Set the desired DPI (resolution)
                Resolution = (float)dpi,
                // Export only the selected page
                PageIndex = pageIndex,
                PageCount = 1,
                // Do not include hidden pages
                ExportHiddenPage = false
            };

            // Build output file path (e.g., page_1.png)
            string outputPath = Path.Combine(outputDir, $"page_{pageIndex + 1}.png");

            // Export the selected page as PNG
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Page {pageIndex} exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}