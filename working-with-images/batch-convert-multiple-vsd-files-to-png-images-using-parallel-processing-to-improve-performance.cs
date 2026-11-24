using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input folder and output folder.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Guard: input folder must exist.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists.
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

        // Collect all .vsd files from the input folder (non‑recursive).
        List<string> vsdFiles = new List<string>();
        foreach (string filePath in Directory.GetFiles(inputFolder, "*.vsd"))
        {
            vsdFiles.Add(filePath);
        }

        if (vsdFiles.Count == 0)
        {
            Console.WriteLine("No VSD files found to process.");
            return;
        }

        // Process each file in parallel to improve performance.
        Parallel.ForEach(vsdFiles, inputPath =>
        {
            // Guard: ensure the file still exists before processing.
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            string outputFileName = Path.GetFileNameWithoutExtension(inputPath) + ".png";
            string outputPath = Path.Combine(outputFolder, outputFileName);

            try
            {
                // Load the Visio diagram inside a using block as required.
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Configure PNG export options.
                    ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    // Export only the first page (optional; remove if full diagram export is desired).
                    pngOptions.PageIndex = 0;
                    pngOptions.PageCount = 1;

                    // Save the diagram as PNG.
                    diagram.Save(outputPath, pngOptions);
                }

                Console.WriteLine($"Converted: {inputPath} -> {outputPath}");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during loading or saving.
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        });
    }
}