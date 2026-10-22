using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the source Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: check if the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Build output filename with timestamp before the extension
            string directory = Path.GetDirectoryName(inputPath);
            string originalFileName = Path.GetFileNameWithoutExtension(inputPath);
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string outputFileName = $"{originalFileName}_{timestamp}.csv";
            string outputPath = Path.Combine(directory, outputFileName);

            // Save the diagram as CSV
            diagram.Save(outputPath, SaveFileFormat.Csv);

            Console.WriteLine($"Diagram successfully saved to CSV: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}