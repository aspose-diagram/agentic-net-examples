using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument) and output directory (second argument)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputDir = args.Length > 1 ? args[1] : "output";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        try
        {
            Directory.CreateDirectory(outputDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating output directory: {ex.Message}");
            return;
        }

        // Load the Visio diagram
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Iterate through all pages
        for (int pageIdx = 0; pageIdx < diagram.Pages.Count; pageIdx++)
        {
            Page page = diagram.Pages[pageIdx];

            // If this is a background page, apply blur effect to its shapes
            if (page.Background == BOOL.True)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // TODO: Apply blur effect to the shape using the appropriate Aspose.Diagram API.
                    // The Aspose.Diagram library does not expose a direct blur effect in the current version.
                    // If a blur effect becomes available, it should be applied here, e.g.:
                    // shape.Effects.Blur = ...;
                }
            }

            // Prepare JPEG export options with high quality settings
            ImageSaveOptions jpegOptions = new ImageSaveOptions(SaveFileFormat.Jpeg)
            {
                JpegQuality = 100,          // Maximum quality
                Resolution = 300f,          // High DPI for better detail
                PageIndex = pageIdx,        // Export the current page only
                PageCount = 1
            };

            // Build output file name
            string outputPath = Path.Combine(outputDir, $"Page_{pageIdx + 1}.jpg");

            // Export the page as a high‑quality JPEG
            try
            {
                diagram.Save(outputPath, jpegOptions);
                Console.WriteLine($"Exported page {pageIdx + 1} to {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error exporting page {pageIdx + 1}: {ex.Message}");
            }
        }

        // Dispose diagram if needed (Diagram implements IDisposable in newer versions)
        try
        {
            diagram?.Dispose();
        }
        catch
        {
            // Ignored – Dispose may not be required in older versions
        }
    }
}