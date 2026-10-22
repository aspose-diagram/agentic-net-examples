using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument) or default.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Output directory for PNG files (second argument) or default.
        string outputDir = args.Length > 1 ? args[1] : "output";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram.
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Process only background pages.
                if (page.Background == BOOL.True)
                {
                    // Iterate through all shapes on the background page.
                    for (int shapeIdx = 0; shapeIdx < page.Shapes.Count; shapeIdx++)
                    {
                        Shape shape = page.Shapes[shapeIdx];

                        // Identify foreign (image) shapes.
                        if (shape.Type == TypeValue.Foreign && shape.Image != null)
                        {
                            // Apply a blur filter (e.g., 25% blur).
                            shape.Image.Blur.Value = 0.25;
                        }
                    }
                }

                // Export the current page to PNG.
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                pngOptions.PageIndex = pageIndex;   // Export this specific page.
                pngOptions.PageCount = 1;           // Export only one page per file.

                string outputPath = Path.Combine(outputDir, $"page_{pageIndex + 1}.png");
                diagram.Save(outputPath, pngOptions);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}