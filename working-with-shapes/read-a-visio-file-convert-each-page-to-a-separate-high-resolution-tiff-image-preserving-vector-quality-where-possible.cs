using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioToTiffConverter
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path
            string visioPath = @"C:\Input\sample.vsdx";

            // Output folder for TIFF images
            string outputFolder = @"C:\Output\TiffPages";

            // Ensure output directory exists
            Directory.CreateDirectory(outputFolder);

            // Load the Visio diagram (uses the Diagram(string) constructor)
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Iterate through each page in the diagram
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Configure image save options for high‑resolution TIFF
                    ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Tiff)
                    {
                        // Render only the current page
                        PageIndex = i,
                        PageCount = 1,

                        // High resolution (e.g., 300 DPI)
                        Resolution = 300,

                        // Optional: enlarge page to fit drawing content
                        EnlargePage = true
                    };

                    // Build output file name (e.g., Page_1.tiff)
                    string outputPath = Path.Combine(outputFolder, $"Page_{i + 1}.tiff");

                    // Save the current page as a TIFF image (uses Diagram.Save method)
                    diagram.Save(outputPath, options);
                }
            }

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
