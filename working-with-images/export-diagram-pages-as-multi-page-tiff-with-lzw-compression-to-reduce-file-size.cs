using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputTiffFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure image save options for multi‑page TIFF
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Tiff);

                // Export all pages: start from the first page (index 0) and include all pages
                saveOptions.PageIndex = 0;
                saveOptions.PageCount = diagram.Pages.Count;

                // Set smoothing mode for better rendering quality
                saveOptions.SmoothingMode = Aspose.Diagram.Saving.SmoothingMode.AntiAlias;

                // NOTE: Aspose.Diagram's ImageSaveOptions does not expose a Compression property.
                // LZW compression for TIFF is not directly configurable via the current API,
                // so the default compression will be used.

                // Save the diagram as a multi‑page TIFF file
                diagram.Save(outputPath, saveOptions);
                Console.WriteLine($"Successfully exported to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}