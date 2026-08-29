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

            // Output folder for TIFF images (must exist)
            string outputFolder = @"C:\Output\TiffPages";

            // Load the Visio diagram from file
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Iterate through each page in the diagram
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Configure image save options for high‑resolution TIFF
                    ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Tiff)
                    {
                        // Set DPI (dots per inch) for high resolution
                        Resolution = 300,

                        // Render only the current page
                        PageIndex = i,
                        PageCount = 1,

                        // Optional: set compression (none for lossless)
                        // TiffCompression = TiffCompression.None,

                        // Preserve vector quality by using EMF rendering internally (handled by Aspose)
                        // No additional settings required here
                    };

                    // Build output file name (e.g., Page_1.tiff, Page_2.tiff, ...)
                    string outputPath = Path.Combine(outputFolder, $"Page_{i + 1}.tiff");

                    // Save the current page as a TIFF image
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
