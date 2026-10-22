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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputPngFile]");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path. If not supplied, replace extension with .png in the same folder.
        string outputPath = args.Length >= 2 ? args[1] : Path.ChangeExtension(inputPath, ".png");

        try
        {
            // Load the Visio diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PNG export options.
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    // Ensure hidden pages are NOT exported.
                    ExportHiddenPage = false // Use bool, not BOOL enum.
                };

                // Save the diagram as PNG using the configured options.
                diagram.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"Diagram successfully exported to PNG: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}
