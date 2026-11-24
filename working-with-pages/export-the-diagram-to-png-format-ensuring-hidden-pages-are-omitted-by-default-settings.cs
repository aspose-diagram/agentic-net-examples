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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputDirectory>");
            return;
        }

        string inputPath = args[0];
        string outputDir = args[1];

        // Guard: input file must exist.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: output directory must exist or be created.
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
                // Iterate over each page and export it as a separate PNG file.
                // Hidden pages will be omitted because ExportHiddenPage is set to false.
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    Page page = diagram.Pages[i];

                    // Prepare PNG save options.
                    ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png)
                    {
                        // Do not export hidden pages.
                        ExportHiddenPage = false,
                        // Export only the current page.
                        PageIndex = i,
                        PageCount = 1
                    };

                    // Build output file name.
                    string outputPath = Path.Combine(outputDir, $"Page_{i + 1}.png");

                    // Save the current page as PNG.
                    diagram.Save(outputPath, pngOptions);
                    Console.WriteLine($"Exported page {i + 1} to '{outputPath}'.");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}