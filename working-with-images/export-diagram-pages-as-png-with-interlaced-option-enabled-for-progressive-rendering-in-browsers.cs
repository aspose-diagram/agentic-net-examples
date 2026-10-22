using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input Visio file and output directory.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputDirectory>");
            return;
        }

        string inputPath = args[0];
        string outputDir = args[1];

        // Guard: check input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure output directory exists or create it.
        if (!Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory '{outputDir}': {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page and export it as a PNG image.
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Configure image save options for PNG.
                    ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    // Export only the current page.
                    saveOptions.PageIndex = i;
                    saveOptions.PageCount = 1;

                    // NOTE: Aspose.Diagram does not support interlaced PNG export.
                    // The interlaced option cannot be enabled; we export standard PNG files.

                    string outputPath = Path.Combine(outputDir, $"Page_{i + 1}.png");
                    diagram.Save(outputPath, saveOptions);
                    Console.WriteLine($"Exported page {i + 1} to {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console.
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}