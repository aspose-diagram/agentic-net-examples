using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source VSD file on a network share.
        // Adjust the UNC path as needed.
        string inputPath = @"\\server\share\folder\diagram.vsd";

        // Path where the CSV output will be saved.
        // Adjust the local path as needed.
        string outputPath = @"C:\Temp\diagram.csv";

        // Verify that the input file exists before attempting to load it.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Save the diagram in CSV format.
            diagram.Save(outputPath, SaveFileFormat.Csv);

            Console.WriteLine($"Diagram successfully saved as CSV to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or saving to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}