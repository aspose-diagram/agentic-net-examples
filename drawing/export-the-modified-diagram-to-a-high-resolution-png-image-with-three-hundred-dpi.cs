using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputPngPath]");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Determine output path; if not supplied, replace extension with .png in the same folder
        string outputPath = args.Length >= 2 ? args[1] : Path.ChangeExtension(inputPath, ".png");

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure image save options for high‑resolution PNG (300 DPI)
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                saveOptions.Resolution = 300f; // DPI

                // Export the entire diagram to PNG using the configured options
                diagram.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"Diagram successfully exported to PNG at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}