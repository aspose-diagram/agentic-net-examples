using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputThumbnailPath]");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Determine output path (optional argument or default)
        string outputPath;
        if (args.Length >= 2 && !string.IsNullOrWhiteSpace(args[1]))
        {
            outputPath = args[1];
        }
        else
        {
            // Create a thumbnail file name based on the input file name
            string directory = Path.GetDirectoryName(inputPath) ?? string.Empty;
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_thumb.png");
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure image save options for low‑resolution thumbnail
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    // Export only the first page (zero‑based index)
                    PageIndex = 0,
                    // Render a single page
                    PageCount = 1,
                    // Low resolution (e.g., 72 DPI)
                    Resolution = 72f,
                    // Do not export hidden pages (property expects a bool)
                    ExportHiddenPage = false
                };

                // Save the thumbnail image
                diagram.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"Thumbnail generated successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}