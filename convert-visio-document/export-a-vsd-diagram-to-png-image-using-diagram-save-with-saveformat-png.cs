using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input VSD file path> <output PNG file path>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure image save options for PNG format
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                // Example: do not export hidden pages (property expects a bool)
                saveOptions.ExportHiddenPage = false; // corrected from BOOL.False

                // Save the diagram as a PNG image
                diagram.Save(outputPath, saveOptions);
                Console.WriteLine($"Diagram successfully exported to PNG: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
