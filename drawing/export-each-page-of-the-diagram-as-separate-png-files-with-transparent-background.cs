using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputDirectory]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Determine output directory (use current directory if not supplied).
        string outputDir = args.Length >= 2 ? args[1] : Directory.GetCurrentDirectory();
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Error: Output directory \"{outputDir}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram.
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Configure image save options for PNG export.
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    // Export only the current page.
                    PageIndex = i,
                    PageCount = 1,
                    // Enable anti‑aliasing for better visual quality.
                    SmoothingMode = Aspose.Diagram.Saving.SmoothingMode.AntiAlias
                    // NOTE: Transparent background is not supported by ImageSaveOptions.
                    // The exported PNG will have the default background color.
                };

                // Build the output file name.
                string outputFileName = $"{Path.GetFileNameWithoutExtension(inputPath)}_Page_{i + 1}.png";
                string outputPath = Path.Combine(outputDir, outputFileName);

                // Save the current page as a PNG file.
                diagram.Save(outputPath, saveOptions);
                Console.WriteLine($"Page {i + 1} saved to \"{outputPath}\"");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}