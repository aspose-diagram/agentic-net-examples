using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input Visio file path (first argument) and output CSV path (second argument).
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "shape_data.csv";

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Export shape data to CSV format, which can be opened and edited in Excel.
                diagram.Save(outputPath, SaveFileFormat.Csv);
                Console.WriteLine($"Shape data successfully exported to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or saving.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}