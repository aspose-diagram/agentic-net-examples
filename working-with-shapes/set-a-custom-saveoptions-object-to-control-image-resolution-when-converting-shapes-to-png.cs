using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Ensure the required arguments are provided: input diagram path and output PNG path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <outputPngPath> [resolutionDPI]");
            return;
        }

        // Assign input and output paths from command‑line arguments.
        string inputPath = args[0];
        // Guard: verify the input Visio file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];
        // Ensure the output directory exists; create it if necessary.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Parse optional resolution argument; default to 300 DPI if not supplied or invalid.
        float resolution = 300f;
        if (args.Length > 2 && float.TryParse(args[2], out float parsedRes) && parsedRes > 0)
        {
            resolution = parsedRes;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Create ImageSaveOptions for PNG output and set the desired resolution (DPI).
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            saveOptions.Resolution = resolution; // Control image resolution.

            // Save the diagram (or the active page) as a PNG image using the custom options.
            diagram.Save(outputPath, saveOptions);

            // Inform the user of successful export.
            Console.WriteLine($"Diagram saved to '{outputPath}' with resolution {resolution} DPI.");
        }
        catch (Exception ex)
        {
            // Write any errors encountered during processing to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}