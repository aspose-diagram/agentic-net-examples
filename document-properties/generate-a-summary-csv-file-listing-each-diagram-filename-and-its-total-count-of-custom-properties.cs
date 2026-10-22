using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input_folder_path> [output_csv_path]");
            return;
        }

        string inputFolder = args[0];
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Determine output CSV file path.
        string outputCsv = args.Length > 1 ? args[1] : Path.Combine(Directory.GetCurrentDirectory(), "summary.csv");

        try
        {
            // Prepare CSV writer.
            using (var writer = new StreamWriter(outputCsv, false))
            {
                // Write CSV header.
                writer.WriteLine("Filename,CustomPropertyCount");

                // Define Visio file extensions to process.
                string[] extensions = new[] { "*.vsdx", "*.vsd", "*.vdx", "*.vsx", "*.vssx", "*.vss", "*.vstx", "*.vst" };

                // Iterate over each matching file in the folder (non‑recursive).
                foreach (string ext in extensions)
                {
                    foreach (string filePath in Directory.GetFiles(inputFolder, ext))
                    {
                        // Guard: ensure the file still exists.
                        if (!File.Exists(filePath))
                        {
                            Console.Error.WriteLine($"File not found (skipped): {filePath}");
                            continue;
                        }

                        try
                        {
                            // Load the Visio diagram.
                            using (var diagram = new Diagram(filePath))
                            {
                                // Retrieve the count of custom document properties.
                                int customPropCount = diagram.DocumentProps.CustomProps.Count;

                                // Write a CSV line with the file name and count.
                                string fileName = Path.GetFileName(filePath);
                                writer.WriteLine($"{fileName},{customPropCount}");
                            }
                        }
                        catch (Exception ex)
                        {
                            // Report loading or processing errors but continue with other files.
                            Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
                        }
                    }
                }
            }

            Console.WriteLine($"Summary CSV created at: {outputCsv}");
        }
        catch (Exception ex)
        {
            // Report any unexpected errors (e.g., file I/O issues).
            Console.Error.WriteLine($"Fatal error: {ex.Message}");
        }
    }
}