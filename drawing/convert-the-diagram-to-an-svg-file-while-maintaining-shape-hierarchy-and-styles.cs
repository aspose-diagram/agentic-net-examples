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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputSvgFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure SVG save options to preserve hierarchy and styles.
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                // Preserve guide shapes and comments (if any) in the output SVG.
                ExportGuideShapes = true,
                IsExportComments = true,
                // Explicitly set the format (required by the API).
                SaveFormat = SaveFileFormat.Svg
            };

            // Save the entire diagram as an SVG file.
            diagram.Save(outputPath, svgOptions);

            Console.WriteLine($"Diagram successfully converted to SVG: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}