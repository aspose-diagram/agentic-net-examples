using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder containing VSD files.
            string inputFolder;
            if (args.Length > 0 && Directory.Exists(args[0]))
            {
                inputFolder = args[0];
            }
            else
            {
                Console.WriteLine("Enter the full path to the folder containing VSD files:");
                inputFolder = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(inputFolder) || !Directory.Exists(inputFolder))
                {
                    Console.WriteLine("Invalid folder path. Exiting.");
                    return;
                }
            }

            // Get all .vsd files in the folder (non‑recursive).
            string[] vsdFiles = Directory.GetFiles(inputFolder, "*.vsd", SearchOption.TopDirectoryOnly);
            if (vsdFiles.Length == 0)
            {
                Console.WriteLine("No VSD files found in the specified folder.");
                return;
            }

            // Process each VSD file.
            foreach (string vsdPath in vsdFiles)
            {
                try
                {
                    // Load the Visio diagram.
                    Diagram diagram = new Diagram(vsdPath);

                    // Build the output CSV file path (same name, .csv extension).
                    string csvPath = Path.Combine(
                        Path.GetDirectoryName(vsdPath) ?? string.Empty,
                        Path.GetFileNameWithoutExtension(vsdPath) + ".csv");

                    // Save the diagram as CSV.
                    diagram.Save(csvPath, SaveFileFormat.Csv);

                    Console.WriteLine($"Converted: {Path.GetFileName(vsdPath)} -> {Path.GetFileName(csvPath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{vsdPath}': {ex.Message}");
                }
            }

            Console.WriteLine("Conversion completed.");
        }
    }