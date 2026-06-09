using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file.
            Diagram diagram = new Diagram("input.vsd");

            // Folder where the JPEG files will be saved.
            string outputFolder = "output";
            Directory.CreateDirectory(outputFolder);

            // Desired JPEG quality (0‑100). Lower values give higher compression.
            int jpegQuality = 70;

            // Export each page of the diagram as a separate JPEG image.
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                // Configure image save options for JPEG format.
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg)
                {
                    PageIndex = pageIndex,   // Zero‑based index of the page to render.
                    PageCount = 1,           // Render only the current page.
                    JpegQuality = jpegQuality,
                    // Aspose.Diagram generates progressive JPEGs by default when saving to JPEG.
                    // No additional property is required.
                };

                // Build the output file name (e.g., Page_1.jpg, Page_2.jpg, ...).
                string outputPath = Path.Combine(outputFolder, $"Page_{pageIndex + 1}.jpg");

                // Save the current page as a JPEG image.
                diagram.Save(outputPath, saveOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
