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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPngPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block for proper disposal.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PNG export options with a custom DPI of 600.
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                pngOptions.Resolution = 600f; // Set both horizontal and vertical DPI.

                // Save the diagram (all pages) as a PNG image using the configured options.
                diagram.Save(outputPath, pngOptions);

                Console.WriteLine($"Diagram successfully exported to PNG with 600 DPI: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or saving.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}