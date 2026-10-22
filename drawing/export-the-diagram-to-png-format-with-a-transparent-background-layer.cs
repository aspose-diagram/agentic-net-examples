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

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure image save options for PNG
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Export only the first page (adjust as needed)
            pngOptions.PageIndex = 0;
            pngOptions.PageCount = 1;

            // Do not export hidden pages (property expects a bool, not BOOL)
            pngOptions.ExportHiddenPage = false; // corrected from BOOL.False

            // Save the diagram as PNG (transparent background is the default for PNG)
            diagram.Save(outputPath, pngOptions);

            Console.WriteLine($"Diagram exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}
