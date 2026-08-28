using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file and output directory.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: ExportPng16Bit <inputVisioPath> <outputDirectory>");
            return;
        }

        // Guard for input file existence.
        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard for output directory existence; create if missing.
        string outputDir = args[1];
        if (!Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram.
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Prepare PNG save options; Aspose.Diagram does not expose a 16‑bit mode, so default lossless PNG is used.
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    // Export only the current page.
                    PageIndex = i,
                    PageCount = 1,
                    // Preserve original resolution.
                    Resolution = 300f
                };

                // Build the output file name using the page name (sanitized) and index.
                string pageName = diagram.Pages[i].NameU;
                foreach (char c in Path.GetInvalidFileNameChars())
                    pageName = pageName.Replace(c, '_');
                string outputPath = Path.Combine(outputDir, $"Page_{i + 1}_{pageName}.png");

                // Save the current page as a PNG with the configured options.
                diagram.Save(outputPath, pngOptions);
                Console.WriteLine($"Exported page {i + 1} to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}