using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path as first argument and optional output SVG path as second argument.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputSvgFilePath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Determine output path. If not provided, replace input extension with .svg in the same folder.
        string outputPath = args.Length >= 2 ? args[1] : Path.ChangeExtension(inputPath, ".svg");

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Export the diagram to SVG format.
            diagram.Save(outputPath, SaveFileFormat.Svg);

            // Clean up resources.
            diagram.Dispose();

            Console.WriteLine($"Diagram successfully exported to SVG: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or saving.
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}