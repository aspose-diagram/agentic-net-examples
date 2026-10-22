using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths (use defaults if not provided)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.jpg";

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Create image save options for JPEG format
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);

            // Set JPEG quality (0-100). Higher value means better quality.
            saveOptions.JpegQuality = 90;

            // Export the diagram to JPEG using the configured options
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Diagram successfully exported to JPEG with quality {saveOptions.JpegQuality}.");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}