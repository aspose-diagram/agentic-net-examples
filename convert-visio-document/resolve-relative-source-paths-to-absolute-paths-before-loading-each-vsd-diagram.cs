using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Ensure at least one file path is provided.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <relative-or-absolute-path-to-visio-file> [additional files...]");
            return;
        }

        foreach (string inputPath in args)
        {
            // Resolve the path to an absolute path.
            string absolutePath = Path.GetFullPath(inputPath);

            // Guard: verify the file exists before proceeding.
            if (!File.Exists(absolutePath))
            {
                Console.Error.WriteLine($"Error: File not found - '{absolutePath}'.");
                return;
            }

            try
            {
                // Load the Visio diagram from the resolved absolute path.
                Diagram diagram = new Diagram(absolutePath);

                // Prepare the output file name (same folder, suffix "_converted.vsdx").
                string directory = Path.GetDirectoryName(absolutePath);
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(absolutePath);
                string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_converted.vsdx");

                // Save the diagram in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Successfully processed '{absolutePath}' and saved as '{outputPath}'.");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during loading or saving.
                Console.Error.WriteLine($"Error processing '{absolutePath}': {ex.Message}");
                return;
            }
        }
    }
}