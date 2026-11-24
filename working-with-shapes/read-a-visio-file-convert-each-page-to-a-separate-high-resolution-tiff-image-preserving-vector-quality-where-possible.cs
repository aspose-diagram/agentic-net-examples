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

        // Guard: ensure the Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Determine output directory (use input file directory if not supplied).
        string outputDir = args.Length > 1 ? args[1] : Path.GetDirectoryName(inputPath);
        if (string.IsNullOrEmpty(outputDir))
        {
            Console.Error.WriteLine("Error: Unable to determine output directory.");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram.
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Configure high‑resolution TIFF export options.
                    ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Tiff);
                    saveOptions.PageIndex = i;          // Export only the current page.
                    saveOptions.PageCount = 1;
                    saveOptions.Resolution = 300f;      // Set DPI to 300 for high quality.
                    saveOptions.TiffCompression = TiffCompression.Lzw; // Use LZW compression.

                    // Build the output file name (e.g., Page_1.tiff).
                    string outputPath = Path.Combine(outputDir, $"Page_{i + 1}.tiff");

                    // Save the current page as a TIFF image.
                    diagram.Save(outputPath, saveOptions);

                    Console.WriteLine($"Saved page {i + 1} to \"{outputPath}\".");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}