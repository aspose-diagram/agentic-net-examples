using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output directory for PNG images (second argument or default)
        string outputDir = args.Length > 1 ? args[1] : "output";

        // Ensure the output directory exists
        try
        {
            Directory.CreateDirectory(outputDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output directory '{outputDir}': {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Iterate through all shapes on the current page
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];

                    // Prepare custom ImageSaveOptions with desired resolution (e.g., 300 DPI)
                    ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    saveOptions.Resolution = 300f; // Set resolution in DPI

                    // Build output file name using shape ID and page index for uniqueness
                    string outputPath = Path.Combine(outputDir, $"page{pageIndex + 1}_shape{shape.ID}.png");

                    // Export the shape to PNG using the custom save options
                    shape.ToImage(outputPath, saveOptions);
                }
            }

            Console.WriteLine("Shape export completed successfully.");
        }
        catch (Exception ex)
        {
            // Log any Aspose.Diagram related errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}