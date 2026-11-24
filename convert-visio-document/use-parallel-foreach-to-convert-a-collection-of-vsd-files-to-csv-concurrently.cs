using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output directories from arguments or use defaults.
        string inputDir = args.Length > 0 ? args[0] : "InputVsdFiles";
        string outputDir = args.Length > 1 ? args[1] : "OutputCsvFiles";

        // Guard: input directory must exist.
        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory does not exist: {inputDir}");
            return;
        }

        // Ensure output directory exists.
        if (!Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory '{outputDir}': {ex.Message}");
                return;
            }
        }

        // Collect all VSD and VSDX files from the input directory.
        List<string> files = new List<string>();
        try
        {
            files.AddRange(Directory.GetFiles(inputDir, "*.vsd", SearchOption.AllDirectories));
            files.AddRange(Directory.GetFiles(inputDir, "*.vsdx", SearchOption.AllDirectories));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while enumerating files: {ex.Message}");
            return;
        }

        if (files.Count == 0)
        {
            Console.WriteLine("No VSD/VSDX files found to convert.");
            return;
        }

        // Perform conversion in parallel.
        Parallel.ForEach(files, filePath =>
        {
            // Guard: ensure the file still exists.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found (skipped): {filePath}");
                return;
            }

            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(filePath);

                // Build output CSV file path (same name, .csv extension, placed in output directory).
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                string outputPath = Path.Combine(outputDir, fileNameWithoutExt + ".csv");

                // Save diagram as CSV.
                diagram.Save(outputPath, SaveFileFormat.Csv);

                Console.WriteLine($"Converted '{filePath}' to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        });
    }
}