using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> <CacheDirectory>");
            return;
        }

        string visioPath = args[0];
        string cacheDir = args[1];

        // Guard: ensure the Visio file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: Visio file not found at '{visioPath}'.");
            return;
        }

        // Ensure the cache directory exists (create if necessary)
        try
        {
            if (!Directory.Exists(cacheDir))
                Directory.CreateDirectory(cacheDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating cache directory '{cacheDir}': {ex.Message}");
            return;
        }

        // Load the Visio diagram inside a using block (mandatory pattern)
        try
        {
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are logically deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Build a unique file name for the shape thumbnail
                        string thumbnailPath = Path.Combine(
                            cacheDir,
                            $"shape_page{page.ID}_id{shape.ID}.png");

                        // Configure image save options for a 64 × 64 PNG thumbnail
                        ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                        // Set the physical size of the rendered image (width & height in pixels)
                        imgOptions.PageSize = new PageSize(64f, 64f);
                        // Do not export hidden pages/shapes
                        imgOptions.ExportHiddenPage = false;

                        // Export the shape to an image file
                        try
                        {
                            shape.ToImage(thumbnailPath, imgOptions);
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Error exporting shape ID {shape.ID} on page {page.ID}: {ex.Message}");
                            // Continue processing remaining shapes
                        }
                    }
                }

                Console.WriteLine("Thumbnail generation completed.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
        }
    }
}