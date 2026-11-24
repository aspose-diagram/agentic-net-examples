using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define the list of Visio files to be exported to CSV.
        string[] visioFiles = new string[]
        {
            @"C:\VisioFiles\Diagram1.vsd",
            @"C:\VisioFiles\Diagram2.vsdx",
            @"C:\VisioFiles\Diagram3.vdx"
        };

        // Iterate over each file path using a foreach loop.
        foreach (string inputPath in visioFiles)
        {
            // Guard: ensure the input file exists before processing.
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                continue;
            }

            try
            {
                // Load the Visio diagram inside a using block as required.
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Determine the output CSV file path (same folder, same name with .csv extension).
                    string outputPath = Path.ChangeExtension(inputPath, ".csv");

                    // Export the diagram to CSV format.
                    diagram.Save(outputPath, SaveFileFormat.Csv);

                    Console.WriteLine($"Successfully exported '{inputPath}' to CSV at '{outputPath}'.");
                }
            }
            catch (Exception ex)
            {
                // Write any Aspose or I/O errors to the error console.
                Console.Error.WriteLine($"Error processing file '{inputPath}': {ex.Message}");
            }
        }
    }
}