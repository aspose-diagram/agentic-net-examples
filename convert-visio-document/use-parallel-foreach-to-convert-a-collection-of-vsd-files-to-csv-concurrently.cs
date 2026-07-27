using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Input folder containing VSD files; use first argument or current directory
            string inputFolder = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

            // Get all .vsd files in the folder
            string[] vsdFiles = Directory.GetFiles(inputFolder, "*.vsd", SearchOption.TopDirectoryOnly);

            if (vsdFiles.Length == 0)
            {
                Console.WriteLine("No VSD files found in the specified folder.");
                return;
            }

            // Convert each VSD file to CSV concurrently
            Parallel.ForEach(vsdFiles, vsdPath =>
            {
                try
                {
                    // Load the Visio diagram
                    Diagram diagram = new Diagram(vsdPath);

                    // Determine output CSV path (same name with .csv extension)
                    string csvPath = Path.ChangeExtension(vsdPath, ".csv");

                    // Save the diagram as CSV
                    diagram.Save(csvPath, SaveFileFormat.Csv);

                    Console.WriteLine($"Successfully converted: {Path.GetFileName(vsdPath)} -> {Path.GetFileName(csvPath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{vsdPath}': {ex.Message}");
                }
            });
        }
    }