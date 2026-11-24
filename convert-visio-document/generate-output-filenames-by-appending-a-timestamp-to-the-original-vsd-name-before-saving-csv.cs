using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Build output filename with timestamp.
            string directory = Path.GetDirectoryName(inputPath);
            string baseName = Path.GetFileNameWithoutExtension(inputPath);
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string outputFileName = $"{baseName}_{timestamp}.csv";
            string outputPath = Path.Combine(directory, outputFileName);

            // Save the diagram as CSV.
            diagram.Save(outputPath, SaveFileFormat.Csv);

            Console.WriteLine($"CSV exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or saving.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}