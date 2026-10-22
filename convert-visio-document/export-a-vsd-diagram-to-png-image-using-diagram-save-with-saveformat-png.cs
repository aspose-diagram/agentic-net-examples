using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path as first argument and optional output PNG path as second argument.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputPngFilePath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path; if not provided, use same name with .png extension in the same folder.
        string outputPath = args.Length >= 2 ? args[1] : Path.ChangeExtension(inputPath, ".png");

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Configure image save options for PNG format.
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Example of setting additional options (optional):
            // saveOptions.PageIndex = 0; // Export first page.
            // saveOptions.PageCount = 1; // Export a single page.

            // Export the diagram to PNG using the configured options.
            diagram.Save(outputPath, saveOptions);

            // Dispose the diagram to release resources.
            diagram.Dispose();

            Console.WriteLine($"Diagram successfully exported to PNG: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or saving to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}