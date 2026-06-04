using System;
using System.IO;
using Aspose.Diagram;

// Console application that converts all VSD files in a specified folder to CSV files.
    class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder to process. If a path is provided as an argument, use it; otherwise use the current directory.
            string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Error: The folder \"{folderPath}\" does not exist.");
                return;
            }

            // Get all .vsd files in the folder (non‑recursive).
            string[] vsdFiles = Directory.GetFiles(folderPath, "*.vsd", SearchOption.TopDirectoryOnly);

            if (vsdFiles.Length == 0)
            {
                Console.WriteLine("No VSD files found in the specified folder.");
                return;
            }

            foreach (string vsdFilePath in vsdFiles)
            {
                try
                {
                    // Load the Visio diagram using the VSD format.
                    using (Diagram diagram = new Diagram(vsdFilePath, LoadFileFormat.Vsd))
                    {
                        // Build the output CSV file path (same name, .csv extension, placed in the same folder).
                        string csvFilePath = Path.ChangeExtension(vsdFilePath, ".csv");

                        // Save the diagram as CSV.
                        diagram.Save(csvFilePath, SaveFileFormat.Csv);

                        Console.WriteLine($"Converted \"{Path.GetFileName(vsdFilePath)}\" to \"{Path.GetFileName(csvFilePath)}\".");
                    }
                }
                catch (Exception ex)
                {
                    // Report any errors but continue processing remaining files.
                    Console.WriteLine($"Failed to convert \"{Path.GetFileName(vsdFilePath)}\": {ex.Message}");
                }
            }

            Console.WriteLine("Conversion process completed.");
        }
    }