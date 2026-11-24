using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputThumbnailPath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length >= 2 ? args[1] : "thumbnail.png";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure image save options for a low‑resolution thumbnail
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
            {
                // Export only the first page
                PageIndex = 0,
                PageCount = 1,
                // Reduce size by scaling down (e.g., 20% of original)
                Scale = 0.2f,
                // Do not export hidden pages
                ExportHiddenPage = false
            };

            // Save the thumbnail image
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Thumbnail generated successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}