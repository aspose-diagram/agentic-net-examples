using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Directory containing the source VSD files
            string inputDirectory = @"C:\InputVsd";

            // Directory where the CSV files will be saved
            string outputDirectory = @"C:\OutputCsv";

            // Ensure the output directory exists
            Directory.CreateDirectory(outputDirectory);

            // Retrieve all VSD files (including subfolders)
            string[] vsdFiles = Directory.GetFiles(inputDirectory, "*.vsd", SearchOption.AllDirectories);

            // Convert each VSD file to CSV in parallel
            Parallel.ForEach(vsdFiles, vsdFile =>
            {
                try
                {
                    // Load the VSD file
                    Diagram diagram = new Diagram(vsdFile);

                    // Build the CSV output path
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(vsdFile);
                    string csvPath = Path.Combine(outputDirectory, fileNameWithoutExt + ".csv");

                    // Save the diagram as CSV
                    diagram.Save(csvPath, SaveFileFormat.Csv);
                }
                catch (Exception ex)
                {
                    // Log any errors for the specific file
                    Console.Error.WriteLine($"Failed to convert '{vsdFile}': {ex.Message}");
                }
            });

            Console.WriteLine("All files have been processed.");

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
