using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Guard: ensure the output folder exists or create it.
        try
        {
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating output folder \"{outputFolder}\": {ex.Message}");
            return;
        }

        // Load the diagram and export each page as a lossless 16‑bit PNG.
        try
        {
            // Using block is mandatory for Diagram instances.
            using (Diagram diagram = new Diagram(inputPath))
            {
                int pageCount = diagram.Pages.Count;

                // Iterate through all pages.
                for (int i = 0; i < pageCount; i++)
                {
                    // Configure PNG export options.
                    ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png)
                    {
                        // Export a single page at a time.
                        PageIndex = i,
                        PageCount = 1
                        // PNG is inherently lossless; Aspose.Diagram does not expose a direct 16‑bit depth setting.
                    };

                    // Build the output file name.
                    string outputPath = Path.Combine(outputFolder, $"Page_{i + 1}.png");

                    // Save the current page as PNG.
                    diagram.Save(outputPath, pngOptions);
                    Console.WriteLine($"Exported page {i + 1} to \"{outputPath}\"");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}