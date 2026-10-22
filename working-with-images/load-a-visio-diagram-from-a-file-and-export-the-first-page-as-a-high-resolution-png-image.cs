using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output PNG file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPngPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure image save options for high‑resolution PNG export
                ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);
                // Export only the first page (index 0)
                options.PageIndex = 0;
                options.PageCount = 1;
                // Set a high DPI resolution (e.g., 300 DPI) for better quality
                options.Resolution = 300f;

                // Save the first page as a PNG image
                diagram.Save(outputPath, options);
            }

            Console.WriteLine($"First page exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}