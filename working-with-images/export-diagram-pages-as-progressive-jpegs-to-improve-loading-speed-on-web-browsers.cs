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

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsd");

            // Folder where JPEG images will be saved
            string outputFolder = "ExportedJpegs";
            Directory.CreateDirectory(outputFolder);

            // Desired JPEG quality (0‑100). Lower value gives higher compression.
            int jpegQuality = 75;

            // Export each page as a progressive JPEG
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                // Configure image save options for JPEG format
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg)
                {
                    JpegQuality = jpegQuality,   // set compression level
                    PageIndex = pageIndex,       // render this page only
                    PageCount = 1,               // one page at a time
                    Resolution = 96              // optional DPI setting
                };

                // Build the output file name (e.g., Page_1.jpg)
                string outputPath = Path.Combine(outputFolder, $"Page_{pageIndex + 1}.jpg");

                // Save the current page as JPEG
                diagram.Save(outputPath, saveOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
