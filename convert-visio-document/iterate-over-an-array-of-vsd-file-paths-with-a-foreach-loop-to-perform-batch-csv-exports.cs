using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define the list of Visio files to be exported to CSV.
        // Adjust the paths as needed for your environment.
        string[] visioFiles = new string[]
        {
            @"C:\Diagrams\Diagram1.vsd",
            @"C:\Diagrams\Diagram2.vsdx",
            @"C:\Diagrams\Diagram3.vdx"
        };

        // Iterate over each file path and perform the export.
        foreach (string filePath in visioFiles)
        {
            // Guard: ensure the source file exists before processing.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(filePath);

                // Determine the output CSV file path (same folder, .csv extension).
                string outputPath = Path.ChangeExtension(filePath, ".csv");

                // Export the diagram to CSV format.
                diagram.Save(outputPath, SaveFileFormat.Csv);

                Console.WriteLine($"Successfully exported '{filePath}' to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during loading or saving.
                Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }
    }
}