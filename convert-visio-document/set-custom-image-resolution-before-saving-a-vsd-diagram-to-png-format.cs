using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, output file, and desired resolution.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <input.vsd> <output.png> <resolution>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string resolutionArg = args[2];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Parse the resolution value (float). If parsing fails, report the error.
        if (!float.TryParse(resolutionArg, out float resolution) || resolution <= 0)
        {
            Console.Error.WriteLine($"Error: Invalid resolution value '{resolutionArg}'. Must be a positive number.");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API guidelines.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure image save options with the custom resolution.
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                saveOptions.Resolution = resolution; // Set DPI (dots per inch) for the exported PNG.

                // Save the diagram to PNG using the configured options.
                diagram.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"Diagram saved successfully to '{outputPath}' with resolution {resolution} DPI.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}