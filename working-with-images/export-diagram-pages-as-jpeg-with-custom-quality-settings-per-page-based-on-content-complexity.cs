using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page by index (required for PageIndex property)
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    Page page = diagram.Pages[i];

                    // Determine page complexity by counting shapes on the page
                    int shapeCount = 0;
                    foreach (Shape shape in page.Shapes)
                    {
                        shapeCount++;
                    }

                    // Set JPEG quality based on complexity
                    // Higher complexity -> lower quality to reduce file size
                    int jpegQuality = shapeCount > 100 ? 80 : 95;

                    // Prepare image save options for JPEG export
                    ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
                    saveOptions.JpegQuality = jpegQuality;   // Custom quality (0-100)
                    saveOptions.PageIndex = i;               // Export this specific page (zero‑based)
                    saveOptions.PageCount = 1;               // Export only one page

                    // Build output file name using the page ID
                    string outputFileName = $"Page_{page.ID}.jpg";

                    // Save the page as JPEG using the configured options
                    diagram.Save(outputFileName, saveOptions);

                    Console.WriteLine($"Saved page {page.ID} with {shapeCount} shapes to {outputFileName} (Quality={jpegQuality})");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}