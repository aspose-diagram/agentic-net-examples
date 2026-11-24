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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputPngFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure image save options for PNG export.
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                // Do not export hidden pages (property expects a native bool).
                pngOptions.ExportHiddenPage = false;

                // Transparent background is not explicitly supported via ImageSaveOptions in this version.
                // If a transparent background option becomes available, it can be set here.

                // Save the diagram as PNG using the configured options.
                diagram.Save(outputPath, pngOptions);
                Console.WriteLine($"Diagram exported successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}
