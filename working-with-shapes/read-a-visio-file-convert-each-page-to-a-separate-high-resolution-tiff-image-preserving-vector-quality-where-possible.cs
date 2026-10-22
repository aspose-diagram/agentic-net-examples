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

        // Guard: ensure the output folder exists (create if necessary).
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: Could not create output folder \"{outputFolder}\": {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram.
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Prepare high‑resolution TIFF export options.
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Tiff)
                {
                    // Export only the current page.
                    PageIndex = i,
                    PageCount = 1,
                    // Set a high DPI for better quality (e.g., 300 DPI).
                    Resolution = 300
                };

                // Build the output file name (e.g., Page_1.tiff).
                string outputPath = Path.Combine(outputFolder, $"Page_{i + 1}.tiff");

                // Save the current page as a TIFF image.
                diagram.Save(outputPath, saveOptions);
                Console.WriteLine($"Saved page {i + 1} to \"{outputPath}\"");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
        }
    }
}