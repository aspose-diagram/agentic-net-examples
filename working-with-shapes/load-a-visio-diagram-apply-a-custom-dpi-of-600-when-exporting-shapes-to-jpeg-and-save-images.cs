using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument) or default placeholder
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Output directory for JPEG images (second argument) or default folder
        string outputDir = args.Length > 1 ? args[1] : "output_images";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Ensure the output directory exists
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
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure image save options for JPEG with custom DPI (Resolution)
            ImageSaveOptions imageOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
            // Set DPI to 600 (Resolution property defines DPI)
            imageOptions.Resolution = 600;

            // Iterate through each page in the diagram
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Build a unique file name for each shape image
                    string shapeFileName = $"{Path.GetFileNameWithoutExtension(inputPath)}_page{pageIndex + 1}_shape{shape.ID}.jpg";
                    string shapeFilePath = Path.Combine(outputDir, shapeFileName);

                    // Export the shape to JPEG using the configured options
                    shape.ToImage(shapeFilePath, imageOptions);
                }
            }

            Console.WriteLine("Shape export completed successfully.");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors
            Console.Error.WriteLine($"An error occurred during processing: {ex.Message}");
        }
    }
}