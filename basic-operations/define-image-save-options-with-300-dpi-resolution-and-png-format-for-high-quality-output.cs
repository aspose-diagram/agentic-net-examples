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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure image save options for high‑quality PNG output.
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Set resolution to 300 DPI.
            saveOptions.Resolution = 300f;

            // Save the diagram as a PNG image using the defined options.
            diagram.Save(outputPath, saveOptions);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}