using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input directory and output directory
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Guard: input folder must exist
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists (create if necessary)
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        // Collect all VSD/VSDX/VDX files from the input folder (non-recursive)
        List<string> visioFiles = new List<string>();
        foreach (string file in Directory.GetFiles(inputFolder))
        {
            string ext = Path.GetExtension(file).ToLowerInvariant();
            if (ext == ".vsd" || ext == ".vsdx" || ext == ".vdx")
            {
                visioFiles.Add(file);
            }
        }

        if (visioFiles.Count == 0)
        {
            Console.Error.WriteLine("No Visio files found in the input folder.");
            return;
        }

        // Convert each Visio file to CSV concurrently
        Parallel.ForEach(visioFiles, visioPath =>
        {
            // Guard: ensure the file still exists
            if (!File.Exists(visioPath))
            {
                Console.Error.WriteLine($"File not found (skipped): {visioPath}");
                return;
            }

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Build output CSV file path
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(visioPath);
                string csvPath = Path.Combine(outputFolder, fileNameWithoutExt + ".csv");

                // Save diagram as CSV
                diagram.Save(csvPath, SaveFileFormat.Csv);

                Console.WriteLine($"Converted: {visioPath} -> {csvPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{visioPath}': {ex.Message}");
            }
        });
    }
}