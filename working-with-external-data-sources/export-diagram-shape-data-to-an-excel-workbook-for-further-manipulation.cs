using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio diagram file path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output CSV file path (can be opened in Excel)
        string outputPath = "shape_data.csv";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Export all shape data to CSV format
                diagram.Save(outputPath, SaveFileFormat.Csv);
                Console.WriteLine($"Shape data exported successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error console
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}