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

            // Path to the source Visio file
            string sourcePath = "input.vsdx";

            // Folder where PNG files will be saved
            string outputFolder = "output";
            Directory.CreateDirectory(outputFolder);

            // Load the diagram
            Diagram diagram = new Diagram(sourcePath);

            // Export each page as a PNG image
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Configure PNG export options
                ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);
                options.PageIndex = i; // zero‑based page index

                // Note: Aspose.Diagram does not expose an Interlaced property for PNG.
                // Interlaced PNG export is not supported directly.

                string outputPath = Path.Combine(outputFolder, $"Page_{i + 1}.png");
                diagram.Save(outputPath, options);
                Console.WriteLine($"Saved page {i + 1} to {outputPath}");
            }

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
