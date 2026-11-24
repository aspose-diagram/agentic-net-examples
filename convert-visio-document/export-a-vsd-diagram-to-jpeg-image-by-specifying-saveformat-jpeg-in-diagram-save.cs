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
            Console.Error.WriteLine("Usage: Program <input.vsd> <output.jpg>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block to ensure proper disposal.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure image save options for JPEG format.
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
                // Set JPEG quality (0‑100). Adjust as needed.
                saveOptions.JpegQuality = 90;
                // Do not export hidden pages.
                saveOptions.ExportHiddenPage = false; // BOOL.False is not valid; use bool.

                // Export the entire diagram to a JPEG image.
                diagram.Save(outputPath, saveOptions);
                Console.WriteLine($"Diagram successfully exported to JPEG: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
