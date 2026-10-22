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

        // Load the diagram and export to a high‑resolution PNG.
        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Configure image export options.
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
            {
                // Set the desired DPI (resolution) to 300.
                Resolution = 300f,

                // Do not export hidden pages. Use native bool type.
                ExportHiddenPage = false
            };

            // Export the diagram to PNG using the configured options.
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Diagram successfully exported to PNG at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any exception details to the error stream.
            Console.Error.WriteLine($"An error occurred during export: {ex.Message}");
        }
    }
}
