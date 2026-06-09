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

            // Input Visio file path
            string inputPath = "input.vsdx";

            // Output directory for PNG files
            string outputDir = "ExportedPages";
            Directory.CreateDirectory(outputDir);

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Configure PNG export options
                    ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    pngOptions.PageIndex = i;      // Export the current page
                    pngOptions.PageCount = 1;      // Export only this page

                    // NOTE: Aspose.Diagram does not provide an Interlaced property for PNG export.
                    // Interlaced PNGs are not supported, so we export standard PNG files.

                    string outputPath = Path.Combine(outputDir, $"Page_{i + 1}.png");
                    diagram.Save(outputPath, pngOptions);
                    Console.WriteLine($"Exported page {i + 1} to {outputPath}");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
