using System;
using System.IO;
using Aspose.Diagram;

// Console application that converts all VSD files in a specified folder to CSV files.
    class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder to process.
            string inputFolder;
            if (args.Length > 0)
            {
                inputFolder = args[0];
            }
            else
            {
                Console.Write("Enter the path to the folder containing VSD files: ");
                inputFolder = Console.ReadLine();
            }

            if (string.IsNullOrWhiteSpace(inputFolder) || !Directory.Exists(inputFolder))
            {
                Console.WriteLine("Invalid folder path.");
                return;
            }

            // Get all *.vsd files in the folder (non‑recursive).
            string[] vsdFiles = Directory.GetFiles(inputFolder, "*.vsd", SearchOption.TopDirectoryOnly);

            if (vsdFiles.Length == 0)
            {
                Console.WriteLine("No VSD files found in the specified folder.");
                return;
            }

            foreach (string vsdPath in vsdFiles)
            {
                try
                {
                    // Load the Visio diagram. Explicitly specify the format for clarity.
                    Diagram diagram = new Diagram(vsdPath, LoadFileFormat.Vsd);

                    // Build the output CSV file path (same folder, same name, .csv extension).
                    string csvPath = Path.ChangeExtension(vsdPath, ".csv");

                    // Save the diagram as CSV.
                    diagram.Save(csvPath, SaveFileFormat.Csv);

                    Console.WriteLine($"Converted: \"{Path.GetFileName(vsdPath)}\" -> \"{Path.GetFileName(csvPath)}\"");
                }
                catch (Exception ex)
                {
                    // Report any errors but continue processing remaining files.
                    Console.WriteLine($"Error processing \"{Path.GetFileName(vsdPath)}\": {ex.Message}");
                }
            }

            Console.WriteLine("Conversion completed.");
        }
    }