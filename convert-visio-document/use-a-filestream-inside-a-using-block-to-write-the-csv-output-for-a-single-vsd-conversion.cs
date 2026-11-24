using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths (use defaults if not provided)
        string inputPath = args.Length > 0 ? args[0] : "input.vsd";
        string outputPath = args.Length > 1 ? args[1] : "output.csv";

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure the directory for the output file exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        // Use a FileStream inside a using block to write the CSV output
        try
        {
            using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Save the diagram as CSV directly to the FileStream
                diagram.Save(outputStream, SaveFileFormat.Csv);
            }

            Console.WriteLine($"Conversion successful. CSV saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}