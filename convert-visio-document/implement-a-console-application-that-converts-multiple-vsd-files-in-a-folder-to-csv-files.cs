using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder containing VSD files.
        string folderPath;
        if (args.Length > 0)
        {
            folderPath = args[0];
        }
        else
        {
            Console.Write("Enter the path to the folder containing VSD files: ");
            folderPath = Console.ReadLine();
        }

        // Guard: ensure the folder exists.
        if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
        {
            Console.Error.WriteLine("The specified folder does not exist.");
            return;
        }

        // Get all .vsd files in the folder (case‑insensitive).
        string[] vsdFiles = Directory.GetFiles(folderPath, "*.vsd", SearchOption.TopDirectoryOnly);

        if (vsdFiles.Length == 0)
        {
            Console.WriteLine("No VSD files found in the specified folder.");
            return;
        }

        // Process each VSD file.
        foreach (string vsdPath in vsdFiles)
        {
            // Guard: ensure the file still exists before processing.
            if (!File.Exists(vsdPath))
            {
                Console.Error.WriteLine($"File not found: {vsdPath}");
                continue;
            }

            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(vsdPath);

                // Build the output CSV file path (same name, .csv extension).
                string csvPath = Path.Combine(
                    Path.GetDirectoryName(vsdPath) ?? folderPath,
                    Path.GetFileNameWithoutExtension(vsdPath) + ".csv");

                // Save the diagram as CSV.
                diagram.Save(csvPath, SaveFileFormat.Csv);

                Console.WriteLine($"Converted '{Path.GetFileName(vsdPath)}' to '{Path.GetFileName(csvPath)}'.");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during loading or saving.
                Console.Error.WriteLine($"Error processing file '{vsdPath}': {ex.Message}");
            }
        }
    }
}