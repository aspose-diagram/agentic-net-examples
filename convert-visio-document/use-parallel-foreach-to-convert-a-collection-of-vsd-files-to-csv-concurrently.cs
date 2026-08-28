using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input folder and output folder.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputFolder> <outputFolder>");
            return;
        }

        // Assign input folder path and verify it exists.
        string inputFolder = args[0];
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Assign output folder path and create it if missing.
        string outputFolder = args[1];
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {outputFolder}. Error: {ex.Message}");
                return;
            }
        }

        // Collect all VSD files from the input folder.
        List<string> vsdFiles = Directory.GetFiles(inputFolder, "*.vsd", SearchOption.TopDirectoryOnly).ToList();

        // If no files found, inform the user and exit.
        if (vsdFiles.Count == 0)
        {
            Console.Error.WriteLine($"No VSD files found in folder: {inputFolder}");
            return;
        }

        // Process each VSD file in parallel.
        Parallel.ForEach(vsdFiles, filePath =>
        {
            // Guard: ensure the file still exists before processing.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found during processing: {filePath}");
                return;
            }

            try
            {
                // Load the Visio diagram from the VSD file.
                Diagram diagram = new Diagram(filePath);

                // Determine the CSV output path (same name, .csv extension).
                string outputCsvPath = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(filePath) + ".csv");

                // Save the diagram as CSV using the appropriate SaveFileFormat.
                diagram.Save(outputCsvPath, SaveFileFormat.Csv);
            }
            catch (Exception ex)
            {
                // Report any errors that occur during load or save.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        });
    }
}