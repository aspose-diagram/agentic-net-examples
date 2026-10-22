using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output directory (optional second argument)
        string outputDir = args.Length > 1 ? args[1] : Path.GetDirectoryName(inputPath);
        if (string.IsNullOrEmpty(outputDir) || !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Load the diagram using the mandatory using pattern
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Retrieve the current page
                    Page page = diagram.Pages[i];

                    // Simple complexity metric: number of shapes on the page
                    int shapeCount = page.Shapes.Count;

                    // Determine JPEG quality based on complexity
                    int jpegQuality;
                    if (shapeCount > 100)
                        jpegQuality = 95; // Very complex page
                    else if (shapeCount > 50)
                        jpegQuality = 85; // Moderately complex page
                    else
                        jpegQuality = 70; // Simple page

                    // Configure image save options for JPEG export
                    ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
                    saveOptions.PageIndex = i;          // Export current page only
                    saveOptions.PageCount = 1;
                    saveOptions.JpegQuality = jpegQuality;
                    // ExportHiddenPage expects a bool, not BOOL
                    saveOptions.ExportHiddenPage = false; // Do not export hidden pages

                    // Build output file path for the current page
                    string outputPath = Path.Combine(outputDir, $"Page_{i + 1}.jpg");

                    // Save the page as a JPEG image
                    diagram.Save(outputPath, saveOptions);
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}