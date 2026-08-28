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
            string outputFolder = "output_pngs";

            // Ensure the output directory exists
            Directory.CreateDirectory(outputFolder);

            // Load the diagram
            Diagram diagram = new Diagram(sourcePath);

            // Export each page as a PNG image
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Configure PNG save options for the current page
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                pngOptions.PageIndex = i; // zero‑based page index

                // Build the output file name
                string outputPath = Path.Combine(outputFolder, $"Page_{i + 1}.png");

                // Save the page as PNG
                diagram.Save(outputPath, pngOptions);
                Console.WriteLine($"Saved page {i + 1} to {outputPath}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
