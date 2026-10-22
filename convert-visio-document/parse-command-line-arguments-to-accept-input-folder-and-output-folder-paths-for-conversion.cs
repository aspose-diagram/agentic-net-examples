using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments count.
        if (args.Length != 2)
        {
            Console.Error.WriteLine("Usage: <program> <input_folder_path> <output_folder_path>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Guard: verify input folder exists.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Guard: ensure output folder exists (create if necessary).
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder '{outputFolder}': {ex.Message}");
                return;
            }
        }

        // Get all Visio files in the input folder (common extensions).
        string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in visioFiles)
        {
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".vsd" && ext != ".vsdx" && ext != ".vdx" && ext != ".vss" && ext != ".vssx" && ext != ".vst" && ext != ".vstx")
            {
                // Skip non-Visio files.
                continue;
            }

            // Guard: verify the file still exists.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found (skipped): {filePath}");
                continue;
            }

            try
            {
                // Load the diagram.
                Diagram diagram = new Diagram(filePath);

                // Prepare output file path with .vsdx extension.
                string outputFileName = Path.GetFileNameWithoutExtension(filePath) + ".vsdx";
                string outputPath = Path.Combine(outputFolder, outputFileName);

                // Save the diagram in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Converted '{filePath}' to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during loading or saving.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}