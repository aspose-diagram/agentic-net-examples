using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path – replace with your actual file or pass as argument.
        string inputPath = args.Length > 0 ? args[0] : "diagram.vsdx";
        // Guard to ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output directory for JPEG files – replace with your actual folder or pass as argument.
        string outputDir = args.Length > 1 ? args[1] : "ExportedPages";
        // Guard to ensure the output directory exists (create if missing).
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
            // Load the Visio diagram from the file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram.
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Retrieve the current page.
                Page page = diagram.Pages[i];

                // Determine page complexity by counting shapes (simple heuristic).
                int shapeCount = page.Shapes.Count;

                // Choose JPEG quality based on shape count.
                int jpegQuality;
                if (shapeCount < 10)
                {
                    jpegQuality = 90; // High quality for simple pages.
                }
                else if (shapeCount <= 30)
                {
                    jpegQuality = 70; // Medium quality for moderate pages.
                }
                else
                {
                    jpegQuality = 50; // Lower quality for complex pages.
                }

                // Configure image save options for JPEG export.
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg)
                {
                    // Export only the current page.
                    PageIndex = i,
                    PageCount = 1,
                    // Apply the calculated quality setting using the correct property name.
                    JpegQuality = jpegQuality
                };

                // Build the output file name (e.g., diagram_Page1.jpg).
                string outputPath = Path.Combine(
                    outputDir,
                    $"{Path.GetFileNameWithoutExtension(inputPath)}_Page{i + 1}.jpg");

                // Export the page as JPEG using the configured options.
                diagram.Save(outputPath, saveOptions);

                // Log successful export.
                Console.WriteLine($"Exported page {i + 1} with {shapeCount} shapes to '{outputPath}' (Quality={jpegQuality}).");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}