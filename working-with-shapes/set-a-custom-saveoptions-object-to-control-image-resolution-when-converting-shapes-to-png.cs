using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputDirectory>");
            return;
        }

        string inputPath = args[0];
        string outputDir = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Ensure the output directory exists (create if necessary).
        try
        {
            Directory.CreateDirectory(outputDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating output directory \"{outputDir}\": {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Create ImageSaveOptions for PNG with a custom resolution.
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            pngOptions.Resolution = 300f; // Set DPI (dots per inch) for high‑resolution output.

            // Iterate through all pages.
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                // Set page‑specific options (optional, here we export one page at a time).
                pngOptions.PageIndex = pageIndex;
                pngOptions.PageCount = 1;

                // Iterate through all shapes on the current page.
                foreach (Shape shape in diagram.Pages[pageIndex].Shapes)
                {
                    // Build a unique file name for each shape using its ID.
                    string shapeFileName = Path.Combine(outputDir, $"shape_{shape.ID}_page{pageIndex + 1}.png");

                    // Export the shape to PNG using the custom save options.
                    shape.ToImage(shapeFileName, pngOptions);
                }
            }

            Console.WriteLine("Shape export completed successfully.");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors.
            Console.Error.WriteLine($"An error occurred during processing: {ex.Message}");
        }
    }
}