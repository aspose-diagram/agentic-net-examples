using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Directory containing VSD files
            string inputDirectory = @"C:\VisioFiles";

            // Get all .vsd files in the directory (including subfolders)
            string[] vsdFiles = Directory.GetFiles(inputDirectory, "*.vsd", SearchOption.AllDirectories);

            // Convert each VSD file to CSV concurrently
            Parallel.ForEach(vsdFiles, vsdFilePath =>
            {
                try
                {
                    // Load the Visio diagram using the appropriate load format
                    Diagram diagram = new Diagram(vsdFilePath, LoadFileFormat.Vsd);

                    // Determine output CSV file path (same name, .csv extension)
                    string csvFilePath = Path.ChangeExtension(vsdFilePath, ".csv");

                    // Save the diagram as CSV
                    diagram.Save(csvFilePath, SaveFileFormat.Csv);

                    Console.WriteLine($"Successfully converted: {vsdFilePath} -> {csvFilePath}");
                }
                catch (Exception ex)
                {
                    // Log any errors for this file
                    Console.WriteLine($"Error processing file '{vsdFilePath}': {ex.Message}");
                }
            });

            Console.WriteLine("Conversion process completed.");
        }
    }