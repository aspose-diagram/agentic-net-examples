using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string sourcePath = "input.vsdx";

        // Verify the source file exists before proceeding
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"File not found: {sourcePath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(sourcePath);

            // Iterate through each page in the diagram
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Retrieve the current page
                Page page = diagram.Pages[i];

                // Determine page complexity based on the number of shapes
                int shapeCount = page.Shapes.Count;
                int jpegQuality;

                // Simple heuristic: more shapes → lower quality to keep file size reasonable
                if (shapeCount > 100)
                {
                    jpegQuality = 70; // Lower quality for complex pages
                }
                else if (shapeCount > 50)
                {
                    jpegQuality = 80; // Medium quality
                }
                else
                {
                    jpegQuality = 90; // High quality for simple pages
                }

                // Configure image save options for JPEG export
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg)
                {
                    // Export only the current page
                    PageIndex = i,
                    PageCount = 1,

                    // Set the desired JPEG quality (0‑100) using the correct property name
                    JpegQuality = jpegQuality,

                    // Do not export hidden pages
                    ExportHiddenPage = false
                };

                // Build output file name (e.g., Page_1.jpg, Page_2.jpg, ...)
                string outputPath = $"Page_{i + 1}.jpg";

                // Save the current page as a JPEG image with the specified options
                diagram.Save(outputPath, saveOptions);

                Console.WriteLine($"Exported page {i + 1} with {shapeCount} shapes to '{outputPath}' (Quality={jpegQuality}).");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}