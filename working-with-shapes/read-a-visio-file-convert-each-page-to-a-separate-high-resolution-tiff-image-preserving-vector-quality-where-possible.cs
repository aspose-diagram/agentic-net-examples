using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioToTiffConverter
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputVisioPath = @"C:\VisioFiles\sample.vsdx";

            // Directory where individual page TIFFs will be saved
            string outputDirectory = @"C:\VisioFiles\TiffPages";
            Directory.CreateDirectory(outputDirectory);

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputVisioPath);

            // Iterate through each page in the diagram
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                // Configure image save options for high‑resolution TIFF
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Tiff)
                {
                    // Render only the current page
                    PageIndex = pageIndex,
                    PageCount = 1,

                    // Set a high DPI resolution (e.g., 300 DPI)
                    Resolution = 300,

                    // Optional: set TIFF compression (LZW) to reduce file size while keeping quality
                    TiffCompression = TiffCompression.Lzw
                };

                // Build the output file name (e.g., Page_1.tiff)
                string outputFilePath = Path.Combine(outputDirectory, $"Page_{pageIndex + 1}.tiff");

                // Save the current page as a TIFF image
                diagram.Save(outputFilePath, saveOptions);
            }

            // Clean up resources
            diagram.Dispose();

            Console.WriteLine("All pages have been exported to high‑resolution TIFF images.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
