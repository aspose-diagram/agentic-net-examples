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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputTiffFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (Diagram implements IDisposable).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure image save options for TIFF format.
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Tiff);
                // Apply LZW compression to reduce file size.
                saveOptions.TiffCompression = TiffCompression.Lzw;
                // Export all pages as a single multi‑page TIFF.
                saveOptions.PageIndex = 0;
                saveOptions.PageCount = diagram.Pages.Count;

                // Save the diagram to the specified TIFF file.
                diagram.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"Diagram successfully exported to multi‑page TIFF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing.
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}