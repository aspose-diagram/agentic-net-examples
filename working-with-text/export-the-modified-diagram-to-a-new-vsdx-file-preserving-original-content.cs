using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFilePath> <outputFilePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: ensure the output directory exists (if specified).
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Export the (potentially modified) diagram to a new .vsdx file,
                // preserving all original content.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}