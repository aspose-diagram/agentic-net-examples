using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputDirectory]");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output directory (optional second argument)
        string outputDir = args.Length >= 2 ? args[1] : Path.GetDirectoryName(inputPath);
        if (string.IsNullOrEmpty(outputDir))
        {
            Console.Error.WriteLine("Unable to determine output directory.");
            return;
        }

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page and export it as a JPEG with 20 % brightness increase
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Configure image save options for JPEG
                    ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg)
                    {
                        PageIndex = i,                 // Export the current page
                        PageCount = 1,                 // Export only this page
                        ImageBrightness = 0.2f,        // Increase brightness by 20 %
                        ExportHiddenPage = false,      // Do not export hidden pages
                        JpegQuality = 90               // Optional: set JPEG quality (0‑100)
                    };

                    // Build the output file path (e.g., Page_1.jpg)
                    string outputPath = Path.Combine(outputDir, $"Page_{i + 1}.jpg");

                    // Save the page as a JPEG image with the configured options
                    diagram.Save(outputPath, saveOptions);
                    Console.WriteLine($"Exported page {i + 1} to JPEG: {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}