using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input VSD/VSDX path, output JPEG path, optional JPEG quality.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputJpegPath> [jpegQuality]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        int jpegQuality = 90; // Default JPEG quality (0-100)

        // Optional JPEG quality argument parsing.
        if (args.Length >= 3 && int.TryParse(args[2], out int parsedQuality))
        {
            // Clamp the quality to the valid range 0-100.
            jpegQuality = Math.Max(0, Math.Min(100, parsedQuality));
        }

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure image save options for JPEG export.
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
                saveOptions.JpegQuality = jpegQuality;          // Set JPEG quality (0-100).
                saveOptions.ExportHiddenPage = false;           // Do not export hidden pages (bool, not BOOL).

                // Save the diagram as a JPEG image using the configured options.
                diagram.Save(outputPath, saveOptions);

                Console.WriteLine($"Diagram successfully saved to JPEG with quality {jpegQuality} at: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or saving to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
