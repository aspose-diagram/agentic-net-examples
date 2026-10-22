using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> <CacheFolderPath>");
            return;
        }

        string visioPath = args[0];
        string cacheFolder = args[1];

        // Guard: check Visio file existence
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Visio file not found: {visioPath}");
            return;
        }

        // Ensure cache folder exists
        try
        {
            if (!Directory.Exists(cacheFolder))
                Directory.CreateDirectory(cacheFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create cache folder '{cacheFolder}': {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Iterate through each page in the diagram
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Iterate through each shape on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Prepare a unique file name for the shape thumbnail
                    string fileName = $"page_{pageIndex + 1}_shape_{shape.ID}.png";
                    string outputPath = Path.Combine(cacheFolder, fileName);

                    // Configure image save options (PNG format)
                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);

                    // If the API supports explicit width/height, set them to 64 pixels.
                    // These properties may vary by version; adjust as needed.
                    // imgOptions.Width = 64;
                    // imgOptions.Height = 64;

                    // Export the shape to an image file
                    shape.ToImage(outputPath, imgOptions);
                }
            }

            Console.WriteLine("Thumbnails generated successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
        }
    }
}