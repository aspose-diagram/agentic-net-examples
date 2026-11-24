using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define the output directory (current working directory)
        string outputDir = Directory.GetCurrentDirectory();

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram inside a using block
            using (Diagram diagram = new Diagram())
            {
                // Add ten pages and draw a diamond on each page
                for (int i = 0; i < 10; i++)
                {
                    // Create a new page and add it to the diagram
                    Page page = new Page();
                    diagram.Pages.Add(page);

                    // Define diamond vertices as a flat double array:
                    // (2,1) bottom, (3,2) right, (2,3) top, (1,2) left, back to bottom (2,1)
                    double[] diamondPoints = new double[]
                    {
                        2, 1,   // Bottom point
                        3, 2,   // Right point
                        2, 3,   // Top point
                        1, 2,   // Left point
                        2, 1    // Close back to bottom
                    };

                    // Draw the diamond shape as a closed polyline on the current page
                    page.DrawPolyline(diamondPoints);
                }

                // Export each page as a separate PNG file
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Configure PNG save options for a single page
                    ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    pngOptions.PageIndex = i;   // Zero‑based page index
                    pngOptions.PageCount = 1;   // Export only this page

                    // Build the output file path
                    string outputPath = Path.Combine(outputDir, $"Page_{i + 1}.png");

                    // Save the diagram page as PNG
                    diagram.Save(outputPath, pngOptions);
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}