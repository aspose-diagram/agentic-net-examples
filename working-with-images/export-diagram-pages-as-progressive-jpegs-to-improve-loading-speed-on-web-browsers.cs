using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportDiagramPagesToProgressiveJpeg
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            var diagram = new Diagram("input.vsdx");

            // Iterate through each page in the diagram
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Configure image save options for JPEG format
                var imgOptions = new ImageSaveOptions(SaveFileFormat.Jpeg)
                {
                    // Set JPEG quality (0-100). Lower values increase compression.
                    JpegQuality = 70,

                    // Ensure only the current page is rendered
                    PageIndex = i,
                    PageCount = 1,

                    // Optional: set resolution (dpi) if needed
                    Resolution = 96
                };

                // Build output file name for the current page
                string outputFile = $"Page_{i + 1}.jpg";

                // Save the current page as a JPEG image
                diagram.Save(outputFile, imgOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
