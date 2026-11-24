using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input VSDX path and output JPEG path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.jpg>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram within a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create image save options for JPEG format
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
                // Set JPEG compression quality to 80%
                saveOptions.JpegQuality = 80;
                // Optional: do not export hidden pages (plain bool, not BOOL)
                // saveOptions.ExportHiddenPage = false;

                // Save the diagram as a JPEG image using the configured options
                diagram.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"Diagram successfully converted to JPEG: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}