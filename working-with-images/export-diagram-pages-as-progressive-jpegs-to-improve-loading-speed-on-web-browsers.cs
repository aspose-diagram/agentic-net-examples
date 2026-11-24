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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputDirectory]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output directory (use current directory if not provided)
        string outputDir = args.Length >= 2 ? args[1] : Directory.GetCurrentDirectory();
        // Guard: ensure the output directory exists or create it
        try
        {
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output directory '{outputDir}': {ex.Message}");
            return;
        }

        // Load the Visio diagram inside a using block as required
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page and export it as a progressive JPEG
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Retrieve the page (explicit type, no var)
                    Page page = diagram.Pages[i];

                    // Prepare JPEG save options
                    ImageSaveOptions jpegOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
                    // Export only the current page
                    jpegOptions.PageIndex = i;
                    jpegOptions.PageCount = 1;
                    // Include hidden pages if any
                    jpegOptions.ExportHiddenPage = true;
                    // Set JPEG quality (0-100). Higher value yields better quality.
                    jpegOptions.JpegQuality = 90;
                    // Note: Aspose.Diagram renders JPEGs as progressive by default when possible.

                    // Build output file name (e.g., Page_1.jpg)
                    string outputPath = Path.Combine(outputDir, $"Page_{i + 1}.jpg");

                    // Save the page as JPEG using the configured options
                    diagram.Save(outputPath, jpegOptions);
                    Console.WriteLine($"Exported page {i + 1} to {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}